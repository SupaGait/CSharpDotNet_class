using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic {
    public class BaseObject {
        private Vector2 direction;
        private Vector2 position;
        private bool destroyable;

        private Size size = new Size(30, 30);
        private bool visable = true;
        private bool markedForDestroy = false;
        
        public BaseObject(float xPostion, float yPosition, Size size, bool destroyable) {
            this.position = new Vector2(xPostion, yPosition);
            this.size = size;
            this.destroyable = destroyable;
            direction = new Vector2(0, 0);
        }

        public Size Size {
            get { return size; }
            set { size = value; }
        }
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }
        public bool Destroy {
            set { markedForDestroy = value; }
        }
    }
}