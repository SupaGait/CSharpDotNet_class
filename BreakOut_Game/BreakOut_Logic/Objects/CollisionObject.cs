using System.Numerics;

namespace BreakOut_logic.Objects {

    // Abstract class which can be used for objects that have collision check.
    public abstract class CollisionObject : BaseObject, ICheckCollision {
        public CollisionObject(ObjectType objectType, Vector2 position, Vector2 size, bool destroyable) : 
            base(objectType, position, size, destroyable) {
        }
        // Objects need to implement a collition check interface
        public abstract bool checkCollision(BaseObject collisionObject);
    }
}