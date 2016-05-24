using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakOut_view {
    public abstract class PointerModeAction : IRegisterEvents {
        public abstract void register();
        public abstract void unRegister();
        public abstract PointerMode getType();
    }
}