using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;

namespace BreakOut_logic {
    public class Brick : ICheckCollision {
        private Point Position;
        private int Health;
        private Point Size;

        public bool checkCollision(Size size, Point position) {
            throw new NotImplementedException();
        }
    }
}