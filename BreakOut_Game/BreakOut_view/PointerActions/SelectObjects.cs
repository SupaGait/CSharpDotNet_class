using BreakOut_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace BreakOut_view {
    public class SelectObjects : PointerModeAction {
        private Canvas gameScreen;
        private PrintDebugMessage debugMessage;
        private Level level;

        // Save the screen for drawing and the delegate to print messages
        public SelectObjects(Level level, Canvas gameScreen, PrintDebugMessage debugMessage){
            this.level = level;
            this.gameScreen = gameScreen;
            this.debugMessage = debugMessage;
        }

        public override void register() {
            throw new NotImplementedException();
        }

        public override void unRegister() {
            throw new NotImplementedException();
        }

        public override PointerMode getType() {
            return PointerMode.SelectMode;
        }
    }
}