using System;
using System.Numerics;

namespace BreakOut_logic.Objects {
    class SurroundingBox : CollisionObject {

        public SurroundingBox(Vector2 size) : base(Vector2.Zero, size, true) {
        }

        public override bool checkCollision(BaseObject collisionObject) {
            throw new NotImplementedException();
        }
    }
}
