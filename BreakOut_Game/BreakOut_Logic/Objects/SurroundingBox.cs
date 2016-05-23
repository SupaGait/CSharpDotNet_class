using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    class SurroundingBox : BaseObject, ICheckCollision {

        public SurroundingBox(Size size) : base(0, 0, size, true) {   
        }

        public bool checkCollision(Size size, Point position) {
            throw new NotImplementedException();
        }
    }
}
