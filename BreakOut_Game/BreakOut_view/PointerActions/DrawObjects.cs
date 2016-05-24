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

        private Shape currentShape;
        private Point currentShapeStart;

        // Save the screen for drawing and the delegate to print messages
        public DrawObjects(Canvas gameScreen, PrintDebugMessage debugMessage){
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
            if(currentShape == null) { 

                // Pointer location
                Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(gameScreen);
            
                // Initiate drawing of a new object, save the location to be able to determine mouse relative movement
                currentShape = ShapeFactory.createShape(ShapeFactory.objectShape.SimpleBrickShape);
                currentShape.Width = 0;
                currentShape.Height = 0;
                currentShapeStart.X = point.Position.X;
                currentShapeStart.Y = point.Position.Y;
                Canvas.SetLeft(currentShape, point.Position.X);
                Canvas.SetTop(currentShape, point.Position.Y);

                // Add the new shape to the canvas, and set initial position
                gameScreen.Children.Add(currentShape);
            }
            else {
                // Finish current object
                // Todo
                currentShape = null;
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
            if (currentShape != null) {
                double objectWidth = point.Position.X - currentShapeStart.X;
                double objectHeight = point.Position.Y - currentShapeStart.Y;

                // New width of the object
                if(objectWidth < 0) {
                    Canvas.SetLeft(currentShape, currentShapeStart.X + objectWidth);
                    currentShape.Width = objectWidth * -1;
                }
                else {
                    currentShape.Width = objectWidth;
                }

                // New height of the object
                if(objectHeight < 0) {
                    Canvas.SetTop(currentShape, currentShapeStart.Y + objectHeight);
                    currentShape.Height = objectHeight * -1;
                }
                else {
                    currentShape.Height = objectHeight;
                }
            }
        }
        #endregion // events

        public override PointerMode getType() {
            return PointerMode.DrawMode;
        }
    }
}