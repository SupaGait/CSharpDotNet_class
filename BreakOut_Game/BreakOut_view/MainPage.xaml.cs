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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BreakOut_view
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IDrawComponents
    {
        Game theGame;
        //DispatcherTimer updateGUI = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            // Configurate GUI
            //updateGUI.Interval = new TimeSpan(0, 0, 0, 0, 20); //50hz
            //updateGUI.Tick += UpdateGUI_Tick;

            // Set the game callbacks for drawing to this
            theGame = new Game(this, 20);
        }
        public void drawPaddle(Paddle paddle) {
            // Clear
            //GameScreen.Children.Remove()

            Shape paddleShape = new Rectangle() {
                Height = paddle.Size.Height,
                Width = paddle.Size.Width,
                Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255 ,0, 0)) };
            
            // Positioning
            Canvas.SetLeft(paddleShape, paddle.Position.X);
            Canvas.SetTop(paddleShape, GameScreen.ActualHeight - paddle.Size.Height);
            GameScreen.Children.Add(paddleShape);
        }
        public void drawBricks(List<Brick> bricks) {
            throw new NotImplementedException();
        }

        private void UpdateGUI_Tick(object sender, object e) {
            //theGame.Paddle.Position
        }

        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(this);
            theGame.Paddle.Position = new Point(point.Position.X, 0);
        }
    }
}
