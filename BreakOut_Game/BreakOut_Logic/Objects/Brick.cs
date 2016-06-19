using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Brick : CollisionObject {
        private int Health;

        public Brick(Vector2 position, Vector2 size, bool destroyable) : 
            base(ObjectType.BrickType, position, size, destroyable) {
        }
        public Brick(){
        }

        public override bool checkCollision(BaseObject collisionObject) {
            return checkSquareCollision(collisionObject, this);
        }
        public void InflictDamage(int dmg) {
            Health -= dmg;
            if(Health < 0) {
                Destroy = true;
            }
        }
        // Return a new angle based on position of impact
        public void getNewBallAngle(Ball ball) {
            Vector2 resultV = ball.Position - this.Position;

            Vector2 centerPos = this.Position + this.Size/2;

            // From top/bottom
            ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);


            // From left/right
            /*
            // Check axis
            if (resultV.X < 0 || resultV.X > this.Size.X) {
                // X is out of the box
                ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
            }
            else {
                // Y is out of the box
                ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
            }*/
        }

        internal int getPoints() {
            return 1;
        }
    }
}