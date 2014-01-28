using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;
using Knot3.Audio;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Ein Inputhandler, der für das Verschieben der Kanten zuständig ist.
	/// </summary>
	public sealed class EdgeMovement : IGameObject, IEnumerable<IGameObject>
	{
		#region Properties

		private IGameScreen screen;

		/// <summary>
		/// Enthält Informationen über die Position des Knotens.
		/// </summary>
		public GameObjectInfo Info { get; private set; }

		/// <summary>
		/// Der Knoten, dessen Kanten verschoben werden können.
		/// </summary>
		public Knot Knot { get; set; }

		/// <summary>
		/// Die Spielwelt, in der sich die 3D-Modelle der Kanten befinden.
		/// </summary>
		public World World { get; set; }

		private Vector3 previousMousePosition = Vector3.Zero;
		private List<ShadowGameObject> shadowObjects;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines EdgeMovement-Objekts und initialisiert diese
		/// mit ihrem zugehörigen IGameScreen-Objekt screen, der Spielwelt world und
		/// Objektinformationen info.
		/// </summary>
		public EdgeMovement (IGameScreen screen, World world, Vector3 position)
		{
			this.screen = screen;
			World = world;
			Info = new GameObjectInfo (position: position);
			shadowObjects = new List<ShadowGameObject> ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gibt den Ursprung des Knotens zurück.
		/// </summary>
		public Vector3 Center ()
		{
			return Info.Position;
		}

		/// <summary>
		/// Gibt immer \glqq null\grqq~zurück.
		/// </summary>
		public GameObjectDistance Intersects (Ray Ray)
		{
			return null;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public void Update (GameTime time)
		{
			SelectEdges (time);
			MoveEdges (time);
		}

		/// <summary>
		/// Führt die Auswahl von Kanten mit Linksklick und evtl. Shift/Ctrl aus.
		/// </summary>
		private void SelectEdges (GameTime time)
		{
			// Überprüfe, ob das Objekt über dem die Maus liegt, eine Pipe ist
			if (World.SelectedObject is PipeModel) {
				PipeModel pipe = World.SelectedObject as PipeModel;

				// Bei einem Linksklick...
				if (InputManager.LeftMouseButton == ClickState.SingleClick) {

					// Zeichne im nächsten Frame auf jeden Fall neu
					World.Redraw = true;

					try {
						Edge selectedEdge = pipe.Info.Edge;
						Console.WriteLine ("knot.Count() = " + Knot.Count ());

						// Ctrl gedrückt
						if (Keys.LeftControl.IsHeldDown ()) {
							Knot.AddToSelection (selectedEdge);
						}
						// Shift gedrückt
						else if (Keys.LeftShift.IsHeldDown ()) {
							Knot.AddRangeToSelection (selectedEdge);
						}
						// keine Taste gedrückt
						else {
							Knot.ClearSelection ();
							Knot.AddToSelection (selectedEdge);
						}

					}
					catch (ArgumentOutOfRangeException exp) {
						Console.WriteLine (exp.ToString ());
					}
				}
			}

			// Wenn das selektierte Objekt weder Kante noch Pfeil ist...
			else if (!(World.SelectedObject is ArrowModel)) {
				// dann leert ein Linksklick die Selektion
				if (InputManager.LeftMouseButton == ClickState.SingleClick) {
					Knot.ClearSelection ();
				}
			}
		}

		/// <summary>
		/// Führt das Verschieben der Kanten aus.
		/// </summary>
		private void MoveEdges (GameTime time)
		{
			// Wenn die Maus über einer Kante oder einem Pfeil liegt
			if (World.SelectedObject is PipeModel || World.SelectedObject is ArrowModel) {
				GameModel selectedModel = World.SelectedObject as GameModel;

				// Berechne die Mausposition in 3D
				Vector3 currentMousePosition = World.Camera.To3D (
				                                   position: InputManager.CurrentMouseState.ToVector2 (),
				                                   nearTo: selectedModel.Center ()
				                               );

				// Wenn die Maus gedrückt gehalten ist und wir mitten im Ziehen der Kante
				// an die neue Position sind
				if (screen.Input.CurrentInputAction == InputAction.SelectedObjectShadowMove) {
					// Wenn dies der erste Frame ist...
					if (previousMousePosition == Vector3.Zero) {
						previousMousePosition = currentMousePosition;
						// dann erstelle die Shadowobjekte und fülle die Liste
						CreateShadowModels ();
					}

					// Setze die Positionen der Shadowobjekte abhängig von der Mausposition
					if (selectedModel is ArrowModel) {
						// Wenn ein Pfeil selektiert wurde, ist die Verschiebe-Richtung eindeutig festgelegt
						UpdateShadowPipes (currentMousePosition, (selectedModel as ArrowModel).Info.Direction);
					}
					else {
						// Wenn etwas anderes (eine Kante) selektiert wurde,
						// muss die Verschiebe-Richtung berechnet werden
						UpdateShadowPipes (currentMousePosition);
					}

					// Zeichne im nächsten Frame auf jeden Fall neu
					World.Redraw = true;
				}

				// Wenn die Verschiebe-Aktion beendet ist (wenn die Maus losgelassen wurde)
				else if (screen.Input.CurrentInputAction == InputAction.SelectedObjectMove) {
					// Führe die finale Verschiebung durch
					if (selectedModel is ArrowModel) {
						// Wenn ein Pfeil selektiert wurde, ist die Verschiebe-Richtung eindeutig festgelegt
						MovePipes (currentMousePosition, (selectedModel as ArrowModel).Info.Direction);
					}
					else {
						// Wenn etwas anderes (eine Kante) selektiert wurde,
						// muss die Verschiebe-Richtung berechnet werden
						MovePipes (currentMousePosition);
					}
					DestroyShadowModels ();
					// Zeichne im nächsten Frame auf jeden Fall neu
					World.Redraw = true;
				}

				// Keine Verschiebeaktion
				else {
					previousMousePosition = Vector3.Zero;

					// Wenn die Shadowobjekt-Liste nicht leer ist...
					if (shadowObjects.Count > 0) {
						// dann leere die Liste
						DestroyShadowModels ();
						// Zeichne im nächsten Frame auf jeden Fall neu
						World.Redraw = true;
					}
				}
			}
		}

		/// <summary>
		/// Bestimme die Richtung und die Länge in Rasterpunkt-Einheiten
		/// und verschiebe die ausgewählten Kanten.
		/// </summary>
		private void MovePipes (Vector3 currentMousePosition, Direction direction)
		{
			float count = ComputeLength (currentMousePosition);
			if (count > 0) {
				try {
					if (Knot.Move (direction, (int)Math.Round (count))) {
						screen.Audio.PlaySound (Sound.PipeMoveSound);
					}
					else {
						screen.Audio.PlaySound (Sound.PipeInvalidMoveSound);
					}
					previousMousePosition = currentMousePosition;

				}
				catch (ArgumentOutOfRangeException exp) {
					Console.WriteLine (exp.ToString ());
				}
			}

			/*Console.WriteLine ("selected:");
			foreach (Edge e in Knot.SelectedEdges) {
				Console.WriteLine ("- " + e);
			}*/
		}

		private void MovePipes (Vector3 currentMousePosition)
		{
			Direction direction = ComputeDirection (currentMousePosition);
			MovePipes (currentMousePosition, direction);
		}

		/// <summary>
		/// Berechne aus der angegebenen aktuellen Mausposition
		/// die hauptsächliche Richtung und die Länge in Rasterpunkt-Einheiten.
		/// </summary>
		private Direction ComputeDirection (Vector3 currentMousePosition)
		{
			Vector3 mouseMove = currentMousePosition - previousMousePosition;
			return mouseMove.PrimaryDirection ().ToDirection ();
		}

		/// <summary>
		/// Berechne aus der angegebenen aktuellen Mausposition
		/// die hauptsächliche Richtung und die gerundete Länge in Rasterpunkt-Einheiten.
		/// </summary>
		private float ComputeLength (Vector3 currentMousePosition)
		{
			Vector3 mouseMove = currentMousePosition - previousMousePosition;
			return mouseMove.Length () / Node.Scale;
		}

		/// <summary>
		/// Erstellt für die selektierten Kantenmodelle und die Pfeile jeweils Shadowobjekte.
		/// </summary>
		private void CreateShadowModels ()
		{
			DestroyShadowModels ();
			foreach (PipeModel pipe in World.OfType<PipeModel>()) {
				if (Knot.SelectedEdges.Contains (pipe.Info.Edge)) {
					pipe.Info.IsVisible = false;
					shadowObjects.Add (new ShadowGameModel (screen, pipe));
				}
			}
			foreach (ArrowModel arrow in World.OfType<ArrowModel>()) {
				arrow.Info.IsVisible = false;
				shadowObjects.Add (new ShadowGameModel (screen, arrow));
			}
		}

		/// <summary>
		/// Entfernt alle Shadowobjekte.
		/// </summary>
		private void DestroyShadowModels ()
		{
			shadowObjects.Clear ();
			foreach (PipeModel pipe in World.OfType<PipeModel>()) {
				pipe.Info.IsVisible = true;
			}
			foreach (ArrowModel arrow in World.OfType<ArrowModel>()) {
				arrow.Info.IsVisible = true;
			}
		}

		/// <summary>
		/// Setze die Position der Shadowobjekte der selektierten Kantenmodelle
		/// auf die von der aktuellen Mausposition abhängende Position.
		/// </summary>
		private void UpdateShadowPipes (Vector3 currentMousePosition, Direction direction, float count)
		{
			foreach (ShadowGameModel shadowObj in shadowObjects) {
				shadowObj.ShadowPosition = shadowObj.OriginalPosition + direction.Vector * count * Node.Scale;
				shadowObj.ShadowAlpha = 1f;
				shadowObj.ShadowColor = Color.White;
			}
		}

		private void UpdateShadowPipes (Vector3 currentMousePosition, Direction direction)
		{
			//Console.WriteLine ("XXX: " + direction);
			float count = ComputeLength (currentMousePosition);
			UpdateShadowPipes (currentMousePosition, direction, count);
		}

		private void UpdateShadowPipes (Vector3 currentMousePosition)
		{
			float count = ComputeLength (currentMousePosition);
			Direction direction = ComputeDirection (currentMousePosition);
			UpdateShadowPipes (currentMousePosition, direction, count);
		}

		/// <summary>
		/// Gibt einen Enumerator über die während einer Verschiebeaktion dynamisch erstellten 3D-Modelle zurück.
		/// [returntype=IEnumerator<IGameObject>]
		/// </summary>
		public IEnumerator<IGameObject> GetEnumerator ()
		{
			foreach (IGameObject shadowObj in shadowObjects) {
				yield return shadowObj;
			}
		}

		// Explicit interface implementation for nongeneric interface
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // Just return the generic version
		}

		/// <summary>
		/// Zeichnet die während einer Verschiebeaktion dynamisch erstellten 3D-Modelle.
		/// </summary>
		public void Draw (GameTime time)
		{
			foreach (IGameObject shadowObj in shadowObjects) {
				shadowObj.Draw (time);
			}
		}

		#endregion
	}
}

