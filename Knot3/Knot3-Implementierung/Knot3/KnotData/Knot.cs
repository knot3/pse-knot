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
using Knot3.Utilities;

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
		public string Name
		{
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
		public Action SelectionChanged { get; set; }

		private List<SelectionBlock> StructuredSelection;
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
			edges = new Circle<Edge> (new Edge[] {
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
			CreateStructuredSelection ();
			if (StructuredSelection.Count == 0) {
				return false;
			}
			if (StructuredSelection [0].Begin == StructuredSelection [0].End.Next) {
				return true;
			}
			Stack<Direction> stack = new Stack<Direction> ();
			for (int b = 0; b < StructuredSelection.Count; ++b) {
				SelectionBlock currentBlock = StructuredSelection [b];
				SelectionBlock nextBlock = StructuredSelection.At (b + 1);

				Circle<Edge> pointer = currentBlock.Begin;
				do {
					stack.Push (pointer.Content.Direction);
					pointer = pointer.Next;
				}
				while (pointer != currentBlock.End.Next);

				for (int i = 0; i < distance; i++) {
					stack.Push (direction.ReverseDirection ());
				}
				int counter = 0;
				while (stack.Peek() == pointer.Content.Direction.ReverseDirection() && pointer != nextBlock.Begin) {
					if (counter >= distance) { // Passiert, wenn man versucht den Knoten vollständig ineinander zu schieben.
						return false;
					}
					stack.Pop ();
					pointer = pointer.Next;
					counter++;
				}
				while (pointer != nextBlock.Begin) {
					stack.Push (pointer.Content.Direction);
					pointer = pointer.Next;
				}
				for (int i = 0; i < distance; i++) {
					if (stack.Peek () == direction.ReverseDirection ()) {
						stack.Pop ();
					}
					else {
						stack.Push (direction);
					}
				}
			}

			Vector3 pos3D = Vector3.Zero;
			HashSet<Vector3> occupancy = new HashSet<Vector3> ();
			while (stack.Count > 0) {
				if (occupancy.Contains (pos3D + (stack.Peek ().ToVector3 () / 2))) {
					return false;
				}
				else {
					occupancy.Add (pos3D + (stack.Peek ().ToVector3 () / 2));
					pos3D += stack.Pop ().ToVector3 ();
				}
			}
			return true;
		}

		/// <summary>
		/// Verschiebt die aktuelle Kantenauswahl in die angegebene Richtung um die angegebene Distanz.
		/// </summary>
		public bool Move (Direction direction, int distance)
		{
			if (!IsValidMove (direction, distance)) {
				return false;
			}
			// Durchlauf über die Selektionsblöcke
			for (int b = 0; b < StructuredSelection.Count; ++b) {
				SelectionBlock currentBlock = StructuredSelection [b];

				Circle<Edge> pointer = currentBlock.Begin;
				// Vor der Selektion Kanten einfügen, wenn die vorhandenen nicht in die entgegengesetzte Richtung zeigen.
				// Wenn das der Fall ist stattdessen die Kante löschen.
				for (int n = 0; n < distance; n++) {
					if (pointer.Previous.Content.Direction == direction.ReverseDirection ()) {
						// Wenn die zu löschende Kante der Einstigspunkt ist, einen neuen setzten.
						if (pointer.Previous == edges) {
							edges = pointer;
						}
						pointer.Previous.Remove ();
					}
					else {
						pointer.InsertBefore (new Edge (direction));
					}
				}

				pointer = currentBlock.End;
				// Hinter der Selektion Kanten einfügen, wenn die vorhandenen nicht in die entgegengesetzte Richtung zeigen.
				// Wenn das der Fall ist stattdessen die Kante löschen.
				for (int n = 0; n < distance; n++) {
					if (pointer.Next.Content.Direction == direction) {
						// Wenn die zu löschende Kante der Einstigspunkt ist, einen neuen setzten.
						if (pointer.Next == edges) {
							edges = edges.Previous;
						}
						pointer.Next.Remove ();
					}
					else {
						pointer.InsertAfter (new Edge (direction.ReverseDirection ()));
					}
				}
			}
			EdgesChanged ();
			return true;
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
			if (MetaData.Format == null) {
				throw new IOException ("Error: Knot: MetaData.Format is null!");
			}
			else if (MetaData.Filename == null) {
				throw new IOException ("Error: Knot: MetaData.Filename is null!");
			}
			else {
				MetaData.Format.Save (this);
			}
		}

		/// <summary>
		/// Erstellt eine vollständige Kopie des Knotens, inklusive der Kanten-Datenstruktur und des Metadaten-Objekts.
		/// </summary>
		public Object Clone ()
		{
			Circle<Edge> newCircle = new Circle<Edge> (edges as IEnumerable<Edge>);
			return new Knot (
			           metaData: new KnotMetaData (
			               name: MetaData.Name,
			               countEdges: () => 0,
			               format: MetaData.Format,
			               filename: MetaData.Filename
			           ),
			           edges: newCircle
			) {
				selectedEdges = new List<Edge>(selectedEdges),
				EdgesChanged = EdgesChanged,
				SelectionChanged = SelectionChanged,
			};
		}

		private void OnSelectionChanged ()
		{
			StructuredSelection = null;
			SelectionChanged ();
		}

		/// <summary>
		/// Fügt die angegebene Kante zur aktuellen Kantenauswahl hinzu.
		/// </summary>
		public void AddToSelection (Edge edge)
		{
			if (!selectedEdges.Contains (edge)) {
				selectedEdges.Add (edge);
			}
			lastSelected = edges.Find (edge).ElementAt (0);
			OnSelectionChanged ();
		}

		/// <summary>
		/// Entfernt die angegebene Kante von der aktuellen Kantenauswahl.
		/// </summary>
		public void RemoveFromSelection (Edge edge)
		{
			selectedEdges.Remove (edge);
			if (lastSelected.Content == edge) {
				lastSelected = null;
			}
			StructuredSelection = null;
			OnSelectionChanged ();
		}

		/// <summary>
		/// Hebt die aktuelle Kantenauswahl auf.
		/// </summary>
		public void ClearSelection ()
		{
			selectedEdges.Clear ();
			lastSelected = null;
			OnSelectionChanged ();
		}

		/// <summary>
		/// Fügt alle Kanten auf dem kürzesten Weg zwischen der zuletzt ausgewählten Kante und der angegebenen Kante
		/// zur aktuellen Kantenauswahl hinzu. Sind beide Wege gleich lang,
		/// wird der Weg in Richtung der ersten Kante ausgewählt.
		/// </summary>
		public void AddRangeToSelection (Edge selectedEdge)
		{
			Circle<Edge> selectedCircle = null;
			if (edges.Contains (selectedEdge, out selectedCircle) && selectedEdge != lastSelected.Content) {
				List<Edge> forward = new List<Edge> (lastSelected.RangeTo (selectedCircle));
				List<Edge> backward = new List<Edge> (selectedCircle.RangeTo (lastSelected));

				if (forward.Count < backward.Count) {
					foreach (Edge e in forward) {
						if (!selectedEdges.Contains (e)) {
							selectedEdges.Add (e);
						}
					}
				}
				else {
					foreach (Edge e in backward) {
						if (!selectedEdges.Contains (e)) {
							selectedEdges.Add (e);
						}
					}
				}
				lastSelected = selectedCircle;
			}
			OnSelectionChanged ();
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
			KeyValuePair<Circle<Edge>, int> thisCharakteristik = Charakteristik();
			KeyValuePair<Circle<Edge>, int> otherCharakteristik = other.Charakteristik();
			if (thisCharakteristik.Value != otherCharakteristik.Value) {
				return false;
			}
			// Bei Struktur im gleicher Richtung
			if (thisCharakteristik.Key.Content.Direction == otherCharakteristik.Key.Content.Direction) {
				Circle<Edge> currentThisCircleElement = thisCharakteristik.Key.Next;
				Circle<Edge> currentOtherCircleElement = otherCharakteristik.Key.Next;
				while (currentThisCircleElement != thisCharakteristik.Key) {
					if (currentThisCircleElement.Content.Direction != currentOtherCircleElement.Content.Direction) {
						return false;
					}
					currentThisCircleElement = currentThisCircleElement.Next;
					currentOtherCircleElement = currentOtherCircleElement.Next;
				}
				return true;
			}
			// Bei Struktur in entgegengesetzter Richtung
			else if (thisCharakteristik.Key.Content.Direction == otherCharakteristik.Key.Content.Direction.ReverseDirection()) {
				Circle<Edge> currentThisCircleElement = thisCharakteristik.Key.Next;
				Circle<Edge> currentOtherCircleElement = otherCharakteristik.Key.Next;
				while (currentThisCircleElement != thisCharakteristik.Key) {
					if (currentThisCircleElement.Content.Direction != currentOtherCircleElement.Content.Direction.ReverseDirection()) {
						return false;
					}
					currentThisCircleElement = currentThisCircleElement.Next;
					currentOtherCircleElement = currentOtherCircleElement.Next;
				}
				return true;
			}
			else {
				return false;
			}
			/*
			Circle<Edge> startA = edges;
			do {
				foreach (Circle<Edge> startB in other.edges.Find ((edge) => edge.Direction == startA.Content.Direction)) {
					// Vorwärts
					Circle<Edge> currentA = startA.Next;
					Circle<Edge> currentB = startB.Next;
					while (currentA.Content.Direction == currentB.Content.Direction) {
						if (currentA == startA) {
							return true;
						}
						currentA = currentA.Next;
						currentB = currentB.Next;
					}
				}
				foreach (Circle<Edge> startB in other.edges.Find ((edge) => edge.Direction.ToVector3() == -startA.Content.Direction.ToVector3())) {
					// Rückwärts
					Circle<Edge> currentA = startA.Next;
					Circle<Edge> currentB = startB.Previous;
					while (currentA.Content.Direction.ToVector3() == -currentB.Content.Direction.ToVector3()) {
						if (currentA == startA) {
							return true;
						}
						currentA = currentA.Next;
						currentB = currentB.Previous;
					}
				}

				startA = startA.Next;
			}
			while (startA != edges);
			return false;
			 */
		}
		/// <summary>
		/// Gibt Chrakteristische werte zurück, die bei gleichen Knoten gleich sind.
		/// Einmal als Key ein eindeutiges Circle\<Edge\> Element und als Value
		/// einen Charakteristischen Integer. Momentan die Anzahl der Kanten.
		/// </summary>
		private KeyValuePair<Circle<Edge>, int> Charakteristik()
		{
			Circle<Edge> charakteristikElement = edges;
			Vector3 position3D = edges.Content.Direction.ToVector3();
			Vector3 bestPosition3D = edges.Content.Direction.ToVector3() / 2;
			Circle<Edge> edgePointer = edges.Next;
			int edgecounter = 1;
			while (edgePointer != edges) {
				if (((position3D + edgePointer.Content.Direction.ToVector3() / 2).X < bestPosition3D.X) ||
				        ((position3D + edgePointer.Content.Direction.ToVector3() / 2).X == bestPosition3D.X && (position3D + edgePointer.Content.Direction.ToVector3() / 2).Y < bestPosition3D.Y) ||
				        ((position3D + edgePointer.Content.Direction.ToVector3() / 2).X == bestPosition3D.X && (position3D + edgePointer.Content.Direction.ToVector3() / 2).Y == bestPosition3D.Y && (position3D + edgePointer.Content.Direction.ToVector3() / 2).Z < bestPosition3D.Z)) {
					bestPosition3D = position3D + edgePointer.Content.Direction.ToVector3() / 2;
					charakteristikElement = edgePointer;
				}
				edgecounter++;
				position3D += edgePointer.Content.Direction.ToVector3();
				edgePointer = edgePointer.Next;
			}
			return new KeyValuePair<Circle<Edge>, int>(charakteristikElement, edgecounter);
		}


		public override string ToString ()
		{
			return "Knot(name=" + Name + ",#edgecount=" + edges.Count
			       + ",format=" + (MetaData.Format != null ? MetaData.ToString () : "null")
			       + ")";
		}

		/// <summary>
		/// Erstellt, falls notwendig, eine Liste mit Circle Elementen, die jeweils Anfang und Ende eines selektierten Bereiches makieren.
		/// Die Liste hat immer 2n Einträge. Abwechselnd immer Anfang und Ende. Beide Innerhalb der Selektion.
		/// D.h. Ist die Selektion nur eine Kante lang, dann sind die Einträge identisch.
		/// </summary>
		private void CreateStructuredSelection ()
		{
			// Erstelle die Liste nur, wenn es notwendig ist.
			if (StructuredSelection != null) {
				return;
			}
			StructuredSelection = new List<SelectionBlock> ();
			// wenn nichts ausgewählt ist muss nichts weiter erstellt werden.
			if (selectedEdges.Count == 0) {
				return;
			}
			// wenn alles ausgewählt ist kann man die erstellung verkürzen.
			if (selectedEdges.Count == MetaData.CountEdges) {
				StructuredSelection.Add (new SelectionBlock (edges, edges.Previous));
				return;
			}
			Circle<Edge> start = edges;
			Circle<Edge> stop = start.Previous;
			// Suche eine Stelle an der ein Selektionsblock beginnt.
			if (selectedEdges.Contains (start.Content)) {
				// Wenn "edges" in der Selektion ist geh nach links, bis zum Anfang des Blockes.
				while (selectedEdges.Contains(start.Previous.Content)) {
					start = start.Previous;
				}
			}
			else {
				// Wenn "edges" nicht selektiert ist, gehe nach rechts bis zum beginn des nächsten Blockes.
				while (!selectedEdges.Contains(start.Content)) {
					start = start.Next;
				}
			}
			do {
				// "start" zeigt auf den Beginn eines Blockes und wird daher hinzu gefügt.
				Circle<Edge> begin = start;
				stop = start;
				// Gehe bis zum Ende des selektierten Blockes.
				while (selectedEdges.Contains(stop.Next.Content)) {
					stop = stop.Next;
				}
				Circle<Edge> end = stop;

				// Gehe bis zum start des nächsten Blockes.
				start = stop.Next;
				while (!selectedEdges.Contains(start.Content)) {
					start = start.Next;
				}

				// Füge den Selektions-Block der Liste hinzu
				StructuredSelection.Add (new SelectionBlock (begin, end));
			}
			// Höre auf, wenn man wieder beim element ist mit dem man begonnen hat.
			while (start != StructuredSelection[0].Begin);
		}

		#endregion

		#region Classes

		private class SelectionBlock
		{
			public Circle<Edge> Begin { get; set; }

			public Circle<Edge> End { get; set; }

			public SelectionBlock (Circle<Edge> begin, Circle<Edge> end)
			{
				Begin = begin;
				End = end;
			}
		}

		#endregion
	}
}

