using System;
using System.Numerics;

namespace BreakOut_logic.Objects {
    class SurroundingBox : CollisionObject {

        public SurroundingBox(Vector2 size) : base(ObjectType.SurroundingType,false, Vector2.Zero, size, true) {
        }

        public override bool checkCollision(BaseObject collisionObject) {

            // If inside the box, no collision
            Vector2 resultV = collisionObject.Position - this.Position;
            return (resultV.X < 0 || resultV.X > Size.X ||
                    resultV.Y < 0 || resultV.Y > Size.Y);
        }


        // Return a new angle based on position of impact relative to wall
        public void getNewBallAngle(Ball ball, out bool hasHitFloor) {
            Vector2 resultV = ball.Position - Position;
            hasHitFloor = false;


            // Check axis
            if (resultV.X < 0 || resultV.X > Size.X) {
                // X is out of the box
                ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
            }
            else {
                if(resultV.Y >= Size.Y) {
                    hasHitFloor = true;
                }

                // Y is out of the box
                ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
            }
        }

        // Rotate
        //Math.PI;
        //Vector2 v;
        //var ca = (float)Math.Cos(radians);
        //var sa = (float)Math.Sin(radians);
        //return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
    }
}
