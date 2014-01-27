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
		private Circle<Edge> startElement;

		/// <summary>
		/// Die Metadaten des Knotens.
		/// </summary>
		public KnotMetaData MetaData { get; private set; }

		/// <summary>
		/// Ein Ereignis, das in der Move-Methode ausgelöst wird, wenn sich die Struktur der Kanten geändert hat.
		/// </summary>
		public Action EdgesChanged = () => {};

		/// <summary>
		/// Enthält die aktuell vom Spieler selektierten Kanten in der Reihenfolge, in der sie selektiert wurden.
		/// </summary>
		public IEnumerable<Edge> SelectedEdges { get { return selectedEdges; } }

		private List<Edge> selectedEdges;

		/// <summary>
		///
		/// </summary>
		public Action SelectionChanged = () => {};
		private List<SelectionBlock> StructuredSelection;
		private Circle<Edge> lastSelected;
		public Action<Vector3> StartEdgeChanged = (v) => {};

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt einen minimalen Standardknoten. Das Metadaten-Objekt enthält in den Eigenschaften,
		/// die das Speicherformat und den Dateinamen beinhalten, den Wert \glqq null\grqq.
		/// </summary>
		public Knot ()
		{
			MetaData = new KnotMetaData ("", () => startElement.Count, null, null);
			startElement = new Circle<Edge> (new Edge[] {
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
			    countEdges: () => this.startElement.Count,
			    format: metaData.Format,
			    filename: metaData.Filename
			);
			this.startElement = new Circle<Edge> (edges);
			selectedEdges = new List<Edge> ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Prüft, ob eine Verschiebung der aktuellen Kantenauswahl in die angegebene Richtung
		/// um die angegebene Distanz gültig ist.
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
			if (!IsValidMove(direction)) {
				return false;
			}
			Stack<Direction> stack = new Stack<Direction> ();
			for (int b = 0; b < StructuredSelection.Count; ++b) {
				SelectionBlock currentBlock = StructuredSelection [b];
				SelectionBlock nextBlock = StructuredSelection.At (b + 1);

				Circle<Edge> pointer = currentBlock.Begin;
				do {
					stack.Push (pointer.Value.Direction);
					pointer++;
				}
				while (pointer != currentBlock.End.Next);

				for (int i = 0; i < distance; i++) {
					stack.Push (direction.Reverse);
				}
				int counter = 0;
				while (stack.Peek() == pointer.Value.Direction.Reverse && pointer != nextBlock.Begin) {
					if (counter >= distance) { // Passiert, wenn man versucht den Knoten vollständig ineinander zu schieben.
						return false;
					}
					stack.Pop ();
					pointer++;
					counter++;
				}
				while (pointer != nextBlock.Begin) {
					stack.Push (pointer.Value.Direction);
					pointer++;
				}
				for (int i = 0; i < distance; i++) {
					if (stack.Peek () == direction.Reverse) {
						stack.Pop ();
					}
					else {
						stack.Push (direction);
					}
				}
			}
			return IsValidStructure(stack);
		}

		/// <summary>
		/// Prüft ob die gegeben Struktur einen gültigen Knoten darstellt.
		/// </summary>
		public static bool IsValidStructure(Stack<Direction> knot)
		{
			Vector3 position3D = Vector3.Zero;
			HashSet<Vector3> occupancy = new HashSet<Vector3>();
			if (knot.Count < 4) {
				return false;
			}
			while (knot.Count > 0) {
				if (occupancy.Contains(position3D + (knot.Peek() / 2))) {
					return false;
				}
				else {
					occupancy.Add(position3D + (knot.Peek() / 2));
					position3D += knot.Pop();
				}
			}
			if (position3D.DistanceTo(Vector3.Zero) > 0.00001f) {
				return false;
			}
			return true;
		}
		/// <summary>
		/// Gibt an ob ein Move in diese Richtung überhaupt möglich ist.
		/// </summary>
		public bool IsValidMove(Direction dir)
		{
			CreateStructuredSelection();
			if (StructuredSelection.Count == 0) {
				return false;
			}
			// Alles selektiert
			if (StructuredSelection[0].Begin == StructuredSelection[0].End.Next) {
				return true;
			}
			// Für Jeden Block werden Start und ende untersucht.
			foreach (SelectionBlock block in StructuredSelection) {
				// Wenn Kante nach der Bewegung gelöscht werden müsste ist ein Zug nicht möglich
				if (block.Begin.Value.Direction == dir.Reverse && block.Begin.Previous.Value.Direction != dir.Reverse) {
					return false;
				}
				// Wenn Kante nach der Bewegung gelöscht werden müsste ist ein Zug nicht möglich
				if (block.End.Value.Direction == dir && block.End.Next.Value.Direction != dir) {
					return false;
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
					if (pointer.Previous.Value.Direction == direction.Reverse) {
						// Wenn die zu löschende Kante der Einstigspunkt ist, einen neuen setzten.
						if (pointer.Previous == startElement) {
							StartEdgeChanged (pointer.Previous.Value);
							startElement = pointer;
						}
						pointer.Previous.Remove ();
					}
					else {
						pointer.InsertBefore (new Edge (direction));
					}
				}

				//Console.WriteLine("startElement="+startElement);
				for (pointer = currentBlock.Begin; pointer != currentBlock.End.Next; pointer++) {
					//Console.WriteLine("pointer="+pointer);
					if (pointer == startElement) {
						StartEdgeChanged (direction * distance);
					}
				}

				pointer = currentBlock.End;
				// Hinter der Selektion Kanten einfügen, wenn die vorhandenen nicht in die entgegengesetzte Richtung zeigen.
				// Wenn das der Fall ist stattdessen die Kante löschen.
				for (int n = 0; n < distance; n++) {
					if (pointer.Next.Value.Direction == direction) {
						// Wenn die zu löschende Kante der Einstigspunkt ist, einen neuen setzten.
						if (pointer.Next == startElement) {
							StartEdgeChanged (pointer.Value);
							startElement = pointer;
						}
						pointer.Next.Remove ();
					}
					else {
						pointer.InsertAfter (new Edge (direction.Reverse));
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
			return startElement.GetEnumerator ();
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
			Circle<Edge> newCircle = new Circle<Edge> (startElement as IEnumerable<Edge>);
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
			lastSelected = startElement.Find (edge).ElementAt (0);
			OnSelectionChanged ();
		}

		/// <summary>
		/// Entfernt die angegebene Kante von der aktuellen Kantenauswahl.
		/// </summary>
		public void RemoveFromSelection (Edge edge)
		{
			selectedEdges.Remove (edge);
			if (lastSelected.Value == edge) {
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
			if (startElement.Contains (selectedEdge, out selectedCircle) && selectedEdge != lastSelected.Value) {
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
			Knot knotToSave = new Knot (metaData, startElement);
			format.Save (knotToSave);
		}

		/// <summary>
		/// Prüft, ob die räumliche Struktur identisch ist, unabhängig von dem Startpunkt und der Richtung der Datenstruktur.
		/// [parameters=Knot other]
		/// </summary>
		public bool Equals (Knot other)
		{
			KnotCharakteristic thisCharakteristik = Charakteristic ();
			KnotCharakteristic otherCharakteristik = other.Charakteristic ();
			if (thisCharakteristik.CountEdges != otherCharakteristik.CountEdges) {
				return false;
			}
			// Bei Struktur im gleicher Richtung
			if (thisCharakteristik.CharacteristicalEdge.Value.Direction == otherCharakteristik.CharacteristicalEdge.Value.Direction) {
				Circle<Edge> currentThisElement = thisCharakteristik.CharacteristicalEdge.Next;
				Circle<Edge> currentOtherElement = otherCharakteristik.CharacteristicalEdge.Next;
				while (currentThisElement != thisCharakteristik.CharacteristicalEdge) {
					if (currentThisElement.Value.Direction != currentOtherElement.Value.Direction) {
						return false;
					}
					currentThisElement ++;
					currentOtherElement ++;
				}
				return true;
			}
			// Bei Struktur in entgegengesetzter Richtung
			else if (thisCharakteristik.CharacteristicalEdge.Value.Direction == otherCharakteristik.CharacteristicalEdge.Value.Direction.Reverse) {
				Circle<Edge> currentThisElement = thisCharakteristik.CharacteristicalEdge.Next;
				Circle<Edge> currentOtherElement = otherCharakteristik.CharacteristicalEdge.Next;
				while (currentThisElement != thisCharakteristik.CharacteristicalEdge) {
					if (currentThisElement.Value.Direction != currentOtherElement.Value.Direction.Reverse) {
						return false;
					}
					currentThisElement ++;
					currentOtherElement ++;
				}
				return true;
			}
			else {
				return false;
			}
		}

		/// <summary>
		/// Gibt chrakteristische Werte zurück, die bei gleichen Knoten gleich sind.
		/// Einmal als Key ein eindeutiges Circle\<Edge\> Element und als Value
		/// einen Charakteristischen Integer. Momentan die Anzahl der Kanten.
		/// </summary>
		private KnotCharakteristic Charakteristic ()
		{
			Circle<Edge> charakteristikElement = startElement;
			Vector3 position3D = startElement.Value.Direction;
			Vector3 bestPosition3D = startElement.Value.Direction / 2;
			Circle<Edge> edgePointer = startElement.Next;
			int edgeCount = 1;
			for (edgeCount = 1; edgePointer != startElement; edgePointer ++, edgeCount ++) {
				Vector3 nextPosition3D = position3D + edgePointer.Value.Direction / 2;
				if ((nextPosition3D.X < bestPosition3D.X)
				        || (nextPosition3D.X == bestPosition3D.X && nextPosition3D.Y < bestPosition3D.Y)
				        || (nextPosition3D.X == bestPosition3D.X && nextPosition3D.Y == bestPosition3D.Y && nextPosition3D.Z < bestPosition3D.Z)) {

					bestPosition3D = position3D + edgePointer.Value.Direction / 2;
					charakteristikElement = edgePointer;
				}
				position3D += edgePointer.Value.Direction;
			}
			return new KnotCharakteristic (charakteristikElement, edgeCount);
		}

		public override string ToString ()
		{
			return "Knot(name=" + Name + ",#edgecount=" + startElement.Count
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
				StructuredSelection.Add (new SelectionBlock (startElement, startElement.Previous));
				return;
			}
			Circle<Edge> start = startElement;
			Circle<Edge> stop = start.Previous;
			// Suche eine Stelle an der ein Selektionsblock beginnt.
			if (selectedEdges.Contains (start.Value)) {
				// Wenn "edges" in der Selektion ist geh nach links, bis zum Anfang des Blockes.
				while (selectedEdges.Contains(start.Previous.Value)) {
					start --;
				}
			}
			else {
				// Wenn "edges" nicht selektiert ist, gehe nach rechts bis zum beginn des nächsten Blockes.
				while (!selectedEdges.Contains(start.Value)) {
					start ++;
				}
			}
			do {
				// "start" zeigt auf den Beginn eines Blockes und wird daher hinzu gefügt.
				Circle<Edge> begin = start;
				stop = start;
				// Gehe bis zum Ende des selektierten Blockes.
				while (selectedEdges.Contains(stop.Next.Value)) {
					stop ++;
				}
				Circle<Edge> end = stop;

				// Gehe bis zum start des nächsten Blockes.
				start = stop.Next;
				while (!selectedEdges.Contains(start.Value)) {
					start ++;
				}

				// Füge den Selektions-Block der Liste hinzu
				StructuredSelection.Add (new SelectionBlock (begin, end));
			}
			// Höre auf, wenn man wieder beim element ist mit dem man begonnen hat.
			while (start != StructuredSelection[0].Begin);
		}

		#endregion

		#region Classes and Structs

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

		private struct KnotCharakteristic {
			public Circle<Edge> CharacteristicalEdge { get; private set; }

			public int CountEdges { get; private set; }

			public KnotCharakteristic (Circle<Edge> characteristicalEdge, int countEdges) : this()
			{
				CharacteristicalEdge = characteristicalEdge;
				CountEdges = countEdges;
			}
		}

		#endregion
	}
}

