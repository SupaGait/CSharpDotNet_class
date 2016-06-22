using System.Numerics;

namespace BreakOut_logic.Objects {

    // Abstract class which can be used for objects that have collision check.
    public abstract class CollisionObject : BaseObject, ICheckCollision {
        public bool IsMoving { get; }

        public CollisionObject(ObjectType objectType, bool isMoving, Vector2 position, Vector2 size, bool destroyable) :
            base(objectType, position, size, destroyable) {
            IsMoving = isMoving;
        }
        public CollisionObject() { }

        // Objects need to implement a collition check interface
        public abstract bool checkCollision(CollisionObject collisionObject);

        // Object is informed if there is a collision
        public virtual void isInCollisionWith(CollisionObject collisionObject) {
            // Normally do nothing
        }

        // Helper methods
        static public bool checkSquareCollision(BaseObject objectA, BaseObject objectB) {
            // check if the resulting vector from substraction positions falls in the size of the object, which means its a collision

            Vector2 objectALowerRight = objectA.Position + objectA.Size;
            Vector2 objectBLowerRight = objectB.Position + objectB.Size;
            bool verticalInside = false;
            bool horizontalInside = false;

            // Check vertical
            if(objectA.Position.Y > objectB.Position.Y) {
                // A below B
                if(objectA.Position.Y < objectBLowerRight.Y) {
                    // Overlap
                    verticalInside = true;
                }
            }
            else {
                // A above B
                if (objectALowerRight.Y > objectB.Position.Y) {
                    // Overlap
                    verticalInside = true;
                }
            }

            // Check horizontal
            if (objectA.Position.X > objectB.Position.X) {
                // A below B
                if (objectA.Position.X < objectBLowerRight.X) {
                    // Overlap
                    horizontalInside = true;
                }
            }
            else {
                // A above B
                if (objectALowerRight.X > objectB.Position.X) {
                    // Overlap
                    horizontalInside = true;
                }
            }

            /*
            // Check Upper left corner of A in B
            Vector2 upperLeftAinB = objectA.Position - objectB.Position;
            bool upperInside = upperLeftAinB.X > 0 && upperLeftAinB.X < objectB.Size.X && upperLeftAinB.Y > 0 && upperLeftAinB.Y < objectB.Size.Y;

            // Check lower right corner of A in B
            Vector2 lowerRightAInB = objectA.Position - objectB.Position + objectA.Size;
            bool lowerInside = lowerRightAInB.X > 0 && lowerRightAInB.X < objectB.Size.X && lowerRightAInB.Y > 0 && lowerRightAInB.Y < objectB.Size.Y;
            
            return (upperInside || lowerInside);
            */
            return (verticalInside && horizontalInside);
        }

        public void moveAtImpactPosition(CollisionObject collisionObject) {
            // Iterate back until the ball is out of this object
            //Vector2 direction = Vector2.Normalize(Direction);
            while (collisionObject.checkCollision(this)) {
                Position -= Direction;
            }
        }
    }
}