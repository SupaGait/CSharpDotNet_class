using BreakOut_logic.Objects;
using System.Collections.Generic;
using System;

namespace BreakOut_logic {
    public class Wall {
        private int remainingBricks = 0;
        private List<BaseObject> BaseObjects { get; set; }
        public List<Brick> Bricks { get; set;}
        public CollisionManager CollisionManager { get; set; }

        public Wall() {
            Bricks = new List<Brick>();
            BaseObjects = new List<BaseObject>();
        }

        public void addObject(BaseObject newObject) {
            BaseObjects.Add(newObject);
        }

        public void addBrick(Brick newBrick) {
            remainingBricks++;
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

        public void Load() {
            throw new System.NotImplementedException();
        }

        public void update() {
            // Remove all bricks marked for destroy
            var destroyBricks = Bricks.FindAll(brick => brick.Destroy == true);
            remainingBricks -= destroyBricks.Count;
            foreach (var brick in destroyBricks) {
                CollisionManager.removeObject(brick);
                Bricks.Remove(brick);
            }
        }
    }
}