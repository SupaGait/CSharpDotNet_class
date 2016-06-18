using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BreakOut_logic;
using Windows.UI.Xaml.Shapes;
using BreakOut_view.Shapes;
using static BreakOut_view.Shapes.ShapeFactory;
using BreakOut_logic.Objects;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BreakOut_view
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page, IDrawComponents
    {
        private Game theGame;
        private DrawableObject paddleDrawObject;
        private DrawableObject ballDrawObject;
        private List<DrawableObject> brickDrawObjects = new List<DrawableObject>();

        public static float menuSize = 200;
        public static Size windowSize = new Size(1280, 720);

        public GamePage()
        {
            this.InitializeComponent();

            // Reset window drawing size
            //ApplicationView.PreferredLaunchViewSize = windowSize;
            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // Set the game callbacks for drawing to this, currently hardcoded screen size
            theGame = new Game(this, 1, new Size(windowSize.Width - menuSize, windowSize.Height));

            // Create default objects
            paddleDrawObject = createShape(objectShape.PaddleShape);
            GameScreen.Children.Add(paddleDrawObject.Shape);
            ballDrawObject = createShape(objectShape.BallShape);
            GameScreen.Children.Add(ballDrawObject.Shape);
        }

        #region navigation

        // If navigated to here, check the reason
        protected override void OnNavigatedTo(NavigationEventArgs e) {

            // Check if we want to test a level
            if ( e.Parameter is Wall) {
                Wall level = e.Parameter as Wall;
                theGame.LevelManager.Level = level;
            }
            theGame.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            // When leaving, pauze the game
            theGame.Pauze();
            base.OnNavigatedFrom(e);
        }
        #endregion // navigation

        #region drawing
        public void drawPaddle(Paddle paddle) {
            // Sizing
            this.paddleDrawObject.Shape.Width = paddle.Size.X;
            this.paddleDrawObject.Shape.Height = paddle.Size.Y;

            // Positioning
            Canvas.SetLeft(this.paddleDrawObject.Shape, paddle.Position.X);
            Canvas.SetTop(this.paddleDrawObject.Shape, paddle.Position.Y);
        }
        public void drawBricks(List<Brick> bricks) {
            List<Brick> bricksToUpdate = new List<Brick>(bricks);
            List<DrawableObject> brickObjectsToDestroy = new List<DrawableObject>();

            // Update all visual brickObjects
            foreach (DrawableObject brickObject in brickDrawObjects) {
                bool brickIsFound = false;
                foreach (Brick brick in bricksToUpdate) {
                    // Update the brick object if nessecary
                    if (brick.GetId == brickObject.Id) {
                        // TODO: update if necessary
                        bricksToUpdate.Remove(brick);
                        brickIsFound = true;
                        break;
                    }
                }
                if (!brickIsFound) {
                    brickObjectsToDestroy.Add(brickObject);
                }
            }

            // Delete the unused visual object
            foreach (DrawableObject brickObject in brickObjectsToDestroy) {
                brickDrawObjects.Remove(brickObject);
            }
            
            // Create and Add new bricks
            foreach (Brick newBrick in bricksToUpdate) {
                // Create the object
                DrawableObject brickDrawObject = createShape(objectShape.SimpleBrickShape);
                brickDrawObjects.Add(brickDrawObject);
                brickDrawObject.Shape.Height = newBrick.Size.Y;
                brickDrawObject.Shape.Width = newBrick.Size.X;

                // Position on cavas
                GameScreen.Children.Add(brickDrawObject.Shape);
                Canvas.SetLeft(brickDrawObject.Shape, newBrick.Position.X);
                Canvas.SetTop(brickDrawObject.Shape, newBrick.Position.Y);
            }
        }
        public void drawBall(Ball ball) {
            // Sizing
            this.ballDrawObject.Shape.Width = ball.Size.X;
            this.ballDrawObject.Shape.Height = ball.Size.Y;

            // Transform the ball to the new position
            var curX = Canvas.GetLeft(this.ballDrawObject.Shape);
            var curY = Canvas.GetTop(this.ballDrawObject.Shape);
            var transform = new TranslateTransform(); //TODO: attach transform to DrawObject instead of recreating.
            transform.X = ball.Position.X - curX;
            transform.Y = ball.Position.Y - curY;
            this.ballDrawObject.Shape.RenderTransform = transform;

        }
        #endregion //drawing

        #region events
        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(GameScreen);
            theGame.Paddle.setUserPosition((float)point.Position.X, (float)point.Position.Y);
        }

        //Todo: for rescaling feature..
        private void GameScreen_SizeChanged(object sender, SizeChangedEventArgs e) {
            double width = (sender as Canvas).ActualWidth;
            double height = (sender as Canvas).ActualHeight;
            //theGame.SetScreenSize(new Size(width, height));
        }

        private void GameScreen_KeyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Escape) {
                this.Frame.Navigate(typeof(StartPage), this);
            }
        }

        private void button_home_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(StartPage), this);
        }

        #endregion //events
    }
}
