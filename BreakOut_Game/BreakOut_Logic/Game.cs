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
        
        // Managers
        public Status Status { get; }
        public LevelManager LevelManager { get; }
        private CollisionManager collisionObjectsManager { get; }

        // Game Objects
        private Paddle paddle;
        private Ball ball;
        private SurroundingBox surroundingBox;
     
        // Logic
        private DispatcherTimer timer;
        private IDrawComponents drawer;

        public Game(IDrawComponents drawer, int updateTime_ms, Size gameScreenSize) {
            // Game logic
            Status = new Status();
            collisionObjectsManager = new CollisionManager();
            LevelManager = new LevelManager(collisionObjectsManager);
            timer = new DispatcherTimer();

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
            float elapsedTimeMs = (float)(sender as DispatcherTimer).Interval.TotalMilliseconds;

            if (Status.GameStatus == GameStatus.GameRunningStatus) {
                // Update    
                ball.update(elapsedTimeMs);
                paddle.update(elapsedTimeMs);
                LevelManager.update();

                // Draw
                drawer.drawPaddle(paddle);
                drawer.drawBall(ball);
                drawer.drawBricks(LevelManager.Level.Bricks);
            }
        }

        public void setPaddlePosition(float xPostion, float yPosition) {
            paddle.setUserPosition(xPostion, yPosition);
        }

        public void Start() {
            Status.newGame();
        }
        public void Pauze() {
            Status.pauzeGame();
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