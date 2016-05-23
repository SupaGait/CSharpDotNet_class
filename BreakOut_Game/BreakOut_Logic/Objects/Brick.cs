using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Brick : CollisionObject {
        private int Health;

        public Brick(Vector2 position, Vector2 size, bool destroyable) : 
            base(ObjectType.BrickType, position, size, destroyable) {
        }
        public override bool checkCollision(BaseObject collisionObject) {
            throw new NotImplementedException();
        }
    }
}