using System;
using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class Paddle : CollisionObject {
        static Vector2 defaultSize = new Vector2(150, 30);
        private double speed = 0.0;
        private Vector2 userPosition = new Vector2(0, 0);

        public Paddle(Vector2 gameScreenSize) : 
            base(new Vector2((gameScreenSize.X - defaultSize.X)/2, gameScreenSize.Y - defaultSize.Y), 
                defaultSize, 
                false) {
            // Consturctor
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
            // Todo
            Position = userPosition;
        }

        public void setUserPosition(float xPostion, float yPosition) {
            userPosition.X = xPostion;
            userPosition.Y = yPosition;
        }

        // Check for collision
        public override bool checkCollision(BaseObject collisionObject) {
            // check if the resulting vector from substraction positions falls in the size of the object, which means its a collision
            Vector2 resultV = collisionObject.Position - this.Position;
            return (resultV.X > 0 && 
                    resultV.X < this.Size.X &&
                    resultV.Y > 0 &&
                    resultV.Y < this.Size.Y);
        }

        // Location where the paddle needs to go by user
        private Point requestedPosition = new Point(0, 0);
        public Point RequestedPosition {
            get { return requestedPosition; }
            set { requestedPosition = value; }
        }
    }
}