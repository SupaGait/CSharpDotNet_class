using BreakOut_logic.Objects;
using System.Collections.Generic;

namespace BreakOut_logic {
    public interface IDrawComponents {
        void drawPaddle(Paddle paddle);
        void drawBricks(List<Brick> bricks);
        void drawBall(Ball ball);
    }
}