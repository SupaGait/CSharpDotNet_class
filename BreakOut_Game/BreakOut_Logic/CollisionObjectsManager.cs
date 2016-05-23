using System.Collections.Generic;

namespace BreakOut_logic {
    public class CollisionObjectsManager {

        private List<ICheckCollision> collisionObjects = new List<ICheckCollision>();

        // Add an object which implements the CheckCollision interface
        public void addObject(ICheckCollision collisionObject) {
            collisionObjects.Add(collisionObject);
        }

        public List<BaseObject> checkCollision() {
            List<BaseObject> collisionObjects = new List<BaseObject>();

            return collisionObjects;
        }
    }
}