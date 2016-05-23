using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball : BaseObject {
        private Vector2 direction;
        private int velocity_PixelSec = 100;

        public Ball() : base(0,0, new Size(30, 30), false) {
            // Todo input position and direction based on paddle?
            direction = Vector2.Normalize(new Vector2(0, 1));
        }

        internal void update(float elapsedTimeMs) {
            // Update the position based on the direction
            Position += ((elapsedTimeMs / 1000) * velocity_PixelSec) * direction;

            // Check for collision against any object
        }
    }
}