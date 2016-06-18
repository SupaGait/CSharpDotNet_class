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
        private Wall level;

        // Save the screen for drawing and the delegate to print messages
        public DrawObjects(Wall level, Canvas gameScreen, PrintDebugMessage debugMessage){
            this.level = level;
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

        #region events
        private void GameScreen_PointerPressed(object sender, PointerRoutedEventArgs e) {
            debugMessage("Pressed!");

            // New object
            if(currentObject == null) { 

                // Pointer location
                Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(gameScreen);
            
                // Initiate drawing of a new object, save the location to be able to determine mouse relative movement
                currentObject = ShapeFactory.createShape(ShapeFactory.objectShape.SimpleBrickShape);
                currentObject.Shape.Width = 0;
                currentObject.Shape.Height = 0;
                currentShapeStart.X = point.Position.X;
                currentShapeStart.Y = point.Position.Y;
                Canvas.SetLeft(currentObject.Shape, point.Position.X);
                Canvas.SetTop(currentObject.Shape, point.Position.Y);

                // Add the new shape to the canvas, and set initial position
                gameScreen.Children.Add(currentObject.Shape);
            }
            else {
                // Create a new game object (Brick atm)
                float x = (float)Canvas.GetLeft(currentObject.Shape);
                float y = (float)Canvas.GetTop(currentObject.Shape);
                var size = new System.Numerics.Vector2((float)currentObject.Shape.Width, (float)currentObject.Shape.Height);
                var position = new System.Numerics.Vector2(x, y);
                //BaseObject newObject =  new BaseObject(ObjectType.BrickType, position, size, true);
                Brick newBrick = new Brick(position, size, true);

                // Add the base object to the level, no more object selected
                //level.addObject(newObject);
                level.addBrick(newBrick);
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
            message = "X: "+point.Position.X.ToString() + "\nY: " + point.Position.Y.ToString();
            debugMessage(message);

            // Draw/Update the new object
            if (currentObject != null) {
                double objectWidth = point.Position.X - currentShapeStart.X;
                double objectHeight = point.Position.Y - currentShapeStart.Y;

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