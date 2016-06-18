using BreakOut_logic.Objects;
using System.Collections.Generic;

namespace BreakOut_logic {
    public class Wall {
        private int remainingBricks;
        private List<BaseObject> BaseObjects { get; set; }
        public List<Brick> Bricks { get; set;}

        public Wall() {
            Bricks = new List<Brick>();
            BaseObjects = new List<BaseObject>();
        }

        public void addObject(BaseObject newObject) {
            BaseObjects.Add(newObject);
        }

        public void addBrick(Brick newBrick) {
            Bricks.Add(newBrick);
        }

        public void Load() {
            throw new System.NotImplementedException();
        }
    }
}