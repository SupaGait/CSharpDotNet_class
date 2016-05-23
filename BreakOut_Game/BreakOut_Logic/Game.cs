using BreakOut_logic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Xaml;

namespace BreakOut_logic {
    public class Game {
        // Game Objects
        private Paddle paddle = new Paddle();
        private Ball ball = new Ball();
        private SurroundingBox surroundingBox;

        // Game logic
        private Status status = new Status();
        private LevelManager levelManager = new LevelManager();
        private DispatcherTimer timer = new DispatcherTimer();
        private IDrawComponents drawer;

        public Game(IDrawComponents drawer, int updateTime_ms, Size gameScreenSize) {
            // Configurate the timer
            timer.Tick += updateGameTimout;
            timer.Interval = new TimeSpan(0, 0, 0, 0, updateTime_ms);
            timer.Start();
            
            // Save the responsible for drawing on screen
            this.drawer = drawer;

            // Configurate the game
            surroundingBox = new SurroundingBox(gameScreenSize);
            ball.Position = new Point(gameScreenSize.Width / 2, gameScreenSize.Height / 2);
        }

        private void updateGameTimout(object sender, object e) {
            // Update
            double elapsedTimeMs = (sender as DispatcherTimer).Interval.TotalMilliseconds;
            //ball.update(elapsedTimeMs);

            // Draw
            drawer.drawPaddle(paddle);
            drawer.drawBall(ball);
            //drawer.drawBricks(levelManager.Level.Bricks);
        }

        public Paddle Paddle {
            get {
                return paddle;
            }
        }
        public Ball Ball {
            get {
                return ball;
            }
        }

        public Status Status {
            get {
                return status;
            }
        }

        public LevelManager LevelManager {
            get {
                return levelManager;
            }
        }

        public void Start() {
            throw new System.NotImplementedException();
        }

        public void Pauze() {
            throw new System.NotImplementedException();
        }

        public void Reset() {
            throw new System.NotImplementedException();
        }

        internal SurroundingBox SurroundingBox {
            get {
                return surroundingBox;
            }
        }
        public void SetScreenSize(Size size) {
            throw new NotImplementedException();
        }
    }
}