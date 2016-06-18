using BreakOut_logic.Objects;
using System.Collections.Generic;

namespace BreakOut_logic {
    public class CollisionManager {

        private List<CollisionObject> collisionObjects = new List<CollisionObject>();

        // Add an object which implements the CheckCollision interface
        public void addObject(CollisionObject collisionObject) {
            collisionObjects.Add(collisionObject);
        }

        public void removeObject(CollisionObject collisionObject) {
            collisionObjects.Remove(collisionObject);
        }

        // Run through all known collision objects and check for a collision with the given object
        public List<CollisionObject> checkCollision(BaseObject objectToCheck) {
            List<CollisionObject> objectsInCollision = new List<CollisionObject>();

            foreach(CollisionObject collisionObject in collisionObjects) {
                if (collisionObject.checkCollision(objectToCheck)) {
                    objectsInCollision.Add(collisionObject);
                }
            }
            return objectsInCollision;
        }
    }
}