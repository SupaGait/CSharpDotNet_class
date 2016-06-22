
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BreakOut_logic;
using static BreakOut_view.Shapes.ShapeFactory;
using BreakOut_logic.Objects;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BreakOut_view {

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page, IDrawComponents
    {
        private Game theGame;
        private DrawableObject paddleDrawObject;
        private DrawableObject ballDrawObject;
        private List<DrawableObject> brickDrawObjects = new List<DrawableObject>();
        private Storyboard storyboard = new Storyboard();

        public static float menuSize = 200;
        public static Size windowSize = new Size(1280, 720);

        public GamePage()
        {
            InitializeComponent();

            // Set the game callbacks for drawing to this, currently hardcoded screen size
            theGame = new Game(this, 1, new Size(windowSize.Width - menuSize, windowSize.Height));

            // Create default objects
            paddleDrawObject = createShape(objectShape.PaddleShape);
            GameScreen.Children.Add(paddleDrawObject.Shape);

            ballDrawObject = createShape(objectShape.BallShape);
            ballDrawObject.transform = new TranslateTransform();
            GameScreen.Children.Add(ballDrawObject.Shape);

            // Update stats regulary
            var statsTimer = new DispatcherTimer();
            statsTimer.Tick += StatsTimer_Tick;
            statsTimer.Interval = new TimeSpan(0, 0, 0, 0, 100); // 100ms
            statsTimer.Start();

            GameOverWindow.Visibility = Visibility.Collapsed;
        }

        private void StatsTimer_Tick(object sender, object e) {
            Status status = theGame.Status;
            balls_textBlock.Text = status.Balls.ToString();
            score_textBlock.Text = status.Score.ToString();
            bricksRemaining_textBlock.Text = status.RemainingBricks.ToString();
            level_textBlock.Text = status.CurrentLevel.ToString();

            // Check for game over
            if(status.GameStatus == GameStatus.GameOverStatus || status.GameStatus == GameStatus.GameCompleteStatus) {
                finalScore_textBox.Text = theGame.Status.Score.ToString();
                if (status.GameStatus == GameStatus.GameOverStatus) {
                    finalMessage_textBlock.Text = "Game over";
                }
                else {
                    finalMessage_textBlock.Text = "Winner";
                }
                GameOverWindow.Visibility = Visibility.Visible;
            }
        }

        #region navigation

        // If navigated to here, check the reason
        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            try {
                // Check if we want to test a level
                if (e.Parameter is Wall) {
                    Wall level = e.Parameter as Wall;
                    theGame.LevelManager.Level = level;
                    theGame.start();
                }
                else {
                    // Normal (re)start
                    theGame.startScenario();
                }
            }
            catch (Exception ex) {
                MessageDialog dialog = new MessageDialog("Error: " + ex.ToString());
                await dialog.ShowAsync();
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            // When leaving, pauze the game
            theGame.pauze();
            base.OnNavigatedFrom(e);
        }
        #endregion // navigation

        #region drawing
        public void drawPaddle(Paddle paddle) {
            // Sizing
            paddleDrawObject.Shape.Width = paddle.Size.X;
            paddleDrawObject.Shape.Height = paddle.Size.Y;

            // Positioning
            Canvas.SetLeft(paddleDrawObject.Shape, paddle.Position.X);
            Canvas.SetTop(paddleDrawObject.Shape, paddle.Position.Y);
        }
        public void drawBricks(List<Brick> bricks) {
            List<Brick> bricksToUpdate = new List<Brick>(bricks);
            List<DrawableObject> brickObjectsToDestroy = new List<DrawableObject>();

            // Update all visual brickObjects
            foreach (DrawableObject brickObject in brickDrawObjects) {
                bool brickIsFound = false;
                foreach (Brick brick in bricksToUpdate) {
                    // Update the brick object if nessecary
                    if (brick.Id == brickObject.Id) {
                        // Update if necessary (When additional features are added to the brick)
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
                GameScreen.Children.Remove(brickObject.Shape);
                brickDrawObjects.Remove(brickObject);
            }
            
            // Create and Add new bricks
            foreach (Brick newBrick in bricksToUpdate) {
                // Create the object
                DrawableObject brickDrawObject = createShape(objectShape.SimpleBrickShape);
                brickDrawObject.Id = newBrick.Id;
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
            ballDrawObject.Shape.Width = ball.Size.X;
            ballDrawObject.Shape.Height = ball.Size.Y;

            Canvas.SetLeft(this.ballDrawObject.Shape, ball.Position.X);
            Canvas.SetTop(this.ballDrawObject.Shape, ball.Position.Y);
        }
        #endregion //drawing

        #region events
        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(GameScreen);
            theGame.setPaddlePosition((float)point.Position.X, (float)point.Position.Y);
        }
        private void GameScreen_PointerPressed(object sender, PointerRoutedEventArgs e) {
            theGame.shootBall();
        }

        //Todo: for rescaling feature..
        private void GameScreen_SizeChanged(object sender, SizeChangedEventArgs e) {
            double width = (sender as Canvas).ActualWidth;
            double height = (sender as Canvas).ActualHeight;
            //theGame.SetScreenSize(new Size(width, height));
        }

        private void GameScreen_KeyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Escape) {
                Frame.Navigate(typeof(StartPage), this);
            }
        }

        private void button_home_Click(object sender, RoutedEventArgs e) {
            Frame.Navigate(typeof(StartPage), this);
        }

        private void button_pauze_Click(object sender, RoutedEventArgs e) {
            if(theGame.Status.GameStatus == GameStatus.GameRunningStatus) {
                theGame.pauze();
                button_pauze.Content = "Resume";
            }
            else if(theGame.Status.GameStatus == GameStatus.GamePauzedStatus) {
                theGame.resume();
                button_pauze.Content = "Pauze";
            }
        }
        private void button_restartGame_Click(object sender, RoutedEventArgs e) {
            GameOverWindow.Visibility = Visibility.Collapsed;
            theGame.startScenario();
        }

        #endregion //events


    }
}
