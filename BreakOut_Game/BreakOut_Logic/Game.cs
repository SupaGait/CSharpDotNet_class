using BreakOut_logic.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Windows.Foundation;
using Windows.System.Threading;
using Windows.UI.Xaml;

namespace BreakOut_logic {
    public class Game {
        // Game Objects
        private Paddle paddle;
        private Ball ball;
        private SurroundingBox surroundingBox;

        // Game logic
        private Status status = new Status();
        private LevelManager levelManager = new LevelManager();
        private CollisionManager collisionObjectsManager = new CollisionManager();
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
            surroundingBox = new SurroundingBox(new Vector2((float)gameScreenSize.Width, (float)gameScreenSize.Height));

            // Create objects
            ball = new Ball(collisionObjectsManager);
            paddle = new Paddle(new Vector2((float)gameScreenSize.Width, (float)gameScreenSize.Height));
            ball.Position = new Vector2((float)gameScreenSize.Width / 2, (float)gameScreenSize.Height / 2);

            // Add default objects to collision manager
            collisionObjectsManager.addObject(surroundingBox);
            collisionObjectsManager.addObject(paddle);
        }

        private void updateGameTimout(object sender, object e) {
            // Update
            float elapsedTimeMs = (float)(sender as DispatcherTimer).Interval.TotalMilliseconds;
            ball.update(elapsedTimeMs);
            paddle.update(elapsedTimeMs);

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

        public CollisionManager CollisionObjectsManager {
            get {
                return collisionObjectsManager;
            }
        }
    }
}