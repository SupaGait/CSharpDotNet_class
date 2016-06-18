using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Paddle : CollisionObject {
        static Vector2 defaultSize = new Vector2(150, 30);
        private double speed = 0.0;
        private Vector2 userPosition = new Vector2(0, 0);
        private Vector2 gameScreenSize;
        private float maxXposition = 0;

        public Paddle(Vector2 gameScreenSize) : 
            base(ObjectType.PaddleType, new Vector2((gameScreenSize.X - defaultSize.X)/2, gameScreenSize.Y - defaultSize.Y), 
                defaultSize, false) {
            this.gameScreenSize = gameScreenSize;
            this.maxXposition = gameScreenSize.X - this.Size.X;
        }

        public BreakOut_logic.IPaddleFeature[] IPaddleFeature {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public void update(float elapsedTimeMs) {
            // Move the paddle to the location requested by the user

            // Center the paddle under the position
            Position = new Vector2(userPosition.X - this.Size.Y/2, Position.Y);
            // Apply clipping
            if (Position.X < 0) {
                Position = new Vector2(0, Position.Y);
            }
            else if(Position.X > maxXposition) {
                Position = new Vector2(maxXposition, Position.Y);
            }
        }

        public void setUserPosition(float xPostion, float yPosition) {
            userPosition.X = xPostion;
            userPosition.Y = yPosition;
        }

        // Check for collision
        public override bool checkCollision(BaseObject collisionObject) {
            return checkSquareCollision(collisionObject, this);
        }

        // Return a new angle based on position of impact relative to paddle
        public void getNewBallAngle(Ball ball) {
            float paddleXCenter = Position.X + Size.X/2;
            float impactXDiff = ball.Position.X - paddleXCenter;
            float outputX = (impactXDiff == 0 ? 0 : impactXDiff / Size.X * (float)Math.PI);

            // Manually give a new direction
            ball.Direction = new Vector2(outputX, -1); // Always Up
        }

        // Location where the paddle needs to go by user
        private Point requestedPosition = new Point(0, 0);
        public Point RequestedPosition {
            get { return requestedPosition; }
            set { requestedPosition = value; }
        }
    }
}