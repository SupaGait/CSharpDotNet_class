﻿using BreakOut_logic.Objects;
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
        private CollisionManager CollisionObjectsManager { get; }

        // Game Objects
        private Paddle paddle;
        private Ball ball;
        private SurroundingBox surroundingBox { get; }
     
        // Logic
        private DispatcherTimer timer;
        private IDrawComponents drawer;

        public Game(IDrawComponents drawer, int updateTime_ms, Size gameScreenSize) {
            this.drawer = drawer;

            // Game logic
            CollisionObjectsManager = new CollisionManager();
            Status = new Status();
            LevelManager = new LevelManager(CollisionObjectsManager, Status);
            timer = new DispatcherTimer();

            // Configurate the timer
            timer.Tick += updateGameTimout;
            timer.Interval = new TimeSpan(0, 0, 0, 0, updateTime_ms);
            timer.Start();

            // Configurate the game
            surroundingBox = new SurroundingBox(new Vector2((float)gameScreenSize.Width, (float)gameScreenSize.Height));

            // Create objects
            paddle = new Paddle(new Vector2((float)gameScreenSize.Width, (float)gameScreenSize.Height));
            ball = new Ball(CollisionObjectsManager, paddle, Status);
            ball.Position = new Vector2((float)gameScreenSize.Width / 2, (float)gameScreenSize.Height / 2);

            // Add default objects to collision manager
            CollisionObjectsManager.addObject(surroundingBox);
            CollisionObjectsManager.addObject(paddle);
            CollisionObjectsManager.addObject(ball);
        }

        private void updateGameTimout(object sender, object e) {
            float elapsedTimeMs = (float)(sender as DispatcherTimer).Interval.TotalMilliseconds;

            if (Status.GameStatus == GameStatus.GameRunningStatus) {
                // Update objects, positions, animations, whatever
                ball.update(elapsedTimeMs);
                paddle.update(elapsedTimeMs);
                LevelManager.update();

                // Collisions
                CollisionObjectsManager.update();

                // Draw
                drawer.drawPaddle(paddle);
                drawer.drawBall(ball);
                drawer.drawBricks(LevelManager.Level.Bricks);
            }
        }

        public void setPaddlePosition(float xPostion, float yPosition) {
            paddle.setUserPosition(xPostion, yPosition);
        }

        public void start() {
            Status.runTestGame();
        }
        public void startScenario() {
            Status.pauzeGame();
            LevelManager.loadNewScenario();
            Status.runGame();
        }
        public void pauze() {
            Status.pauzeGame();
        }
        public void resume() {
            if(Status.GameStatus == GameStatus.GamePauzedStatus) {
                Status.resumeGame();
            }
        }
        public void shootBall() {
            ball.launchBall();
        }

        public void Reset() {
            throw new System.NotImplementedException();
        }
        public void SetScreenSize(Size size) {
            throw new NotImplementedException();
        }

    }
}