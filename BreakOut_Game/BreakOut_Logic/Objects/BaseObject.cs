using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class BaseObject {
        private Vector2 position;
        private Vector2 size;
        private Vector2 direction;

        private bool destroyable;
        private bool visable = true;
        private bool markedForDestroy = false;
        private float xPostion;
        private float yPosition;
        private Size size1;

        public BaseObject(Vector2 position, Vector2 size, bool destroyable) {
            this.position = position;
            this.size = size;
            this.destroyable = destroyable;
            direction = new Vector2(0, 0);
        }

        public BaseObject(float xPostion, float yPosition, Size size1, bool destroyable) {
            this.xPostion = xPostion;
            this.yPosition = yPosition;
            this.size1 = size1;
            this.destroyable = destroyable;
        }

        public Vector2 Size {
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