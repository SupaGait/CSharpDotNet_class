using BreakOut_logic.Objects;

namespace BreakOut_logic {
    public interface ICheckCollision {
        bool checkCollision(CollisionObject collisionObject);
    }
}