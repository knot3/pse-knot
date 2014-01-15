using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.KnotData
{
	/// <summary>
	/// Diese Klasse repräsentiert einen Knoten, bestehend aus einem Knoten-Metadaten-Objekt und einer doppelt-verketteten Liste von Kanten. Ein Knoten ist eine zyklische Kantenfolge, bei der keine zwei Kanten Kanten den gleichen Raum einnehmen.
	/// </summary>
	public sealed class Knot : ICloneable, IEnumerable<Edge>, IEquatable<Knot>
	{
        #region Properties

		/// <summary>
		/// Der Name des Knotens, welcher auch leer sein kann.
		/// Beim Speichern muss der Nutzer in diesem Fall zwingend einen nichtleeren Namen wählen.
		/// Der Wert dieser Eigenschaft wird aus der \glqq Name\grqq~-Eigenschaft des Metadaten-Objektes geladen
		/// und bei Änderungen wieder in diesem gespeichert.
		/// Beim Ändern dieser Eigenschaft wird automatisch auch der im Metadaten-Objekt enthaltene Dateiname verändert.
		/// </summary>
		public string Name {
			get { return MetaData.Name; }
			set { MetaData.Name = value; }
		}

		/// <summary>
		/// Das Startelement der doppelt-verketteten Liste, in der die Kanten gespeichert werden.
		/// </summary>
		private Circle<Edge> edges;

		/// <summary>
		/// Die Metadaten des Knotens.
		/// </summary>
		public KnotMetaData MetaData { get; private set; }

		/// <summary>
		/// Ein Ereignis, das in der Move-Methode ausgelöst wird, wenn sich die Struktur der Kanten geändert hat.
		/// </summary>
		public Action EdgesChanged { get; set; }

		/// <summary>
		/// Enthält die aktuell vom Spieler selektierten Kanten in der Reihenfolge, in der sie selektiert wurden.
		/// </summary>
		public IEnumerable<Edge> SelectedEdges { get { return selectedEdges; } }
		
		private List<Edge> selectedEdges;

		/// <summary>
		/// 
		/// </summary>
        public Action SelectionChanged { get { return _SelectionChanged; } set { _SelectionChanged = () => { StructuredSelection = null; value(); }; } }
        private Action _SelectionChanged;
        private List<Circle<Edge>> StructuredSelection;

		private Circle<Edge> lastSelected;

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt einen minimalen Standardknoten. Das Metadaten-Objekt enthält in den Eigenschaften,
		/// die das Speicherformat und den Dateinamen beinhalten, den Wert \glqq null\grqq.
		/// </summary>
		public Knot ()
		{
			MetaData = new KnotMetaData ("", () => edges.Count, null, null);
			edges = new Circle<Edge> (new Edge[]{
				Edge.Up, Edge.Right, Edge.Right, Edge.Down, Edge.Backward,
				Edge.Up, Edge.Left, Edge.Left, Edge.Down, Edge.Forward
			}
			);
			selectedEdges = new List<Edge> ();
		}

		/// <summary>
		/// Erstellt einen neuen Knoten mit dem angegebenen Metadaten-Objekt und den angegebenen Kanten,
		/// die in der doppelt verketteten Liste gespeichert werden.
		/// Die Eigenschaft des Metadaten-Objektes, die die Anzahl der Kanten enthält,
		/// wird auf ein Delegate gesetzt, welches jeweils die aktuelle Anzahl der Kanten dieses Knotens zurückgibt.
		/// </summary>
		public Knot (KnotMetaData metaData, IEnumerable<Edge> edges)
		{
			MetaData = new KnotMetaData (
				name: metaData.Name,
				countEdges: () => this.edges.Count,
				format: metaData.Format,
				filename: metaData.Filename
			);
			this.edges = new Circle<Edge> (edges);
			selectedEdges = new List<Edge> ();
		}

        #endregion

        #region Methods

		/// <summary>
		/// Prüft, ob eine Verschiebung der aktuellen Kantenauswahl in die angegebene Richtung um die angegebene Distanz gültig ist.
		/// </summary>
		public bool IsValidMove (Direction direction, int distance)
		{
            CreateStructuredSelection();
            if (StructuredSelection.Count == 0)
                return false;
            if (StructuredSelection.ElementAt(0) == StructuredSelection.ElementAt(1).Next)
                return true;
            Stack<Direction> stack = new Stack<Direction>();
            Circle<Edge> pointer = StructuredSelection.ElementAt(0);
            int position = 1;
            while (position < StructuredSelection.Count)
            {
                do
                {
                    stack.Push(pointer.Content.Direction);
                    pointer = pointer.Next;
                } while (pointer != StructuredSelection.ElementAt(position).Next);
                position++;
                for (int i = 0; i < distance; i++)
                {
                    stack.Push(direction.ReverseDirection());
                }
                while (stack.Peek() == pointer.Content.Direction.ReverseDirection() && pointer != StructuredSelection.ElementAt(position % StructuredSelection.Count))
                {
                    stack.Pop();
                    pointer = pointer.Next;
                }
                while (pointer != StructuredSelection.ElementAt(position % StructuredSelection.Count))
                {
                    stack.Push(pointer.Content.Direction);
                    pointer = pointer.Next;
                }
                for (int i = 0; i < distance; i++)
                {
                    if (stack.Peek() == direction.ReverseDirection())
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(direction);
                    }
                }
            }
            Vector3 pos3D = new Vector3(0, 0, 0);
            HashSet<Vector3> occupancy = new HashSet<Vector3>();
            while (stack.Count > 0)
            {

                if (occupancy.Contains((pos3D + (stack.Peek().ToVector3() / 2))))
                {
                    return false;
                } else {
                    occupancy.Add((pos3D + (stack.Peek().ToVector3() / 2)));
                    pos3D += stack.Pop().ToVector3();
                }
            }
			return true;
		}

		/// <summary>
		/// Verschiebt die aktuelle Kantenauswahl in die angegebene Richtung um die angegebene Distanz.
		/// </summary>
		public bool Move (Direction direction, int distance)
		{
			if (IsValidMove (direction, distance)) {
				HashSet<Edge> selected = new HashSet<Edge> (selectedEdges);

				Circle<Edge> current = edges;
				do {
					if (!selected.Contains (current.Previous.Content) && selected.Contains (current.Content)) {
						for (int i = 0; i < distance; ++i)
							current.InsertBefore (new Edge (direction));
					}
					if (selected.Contains (current.Content) && !selected.Contains (current.Next.Content)) {
						for (int i = 0; i < distance; ++i)
							current.InsertAfter (new Edge (direction.ReverseDirection ()));
					}

					current = current.Next;
				} while (current != edges);

				current = edges;
				do {
					if (current != current.Previous.Previous && current != current.Previous.Previous
						&& current.Previous.Content.Direction == current.Previous.Previous.Content.Direction.ReverseDirection ()) {
						current.Previous.Previous.Remove ();
						current.Previous.Remove ();
					} else {
						current = current.Next;
					}
				} while (current != edges);

				EdgesChanged ();
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Gibt die doppelt-verkettete Kantenliste als Enumerator zurück.
		/// </summary>
		public IEnumerator<Edge> GetEnumerator ()
		{
			return edges.GetEnumerator ();
		}

		/// <summary>
		/// Speichert den Knoten unter dem Dateinamen in dem Dateiformat, das in dem Metadaten-Objekt angegeben ist.
		/// Enthalten entweder die Dateiname-Eigenschaft, die Dateiformat-Eigenschaft
		/// oder beide den Wert \glqq null\grqq, dann wird eine IOException geworfen.
		/// </summary>
		public void Save ()
		{
			if (MetaData.Format == null)
				throw new IOException ("Error: Knot: MetaData.Format is null!");
			else if (MetaData.Filename == null)
				throw new IOException ("Error: Knot: MetaData.Filename is null!");
			else
				MetaData.Format.Save (this);
		}

		/// <summary>
		/// Erstellt eine vollständige Kopie des Knotens, inklusive der Kanten-Datenstruktur und des Metadaten-Objekts.
		/// </summary>
		public Object Clone ()
		{
			Circle<Edge> newCircle = new Circle<Edge> (edges as IEnumerable<Edge>);
			return new Knot {
				MetaData = new KnotMetaData (
					name: MetaData.Name,
					countEdges: () => newCircle.Count,
					format: MetaData.Format,
					filename: MetaData.Filename
				),
				edges = newCircle,
				selectedEdges = new List<Edge>(selectedEdges),
				EdgesChanged = EdgesChanged,
				SelectionChanged = SelectionChanged,
			};
		}

		/// <summary>
		/// Fügt die angegebene Kante zur aktuellen Kantenauswahl hinzu.
		/// </summary>
		public void AddToSelection (Edge edge)
		{
			if (!selectedEdges.Contains (edge)) {
				selectedEdges.Add (edge);
			}
			lastSelected = edges.Find (edge);
			SelectionChanged ();
		}

		/// <summary>
		/// Entfernt die angegebene Kante von der aktuellen Kantenauswahl.
		/// </summary>
		public void RemoveFromSelection (Edge edge)
		{
            selectedEdges.Remove(edge);
            if (lastSelected.Content == edge)
            {
                lastSelected = null;
            }
		}

		/// <summary>
		/// Hebt die aktuelle Kantenauswahl auf.
		/// </summary>
		public void ClearSelection ()
		{
			selectedEdges.Clear ();
			lastSelected = null;
			SelectionChanged ();
		}

		/// <summary>
		/// Fügt alle Kanten auf dem kürzesten Weg zwischen der zuletzt ausgewählten Kante und der angegebenen Kante
		/// zur aktuellen Kantenauswahl hinzu. Sind beide Wege gleich lang,
		/// wird der Weg in Richtung der ersten Kante ausgewählt.
		/// </summary>
		public void AddRangeToSelection (Edge selectedEdge)
		{
			Circle<Edge> selectedCircle = null;
			if (edges.Contains (selectedEdge, out selectedCircle)) {
				List<Edge> forward = new List<Edge> (lastSelected.RangeTo (selectedCircle));
				List<Edge> backward = new List<Edge> (selectedCircle.RangeTo (lastSelected));

				if (forward.Count < backward.Count) {
					foreach (Edge e in forward) {
						if (!selectedEdges.Contains (e)) {
							selectedEdges.Add (e);
						}
					}
				} else {
					foreach (Edge e in backward) {
						if (!selectedEdges.Contains (e)) {
							selectedEdges.Add (e);
						}
					}
				}
				lastSelected = selectedCircle;
			}
			SelectionChanged ();
		}

		/// <summary>
		/// Prüft, ob die angegebene Kante in der aktuellen Kantenauswahl enthalten ist.
		/// </summary>
		public Boolean IsSelected (Edge edge)
		{
			return selectedEdges.Contains (edge);
		}

		/// <summary>
		/// Gibt die doppelt-verkettete Kantenliste als Enumerator zurück.
		/// [name=IEnumerable.GetEnumerator]
		/// [keywords= ]
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // just return the generic version
		}

		/// <summary>
		/// Speichert den Knoten unter dem angegebenen Dateinamen in dem angegebenen Dateiformat.
		/// </summary>
		public void Save (IKnotIO format, string filename)
		{
			KnotMetaData metaData = new KnotMetaData (MetaData.Name, () => MetaData.CountEdges, format, filename);
			Knot knotToSave = new Knot (metaData, edges);
			format.Save (knotToSave);
		}

		/// <summary>
		/// Prüft, ob die räumliche Struktur identisch ist, unabhängig von dem Startpunkt und der Richtung der Datenstruktur.
		/// [parameters=Knot other]
		/// </summary>
		public Boolean Equals (Knot other)
		{
			throw new System.NotImplementedException ();
		}

		public override string ToString ()
		{
			return "Knot(name=" + Name + ",#edgecount=" + edges.Count
				+ ",format=" + (MetaData.Format != null ? MetaData.ToString () : "null")
				+ ")";
		}
        private void CreateStructuredSelection()
        {
            if (StructuredSelection != null)
            {
                return;
            }
            StructuredSelection = new List<Circle<Edge>>();
            if (selectedEdges.Count == 0)
                return;
            if (selectedEdges.Count == MetaData.CountEdges)
            {
                StructuredSelection.Add(edges);
                StructuredSelection.Add(edges.Previous);
                return;
            }
            Circle<Edge> start = edges;
            Circle<Edge> stop = start.Previous;
            if (selectedEdges.Contains(start.Content))
            {
                while (selectedEdges.Contains(start.Previous.Content))
                {
                    start = start.Previous;
                }
            }
            else
            {
                while (!selectedEdges.Contains(start.Content))
                {
                    start = start.Next;
                }
            }
            do
            {
                StructuredSelection.Add(start);
                stop = start;
                while (selectedEdges.Contains(stop.Next.Content))
                {
                    stop = stop.Next;
                }
                start = stop.Next;
                StructuredSelection.Add(stop);
                while (!selectedEdges.Contains(start.Content))
                {
                    start = start.Next;
                }
            } while (StructuredSelection.ElementAt(0) != start);

            /*
            foreach (Edge edge in selectedEdges)
            {
                start = edges.Find(edge);
                stop = start;
                while (selectedEdges.Contains(start.Previous.Content))
                {
                    start = start.Previous;
                    if (StructuredSelection.Contains(start))
                        goto ENDFOREACH;
                }
                while (selectedEdges.Contains(stop.Next.Content))
                {
                    stop = stop.Next;
                    if (StructuredSelection.Contains(stop))
                        goto ENDFOREACH;
                }
                StructuredSelection.Add(start);
                StructuredSelection.Add(stop);
            ENDFOREACH: ;
            }
 */
        }
        #endregion

	}
}

