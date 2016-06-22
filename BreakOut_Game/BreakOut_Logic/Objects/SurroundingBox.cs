using System;
using System.Numerics;

namespace BreakOut_logic.Objects {
    class SurroundingBox : CollisionObject {

        public SurroundingBox(Vector2 size) : base(ObjectType.SurroundingType,false, Vector2.Zero, size, true) {
        }

        public override bool checkCollision(CollisionObject collisionObject) {
            // If inside the box, no collision
            Vector2 resultV = collisionObject.Position - this.Position;
            return (resultV.X < 0 || resultV.X + collisionObject.Size.X > Size.X ||
                    resultV.Y < 0 || resultV.Y + collisionObject.Size.Y > Size.Y);
        }


        // Return a new angle based on position of impact relative to wall
        public void getNewBallAngle(Ball ball, out bool hasHitFloor) {
            Vector2 resultV = ball.Position - Position;
            hasHitFloor = false;

            // Check X axis
            if (resultV.X + ball.Size.X >= Size.X) {
                // Right, only swap if going the wrong way
                if (ball.Direction.X > 0) {
                    ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
                }
            }
            else if(resultV.X <= 0) {
                // Left, only swap if going the wrong way
                if (ball.Direction.X < 0) {
                    ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
                }
            }
            // Check Y axis
            if (resultV.Y + ball.Size.Y >= Size.Y) {
                // below, only swap if going the wrong way
                hasHitFloor = true;
                if (ball.Direction.Y > 0) {
                    ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
                }
            }
            else if(resultV.Y <= 0 ) {
                // Above, only swap if going the wrong way
                if (ball.Direction.Y < 0) {
                    ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
                }
            }
        }
    }
}
