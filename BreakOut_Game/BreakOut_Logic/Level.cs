using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public class Level {
        private int remainingBricks;
        private List<Brick> bricks;

        public List<Brick> Bricks {
            get {
                return bricks;
            }
            set {
            }
        }

        public void Load() {
            throw new System.NotImplementedException();
        }
    }
}