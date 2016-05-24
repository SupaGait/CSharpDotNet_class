using BreakOut_logic.Objects;
using System.Collections.Generic;

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

        public void addObject(BaseObject newObject) {

        }

        public void Load() {
            throw new System.NotImplementedException();
        }
    }
}