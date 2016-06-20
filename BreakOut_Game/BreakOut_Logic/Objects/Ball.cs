using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball : BaseObject {
        private Vector2 direction;
        private int velocity_PixelSec = 15000;
        private CollisionManager collisionManager;
        private bool ballIsOnPaddle = true;
        private Paddle paddle;
        private Status status;

        public Ball(CollisionManager collisionManager, Paddle paddle, Status status) : base(ObjectType.BallType, Vector2.Zero, new Vector2(30, 30), false) {
            this.collisionManager = collisionManager;
            this.paddle = paddle;
            this.status = status;
            direction = Vector2.UnitY;
        }

        internal void update(float elapsedTimeMs) {
            if (ballIsOnPaddle) {
                Position = paddle.Position;
                Position += new Vector2(paddle.Size.X/2, -paddle.Size.Y);
            }
            else {
                // Update the position based on the direction
                Position += ((elapsedTimeMs / 1000) * velocity_PixelSec) * direction;

                // Check for collision against any object
                var collisionObjects = collisionManager.checkCollision(this);

                // Take the first object
                if (collisionObjects.Count > 0) {
                    foreach (CollisionObject collisioObject in collisionObjects) {
                        // Check for an paddle
                        if (collisioObject.ObjectType == ObjectType.PaddleType) {
                            (collisioObject as Paddle).getNewBallAngle(this);
                        }
                        // Check for wall
                        if (collisioObject.ObjectType == ObjectType.SurroundingType) {
                            bool hasHitFloor;
                            (collisioObject as SurroundingBox).getNewBallAngle(this, out hasHitFloor);
                            if (hasHitFloor) {
                                status.Balls -= 1;
                                resetBall();
                            }
                        }
                        // Check for brick
                        if (collisioObject.ObjectType == ObjectType.BrickType) {
                            Brick brick = (collisioObject as Brick);
                            brick.getNewBallAngle(this);
                            brick.InflictDamage(1);
                            status.addScore(brick.getPoints());
                        }
                    }
                }
            }
        }

        public void launchBall() {
            direction = Vector2.UnitY; 
            ballIsOnPaddle = false;
        }

        public void resetBall() {
            ballIsOnPaddle = true;
        }

        public Vector2 Direction {
            get { return direction; }
            set { direction = value; }
        }
    }
}