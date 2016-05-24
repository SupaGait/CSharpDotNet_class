using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class BaseObject {
        private static long nextId = 0;
        private long id;

        private Vector2 position;
        private Vector2 size;
        private Vector2 direction;
        private ObjectType objectType;
        
        private bool destroyable;
        private bool visable = true;
        private bool markedForDestroy = false;

        public BaseObject(ObjectType objectType, Vector2 position, Vector2 size, bool destroyable) {
            this.objectType = objectType;
            this.position = position;
            this.size = size;
            this.destroyable = destroyable;
            direction = new Vector2(0, 0);

            // Set the Id
            this.id = nextId;
            nextId++;
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
        public ObjectType ObjectType {
            get { return objectType; }
        }
        public long GetId {
            get { return id; }
        }
    }
}