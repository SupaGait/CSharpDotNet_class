using BreakOut_logic.Objects;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BreakOut_logic {
    public class Wall {
        [XmlIgnore]
        public int RemainingBricks { get; set; }

        [XmlIgnore]
        private List<BaseObject> BaseObjects { get; set; }

        public List<Brick> Bricks { get; set;}

        [XmlIgnore]
        public CollisionManager CollisionManager { get; set; }

        public Wall() {
            Bricks = new List<Brick>();
            BaseObjects = new List<BaseObject>();
        }

        public void addObject(BaseObject newObject) {
            BaseObjects.Add(newObject);
        }

        public void addBrick(Brick newBrick) {
            RemainingBricks++;
            Bricks.Add(newBrick);
        }

        internal void fillWall(CollisionManager collisionObjectsManager) {
            CollisionManager = collisionObjectsManager;
            foreach (var item in Bricks) {
                CollisionManager.addObject(item);
            }
        }
        public void clearWall() {
            foreach (var brick in Bricks) {
                CollisionManager.removeObject(brick);
            }
        }

        public void update() {
            // Remove all bricks marked for destroy
            var destroyBricks = Bricks.FindAll(brick => brick.Destroy == true);
            RemainingBricks -= destroyBricks.Count;
            foreach (var brick in destroyBricks) {
                CollisionManager.removeObject(brick);
                Bricks.Remove(brick);
            }
        }
    }
}