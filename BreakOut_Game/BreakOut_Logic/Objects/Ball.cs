using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball : CollisionObject {
        private int velocity_PixelSec = 15000;
        private CollisionManager collisionManager;
        private bool ballIsOnPaddle = true;
        private Paddle paddle;
        private Status status;

        public Ball(CollisionManager collisionManager, Paddle paddle, Status status) : base(ObjectType.BallType, true, Vector2.Zero, new Vector2(30, 30), false) {
            this.collisionManager = collisionManager;
            this.paddle = paddle;
            this.status = status;
            Direction = -Vector2.UnitY;
        }

        internal void update(float elapsedTimeMs) {
            if (ballIsOnPaddle) {
                Position = paddle.Position;
                Position += new Vector2(paddle.Size.X/2, -paddle.Size.Y);
            }
            else {
                // Update the position based on the direction
                Position += ((elapsedTimeMs / 1000) * velocity_PixelSec) * Direction;
            }
        }
        public override bool checkCollision(CollisionObject collisionObject) {
            return checkSquareCollision(collisionObject, this);
        }
        public override void isInCollisionWith(CollisionObject collisionObject) {

            // Replace the ball just before collision
            //moveAtImpactPosition(collisionObject);

            // Check for an paddle
            if (collisionObject.ObjectType == ObjectType.PaddleType) {
                (collisionObject as Paddle).getNewBallAngle(this);
            }

            // Check for wall
            if (collisionObject.ObjectType == ObjectType.SurroundingType) {
                bool hasHitFloor;
                (collisionObject as SurroundingBox).getNewBallAngle(this, out hasHitFloor);
                if (hasHitFloor) {
                    status.Balls -= 1;
                    resetBall();
                }
            }
            // Check for brick
            if (collisionObject.ObjectType == ObjectType.BrickType) {
                Brick brick = (collisionObject as Brick);
                brick.getNewBallAngle(this);
                brick.InflictDamage(1);
                status.addScore(brick.getPoints());
            }
        }

        public void launchBall() {
            if (ballIsOnPaddle) { 
                Direction = -Vector2.UnitY; 
                ballIsOnPaddle = false;
            }
        }
        public void resetBall() {
            ballIsOnPaddle = true;
        }


    }
}