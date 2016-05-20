using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.System.Threading;
using Windows.UI.Xaml;

namespace BreakOut_logic {
    public class Game {
        private Paddle paddle = new Paddle();
        private Ball ball = new Ball();
        private Status status = new Status();
        private LevelManager levelManager = new LevelManager();
        private DispatcherTimer timer = new DispatcherTimer();
        private IDrawComponents drawer;

        public Game(IDrawComponents drawer, int updateTime_ms) {
            // Configurate the timer
            timer.Tick += updateGameTimout;
            timer.Interval = new TimeSpan(0, 0, 0, 0, updateTime_ms);
            timer.Start();
            
            // Save the responsible for drawing on screen
            this.drawer = drawer;
        }

        private void updateGameTimout(object sender, object e) {
            // Update

            // Draw
            drawer.drawPaddle(paddle);
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
    }
}