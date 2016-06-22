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
        public List<CollisionObject> checkCollision(CollisionObject objectToCheck) {
            List<CollisionObject> objectsInCollision = new List<CollisionObject>();

            foreach(CollisionObject collisionObject in collisionObjects) {
                if (collisionObject.checkCollision(objectToCheck)) {
                    objectsInCollision.Add(collisionObject);
                }
            }
            return objectsInCollision;
        }

        public void update() {
            foreach (CollisionObject collisionObjectA in collisionObjects) {
                // For all moving objects, check if they collided against another object
                if (collisionObjectA.IsMoving) {
                    foreach(CollisionObject collisionObjectB in collisionObjects) {
                        // Dont check for itself
                        if (collisionObjectB != collisionObjectA) { 
                            if (collisionObjectB.checkCollision(collisionObjectA)) {
                                // Inform the moving object with the collision
                                collisionObjectA.isInCollisionWith(collisionObjectB);
                            }
                        }
                    }
                }
            }
        }
    }
}