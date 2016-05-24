using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BreakOut_view {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateLevelPage : Page {
        private DrawObjects drawObjects;
        private SelectObjects selectObjects;
        private PointerModeAction selectedMode;

        public CreateLevelPage() {
            this.InitializeComponent();

            // Create the objects
            drawObjects = new DrawObjects(GameScreen, debugMessage);
            selectObjects = new SelectObjects(GameScreen, debugMessage);

            // Start in drawMode
            selectedMode = drawObjects;
            selectedMode.register();
        }

        // Set the response of the interface based on the new mode
        private void setMode(PointerMode newMode) {

            // Unrigister the current(old) mode
            selectedMode.unRegister();

            // Select the new mode
            switch (newMode) {
                case PointerMode.DrawMode: {
                        selectedMode = drawObjects;
                        break;
                    }
                case PointerMode.SelectMode: {
                        selectedMode = selectObjects;
                        break;
                    }
                default:  {
                        throw new InvalidOperationException("Selected unavailable operation.");
                    }
            }
            // Rigister the new mode
            selectedMode.register();
        }

        public void debugMessage(String message) {
            textBox_debug.Text = message + "\n";
        }

        private void button_home_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(StartPage), this);
        }

        private void button_draw_Click(object sender, RoutedEventArgs e) {
            setMode(PointerMode.DrawMode);
            debugMessage("Switched to drawMode");
        }

        private void button_select_Click(object sender, RoutedEventArgs e) {
            setMode(PointerMode.SelectMode);
            debugMessage("Switched to selectMode");
        }
    }
}
