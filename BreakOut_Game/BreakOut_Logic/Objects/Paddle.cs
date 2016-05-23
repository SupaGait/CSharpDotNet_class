using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Paddle : BaseObject, ICheckCollision {
        private double speed = 0.0;
        private Vector2 userPosition = new Vector2(0, 0);
        public Paddle() : base(0, 0, new Size(150, 30), false) {
            
        }

        public BreakOut_logic.IPaddleFeature[] IPaddleFeature {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public void update(float elapsedTimeMs) {
            // Move the paddle to the location requested by the user
            // Todo
            Position = userPosition;
        }

        public void setUserPosition(float xPostion, float yPosition) {
            userPosition.X = xPostion;
            userPosition.Y = yPosition;
        }

        public bool checkCollision(Size size, Point position) {
            throw new NotImplementedException();
        }

        // Location where the paddle needs to go by user
        private Point requestedPosition = new Point(0, 0);
        public Point RequestedPosition {
            get { return requestedPosition; }
            set { requestedPosition = value; }
        }
    }
}