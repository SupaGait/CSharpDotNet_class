using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_logic {
    public interface IDrawComponents {
        void drawPaddle(Paddle paddle);
        void drawBricks(List<Brick> bricks);
    }
}