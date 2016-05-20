using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Windows.Foundation;

namespace BreakOut_logic {
    public class Paddle {
        private Point position = new Point(0, 0);
        private Size size = new Size(150, 30);
        private double speed = 0.0;

        public BreakOut_logic.IPaddleFeature[] IPaddleFeature {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public void Update(double elapsedTime) {
            // Move the paddle to the location requested by the user
            // Todo
            position = requestedPosition;

        }

        // Location where the paddle needs to go by user
        private Point requestedPosition = new Point(0, 0);
        public Point RequestedPosition {
            get { return requestedPosition; }
            set { requestedPosition = value; }
        }

        public Point Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }
        public Size Size {
            get { return size; }
        }

    }
}