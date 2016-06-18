using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball : BaseObject {
        private Vector2 direction;
        private int velocity_PixelSec = 15000;
        private CollisionManager collisionManager;
        private bool ballIsOnPaddle = true;
        private Paddle paddle;
        private CollisionManager collisionObjectsManager;

        public Ball(Paddle paddle, CollisionManager collisionManager) : base(ObjectType.BallType, Vector2.Zero, new Vector2(30, 30), false) {
            // Todo input position and direction based on paddle?
            this.direction = Vector2.UnitY;
            this.paddle = paddle;
            this.collisionManager = collisionManager;
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
                            (collisioObject as SurroundingBox).getNewBallAngle(this);
                        }
                        // Check for brick
                        if (collisioObject.ObjectType == ObjectType.BrickType) {
                            (collisioObject as Brick).getNewBallAngle(this);
                            (collisioObject as Brick).InflictDamage(1);
                        }
                    }
                }
            }
        }

        public void launchBall() {
            this.direction = Vector2.UnitY; 
            ballIsOnPaddle = false;
        }

        public Vector2 Direction {
            get { return direction; }
            set { direction = value; }
        }
    }
}