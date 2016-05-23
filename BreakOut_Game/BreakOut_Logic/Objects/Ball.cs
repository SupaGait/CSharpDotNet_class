using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball {
        private Size size = new Size(30, 30);

        private int velocity = 1;
        private Vector2 position = new Vector2(0, 0);
        private Vector2 direction = new Vector2(0, 0);

        internal void update(double elapsedTimeMs) {
            // Update the position based on the direction
            direction *= 2;
        }

        public Point Position {
            get {
                return new Point(position.X, position.Y);
            }
            set {
                position = new Vector2((float)value.X, (float)value.Y);
            }
        }
        public Size Size {
            get { return size; }
        }


    }
}