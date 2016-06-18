using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class BaseObject {
        private static long nextId = 0;
        public long Id { get; set; }

        private Vector2 position;
        private Vector2 size;
        private Vector2 direction;
        private ObjectType objectType;
        
        private bool destroyable;
        private bool visable = true;
        public bool Destroy { get; set; }

        public BaseObject(ObjectType objectType, Vector2 position, Vector2 size, bool destroyable) : this() {
            this.objectType = objectType;
            this.position = position;
            this.size = size;
            this.destroyable = destroyable;
            direction = new Vector2(0, 0);
            Destroy = false;
        }
        public BaseObject() {
            // Set the Id
            Id = nextId;
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
        public ObjectType ObjectType {
            get { return objectType; }
        }
    }
}