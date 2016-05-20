using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Foundation;

namespace BreakOut_logic {
    public interface ICheckCollision {
        bool checkCollision(Size size, Point position);
    }
}