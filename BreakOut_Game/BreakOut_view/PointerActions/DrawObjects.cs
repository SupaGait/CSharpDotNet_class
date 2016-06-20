using BreakOut_logic;
using BreakOut_logic.Objects;
using BreakOut_view.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace BreakOut_view {
    public class DrawObjects : PointerModeAction {
        private Canvas gameScreen;

        private PrintDebugMessage debugMessage;

        private DrawableObject currentObject;
        private Point currentShapeStart;
        private CreateLevelPage createLevelPage;

        // Save the screen for drawing and the delegate to print messages
        public DrawObjects(CreateLevelPage createLevelPage, Canvas gameScreen, PrintDebugMessage debugMessage){
            this.createLevelPage = createLevelPage;
            this.gameScreen = gameScreen;
            this.debugMessage = debugMessage;
        }

        #region registration
        // Register all the necessary events
        public override void register() {
            gameScreen.PointerMoved += GameScreen_PointerMoved;
            gameScreen.PointerPressed += GameScreen_PointerPressed;
            gameScreen.PointerReleased += GameScreen_PointerReleased;
        }
        // Unregister all the events
        public override void unRegister() {
            gameScreen.PointerMoved -= GameScreen_PointerMoved;
            gameScreen.PointerPressed -= GameScreen_PointerPressed;
            gameScreen.PointerReleased -= GameScreen_PointerReleased;
        }
        #endregion // registration

        public DrawableObject createAndAddObject(ShapeFactory.objectShape objectshape, double width, double height, double xPos, double yPos) {
            DrawableObject drawableObject = ShapeFactory.createShape(objectshape);
            drawableObject.Shape.Width = width;
            drawableObject.Shape.Height = height;
            Canvas.SetLeft(drawableObject.Shape, xPos);
            Canvas.SetTop(drawableObject.Shape, yPos);

            // Add the new shape to the canvas, and set initial position
            createLevelPage.levelObjects.Add(drawableObject);
            gameScreen.Children.Add(drawableObject.Shape);

            return drawableObject;
        }



        #region events
        private void GameScreen_PointerPressed(object sender, PointerRoutedEventArgs e) {
            debugMessage("Pressed!");
            // New object
            if(currentObject == null) { 
                // Pointer location
                Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(gameScreen);

                // Snap it to grid
                Point snappedPoint = createLevelPage.snapToGrid(point.Position);

                // Initiate drawing of a new object, save the location to be able to determine mouse relative movement
                currentObject = createAndAddObject(ShapeFactory.objectShape.SimpleBrickShape, 0, 0, snappedPoint.X, snappedPoint.Y);
                currentShapeStart.X = snappedPoint.X;
                currentShapeStart.Y = snappedPoint.Y;
            }
            else {
                // Create a new game object (Brick atm)
                float x = (float)Canvas.GetLeft(currentObject.Shape);
                float y = (float)Canvas.GetTop(currentObject.Shape);
                var size = new System.Numerics.Vector2((float)currentObject.Shape.Width, (float)currentObject.Shape.Height);
                var position = new System.Numerics.Vector2(x, y);

                // New brick and attach the ID for later use
                Brick newBrick = new Brick(position, size, true);
                currentObject.Id = newBrick.Id;

                // Add the base object to the level, no more object selected
                createLevelPage.Level.addBrick(newBrick);
                currentObject = null;
            }
        }
        private void GameScreen_PointerReleased(object sender, PointerRoutedEventArgs e) {
            debugMessage("Released");
            
            // Stop the drawing of a new object
        }
        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(gameScreen);

            // Debug the position
            String message;
            message = "X: " + point.Position.X.ToString() + "\nY: " + point.Position.Y.ToString();
            debugMessage(message);

            // Snap to grid
            Point snappedPoint = createLevelPage.snapToGrid(point.Position);

            // Move the pointer
            createLevelPage.setPointer(snappedPoint);

            // Draw/Update the new object
            if (currentObject != null) {
                double objectWidth = snappedPoint.X - currentShapeStart.X;
                double objectHeight = snappedPoint.Y - currentShapeStart.Y;

                // New width of the object
                if(objectWidth < 0) {
                    Canvas.SetLeft(currentObject.Shape, currentShapeStart.X + objectWidth);
                    currentObject.Shape.Width = objectWidth * -1;
                }
                else {
                    currentObject.Shape.Width = objectWidth;
                }

                // New height of the object
                if(objectHeight < 0) {
                    Canvas.SetTop(currentObject.Shape, currentShapeStart.Y + objectHeight);
                    currentObject.Shape.Height = objectHeight * -1;
                }
                else {
                    currentObject.Shape.Height = objectHeight;
                }
            }
        }
        #endregion // events

        public override PointerMode getType() {
            return PointerMode.DrawMode;
        }
    }
}