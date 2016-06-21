using System.Numerics;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    public class BaseObject {
        private static long nextId = 0;
        public long Id { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Direction { get; set; }
        public ObjectType ObjectType { get; set; }
        
        public bool Destroyable { get; set; }
        public bool Destroy { get; set; }

        public BaseObject(ObjectType objectType, Vector2 position, Vector2 size, bool destroyable) : this() {
            ObjectType = objectType;
            Position = position;
            Size = size;
            Destroyable = destroyable;
            Direction = new Vector2(0, 0);
            Destroy = false;
        }

        public BaseObject() {
            Id = nextId++;
        }
    }
}