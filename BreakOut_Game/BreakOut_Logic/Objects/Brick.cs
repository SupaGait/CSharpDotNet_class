using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Brick : CollisionObject {
        private int Health;

        public Brick(Vector2 position, Vector2 size, bool destroyable) : 
            base(ObjectType.BrickType,false, position, size, destroyable) {
        }
        public Brick(){}

        public override bool checkCollision(CollisionObject collisionObject) {
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
            Vector2 centerBallpos = ball.Position + ball.Size/2;
            if (centerBallpos.Y > Position.Y &&
                centerBallpos.Y < Position.Y + Size.Y) {
                
                // From sides - inverse the x direction
                ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
            }
            else {
                // From top/bottom - inverse the Y direction
                ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
            }
        }

        internal int getPoints() {
            return 1;
        }
    }
}