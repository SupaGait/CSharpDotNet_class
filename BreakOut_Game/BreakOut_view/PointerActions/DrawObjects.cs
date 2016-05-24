using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BreakOut_view {
    public class DrawObjects : PointerModeAction {
        private bool pressed = false;
        private Canvas gameScreen;
        private PrintDebugMessage debugMessage;

        // Save the screen for drawing and the delegate to print messages
        public DrawObjects(Canvas gameScreen, PrintDebugMessage debugMessage){
            this.gameScreen = gameScreen;
            this.debugMessage = debugMessage;
        }

        #region registration
        // Register all the necessary events
        public override void register() {
            gameScreen.PointerMoved += GameScreen_PointerMoved;
            gameScreen.PointerPressed += GameScreen_PointerPressed;
            gameScreen.PointerReleased += GameScreen_PointerReleased;
        }
        // Unregister all the events
        public override void unRegister() {
            gameScreen.PointerMoved -= GameScreen_PointerMoved;
            gameScreen.PointerPressed -= GameScreen_PointerPressed;
            gameScreen.PointerReleased -= GameScreen_PointerReleased;
        }
        #endregion // registration

        #region events
        private void GameScreen_PointerPressed(object sender, PointerRoutedEventArgs e) {
            debugMessage("Pressed!");
        }
        private void GameScreen_PointerReleased(object sender, PointerRoutedEventArgs e) {
            debugMessage("Released");
        }
        private void GameScreen_PointerMoved(object sender, PointerRoutedEventArgs e) {
            debugMessage("Moved");
        }
        #endregion // events

        public override PointerMode getType() {
            return PointerMode.DrawMode;
        }
    }
}