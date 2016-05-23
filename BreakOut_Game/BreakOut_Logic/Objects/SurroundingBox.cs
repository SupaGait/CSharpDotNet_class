using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BreakOut_logic.Objects {
    class SurroundingBox : ICheckCollision {
        private Size size = new Size(150, 30);

        public SurroundingBox(Size size) {
            this.size = size;
        }

        public Size Size {
            get { return size; }
            set { size = value; }
        }

        public bool checkCollision(Size size, Point position) {
            throw new NotImplementedException();
        }
    }
}
