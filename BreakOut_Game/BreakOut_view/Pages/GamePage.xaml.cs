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
        Game theGame;
        Shape paddleShape;
        Shape ballShape;

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
            paddleShape = createShape(objectShape.PaddleShape);
            GameScreen.Children.Add(paddleShape);
            ballShape = createShape(objectShape.BallShape);
            GameScreen.Children.Add(ballShape);
        }

        // If navigated to here, check the reason
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            theGame.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            // When leaving, pauze the game
            theGame.Pauze();
            base.OnNavigatedFrom(e);
        }

        #region drawing
        public void drawPaddle(Paddle paddle) {
            // Sizing
            paddleShape.Width = paddle.Size.X;
            paddleShape.Height = paddle.Size.Y;

            // Positioning
            Canvas.SetLeft(paddleShape, paddle.Position.X);
            Canvas.SetTop(paddleShape, paddle.Position.Y);
        }
        public void drawBricks(List<Brick> bricks) {
            throw new NotImplementedException();
        }
        public void drawBall(Ball ball) {
            // Sizing
            ballShape.Width = ball.Size.X;
            ballShape.Height = ball.Size.Y;

            // Positioning
            Canvas.SetLeft(ballShape, ball.Position.X);
            Canvas.SetTop(ballShape, ball.Position.Y);
        }
        #endregion //drawing

        private void UpdateGUI_Tick(object sender, object e) {
            //theGame.Paddle.Position
        }

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
    }
}
