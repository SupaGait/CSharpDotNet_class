using BreakOut_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace BreakOut_view {
    public class SelectObjects : PointerModeAction {
        private Canvas gameScreen;
        private PrintDebugMessage debugMessage;
        private CreateLevelPage createLevelPage;
        private DrawableObject currentObject;
        private Brush previousColor;
        private bool mousePressedOnObject;
        private Point objectSelectionPosition;

        // Save the screen for drawing and the delegate to print messages
        public SelectObjects(CreateLevelPage createLevelPage, Canvas gameScreen, PrintDebugMessage debugMessage){
            this.createLevelPage = createLevelPage;
            this.gameScreen = gameScreen;
            this.debugMessage = debugMessage;
            mousePressedOnObject = false;
        }

        public override void register() {
            gameScreen.PointerMoved += GameScreen_PointerMoved;
            gameScreen.PointerPressed += GameScreen_PointerPressed;
            gameScreen.PointerReleased += GameScreen_PointerReleased;
            createLevelPage.deleteButton.Click += DeleteButton_Click;
        }

        public override void unRegister() {
            gameScreen.PointerMoved -= GameScreen_PointerMoved;
            gameScreen.PointerPressed -= GameScreen_PointerPressed;
            gameScreen.PointerReleased -= GameScreen_PointerReleased;
            createLevelPage.deleteButton.Click -= DeleteButton_Click;
        }

        public override PointerMode getType() {
            return PointerMode.SelectMode;
        }

        #region events
        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            var cursorPos = e.GetCurrentPoint(gameScreen).Position;

            // Moving objects
            if (mousePressedOnObject && currentObject !=null) {
                double xPos = cursorPos.X - objectSelectionPosition.X;
                double yPos = cursorPos.Y - objectSelectionPosition.Y;

                // Snap it to grid & move pointer
                Point snappedPoint = createLevelPage.snapToGrid(new Point(xPos,yPos));
                createLevelPage.setPointer(snappedPoint);

                // Clipping
                if (snappedPoint.X < 0) {
                    snappedPoint.X = 0;
                }
                else if(snappedPoint.X + currentObject.Shape.ActualWidth > gameScreen.ActualWidth) {
                    snappedPoint.X = gameScreen.ActualWidth - currentObject.Shape.ActualWidth;
                }
                if (snappedPoint.Y < 0) {
                    snappedPoint.Y = 0;
                }
                else if (snappedPoint.Y + currentObject.Shape.ActualHeight > gameScreen.ActualHeight) {
                    snappedPoint.Y = gameScreen.ActualHeight - currentObject.Shape.ActualHeight;
                }

                Canvas.SetLeft(currentObject.Shape, snappedPoint.X);
                Canvas.SetTop(currentObject.Shape, snappedPoint.Y);
            }
            else {
                // Snap it to grid & move pointer
                Point snappedPoint = createLevelPage.snapToGrid(cursorPos);
                createLevelPage.setPointer(snappedPoint);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            // Delete the object
            if (currentObject != null) {
                // Remove from logic,view,and list.
                createLevelPage.Level.Bricks.RemoveAll(brick => brick.Id == currentObject.Id);
                gameScreen.Children.Remove(currentObject.Shape);
                createLevelPage.levelObjects.Remove(currentObject);
                currentObject = null;
            }
        }
        private void GameScreen_PointerReleased(object sender, PointerRoutedEventArgs e) {
            var cursorPos = e.GetCurrentPoint(gameScreen).Position;

            // Save the new coordinates to the logic
            if (mousePressedOnObject && currentObject != null) {
                var brickFound = createLevelPage.Level.Bricks.Find(brick => brick.Id == currentObject.Id);
                if(brickFound != null) {
                    brickFound.Position = new System.Numerics.Vector2((float)Canvas.GetLeft(currentObject.Shape), (float)Canvas.GetTop(currentObject.Shape));
                }
            }
            mousePressedOnObject = false;
        }
        private void GameScreen_PointerPressed(object sender, PointerRoutedEventArgs e) {
            var clickPos = e.GetCurrentPoint(gameScreen).Position;

            // Deselect any current objects
            if (currentObject != null) {
                currentObject.Shape.Fill = previousColor;
                createLevelPage.deleteButton.Visibility = Visibility.Collapsed;
                currentObject = null;
            }

            DrawableObject clickedObject = null;
            // Search the page for objects matching the location, find first
            foreach (DrawableObject obj in createLevelPage.levelObjects) {
                var left = Canvas.GetLeft(obj.Shape);
                var top = Canvas.GetTop(obj.Shape);
                if (clickPos.X > left  &&
                        clickPos.X < left + obj.Shape.ActualWidth &&
                        clickPos.Y > top  &&
                        clickPos.Y < top + obj.Shape.ActualHeight) {
                    clickedObject = obj;
                    break;
                }
            }
            if (clickedObject != null) {
                debugMessage("Selected object: " + clickedObject.Id.ToString());

                // Show that it is selected
                currentObject = clickedObject;
                previousColor = currentObject.Shape.Fill;
                createLevelPage.deleteButton.Visibility = Visibility.Visible;
                currentObject.Shape.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 150, 0));

                // Save relative click position on object
                objectSelectionPosition = new Point(clickPos.X - Canvas.GetLeft(currentObject.Shape), clickPos.Y - Canvas.GetTop(currentObject.Shape));
                mousePressedOnObject = true;
            }

        }
        #endregion // events
    }
}