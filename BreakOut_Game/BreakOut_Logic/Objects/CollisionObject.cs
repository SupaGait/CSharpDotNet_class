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
        public abstract bool checkCollision(BaseObject collisionObject);

        // Object is informed if there is a collision
        public virtual void isInCollisionWith(BaseObject collisionObject) {
            // Normally do nothing
        }

        // Helper methods
        static public bool checkSquareCollision(BaseObject objectA, BaseObject objectB) {
            // check if the resulting vector from substraction positions falls in the size of the object, which means its a collision

            // Check Upper left corner of A in B
            Vector2 upperLeftAinB = objectA.Position - objectB.Position;
            bool upperInside = upperLeftAinB.X > 0 && upperLeftAinB.X < objectB.Size.X && upperLeftAinB.Y > 0 && upperLeftAinB.Y < objectB.Size.Y;

            // Check lower right corner of A in B
            Vector2 lowerRightAInB = objectA.Position - objectB.Position + objectA.Size;
            bool lowerInside = lowerRightAInB.X > 0 && lowerRightAInB.X < objectB.Size.X && lowerRightAInB.Y > 0 && lowerRightAInB.Y < objectB.Size.Y;

            return (upperInside || lowerInside);
        }

        public void moveAtImpactPosition(BaseObject collisionObject) {
            // Iterate back until the ball is out of this object
            Vector2 direction = Vector2.Normalize(Direction);
            while (checkCollision(collisionObject)) {
                Position -= direction;
            }
        }
    }
}