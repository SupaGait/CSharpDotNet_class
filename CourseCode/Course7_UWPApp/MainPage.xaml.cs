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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Course7_UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static int MinimalClicksAsec = 10;

        private string gameWord = "Spam!. ";
        private int objectCount = 0;
        private DispatcherTimer autoRemoveTimer;
        

        public MainPage()
        {
            // Create the timer for clearing the clicks
            autoRemoveTimer = new DispatcherTimer();
            autoRemoveTimer.Interval = new TimeSpan(0, 0, 1);
            autoRemoveTimer.Tick += AutoRemoveTimer_Tick;
            autoRemoveTimer.Start();
            this.InitializeComponent();

            main_textBlock.Text = "Click the button below to start the game";
        }

        private void AutoRemoveTimer_Tick(object sender, object e) {
            // Remove the text and count
            if (main_textBlock.Text.Contains(gameWord)) {
                main_textBlock.Text = "";
                objectCount = 0;
            }
        }

        private void clickMe_button_Click(object sender, RoutedEventArgs e) {

            // Start the timer
            if (!autoRemoveTimer.IsEnabled) {
                autoRemoveTimer.Start();
                main_textBlock.Text = "";
            }

            // Increase count on click
            main_textBlock.Text += gameWord;
            objectCount += 1;

            // Check if the goal is reached
            if(objectCount >= MinimalClicksAsec) {
                autoRemoveTimer.Stop();
                main_textBlock.Text = "Wow!! You are great!!!, over 10 clicks a sec... whooo.";
            }
        }
    }
}
