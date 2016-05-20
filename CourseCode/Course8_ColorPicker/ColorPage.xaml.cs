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

namespace Course8_ColorPicker {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorPage : Page {

        private Windows.UI.Color selectedColor;
        private MainPage mainPageRef;

        public ColorPage() {
            this.InitializeComponent();
        }

        private Windows.UI.Color getColorFromPosition(PointerRoutedEventArgs e) {
            Windows.UI.Input.PointerPoint point = e.GetCurrentPoint(this);

            // Show the position
            posX_textBox.Text = point.Position.X.ToString();
            posY_textBox.Text = point.Position.Y.ToString();

            // Create a new color based on position
            Windows.UI.Color color = new Windows.UI.Color();
            color.R = (Byte)((255 / this.Frame.ActualWidth) * point.Position.X);
            color.G = (Byte)((255 / this.Frame.ActualHeight) * point.Position.Y);
            color.B = 128;
            color.A = 255;

            return color;
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e) {
            
            // Set background based on position
            this.gridFrame.Background = new SolidColorBrush(getColorFromPosition(e));
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e) {
            
            // Set button and selected color based on clicked position
            selectedColor = getColorFromPosition(e);
            select_button.Background = new SolidColorBrush(selectedColor);
        }

        private void select_button_Click(object sender, RoutedEventArgs e) {
            // Set color
            if (mainPageRef != null) { 
                mainPageRef.selectedColor = selectedColor;
            }
            //mainFrameColor = selectedColor;
            this.Frame.GoBack();
        }
        /*
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            // Get the reference to the color
            if (e.Parameter is MainPage) {
                mainPageRef = (MainPage)e.Parameter;
            }
            base.OnNavigatedTo(e);
        }*/

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            (e.Content as MainPage).selectedColor = selectedColor;
            base.OnNavigatedFrom(e);
        }
    }
}
