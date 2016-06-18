using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Ball : BaseObject {
        private Vector2 direction;
        private int velocity_PixelSec = 15000;
        private CollisionManager collisionManager;

        public Ball(CollisionManager collisionManager) : base(ObjectType.BallType, Vector2.Zero, new Vector2(30, 30), false) {
            // Todo input position and direction based on paddle?
            this.direction = Vector2.Normalize(new Vector2(0, 1));
            this.collisionManager = collisionManager;
        }

        internal void update(float elapsedTimeMs) {
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

        public Vector2 Direction {
            get { return direction; }
            set { direction = value; }
        }
    }
}