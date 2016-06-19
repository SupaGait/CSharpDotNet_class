using BreakOut_logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BreakOut_view {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateLevelPage : Page {
        private DrawObjects drawObjects;
        private SelectObjects selectObjects;
        private PointerModeAction selectedMode;
        private List<Line> grid = new List<Line>();

        public Wall Level { get; set; }
        public int GridSize { get; set; }
        public int HalfGridSize { get; set; }

        public CreateLevelPage() {
            this.InitializeComponent();
            
            // Create the objects
            Level = new Wall();
            drawObjects = new DrawObjects(this, GameScreen, gamePointer, debugMessage);
            selectObjects = new SelectObjects(this, GameScreen, debugMessage);

            // Start in drawMode
            selectedMode = drawObjects;
            selectedMode.register();

            GridSize = (int)slider_gridSize.Value;
            HalfGridSize = GridSize / 2;
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
                default: {
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

        private void button_testLevel_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(GamePage), Level);
        }

        private async void button_saveLevel_Click(object sender, RoutedEventArgs e) {
            await LevelLoader.save(Level, textBox_levelName.Text+".xml");
            debugMessage("Level saved.");
        }

        private async void button_loadLevel_Click(object sender, RoutedEventArgs e) {
            // Load the lelel
            Level = await LevelLoader.load(textBox_levelName.Text + ".xml");

            // Draw the loaded level
            foreach (var brick in Level.Bricks) {
                var drawObject = drawObjects.createAndAddObject(
                    Shapes.ShapeFactory.objectShape.SimpleBrickShape,
                    brick.Size.X, brick.Size.Y,
                    brick.Position.X, brick.Position.Y);
                drawObject.Id = brick.Id;
            }

            debugMessage("Level loaded.");
            //debugMessage(ApplicationData.Current.LocalFolder.Path);
        }

        private void slider_gridSize_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) {
            GridSize = (int)slider_gridSize.Value;
            createGrid();
        }

        private void createGrid() {
            // Destroy old grid
            foreach (var gridLine in grid) {
                GameScreen.Children.Remove(gridLine);
            }
            grid.Clear();

            // Create new grid
            // X
            int screenHeight = (int)GameScreen.ActualHeight;
            for (int xPos = 0; xPos < GameScreen.ActualWidth; xPos += GridSize) {
                var verticalLine = new Line();
                verticalLine.X1 = xPos;
                verticalLine.Y1 = 0;
                verticalLine.X2 = xPos;
                verticalLine.Y2 = screenHeight;
                verticalLine.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                verticalLine.StrokeThickness = 1;

                grid.Add(verticalLine);
                GameScreen.Children.Add(verticalLine);
            }
            // Y
            int screenWidth = (int)GameScreen.ActualWidth;
            for (int yPos = 0; yPos < GameScreen.ActualHeight; yPos += GridSize) {
                var horizontalLine = new Line();
                horizontalLine.X1 = 0;
                horizontalLine.Y1 = yPos;
                horizontalLine.X2 = screenWidth;
                horizontalLine.Y2 = yPos;
                horizontalLine.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                horizontalLine.StrokeThickness = 1;

                grid.Add(horizontalLine);
                GameScreen.Children.Add(horizontalLine);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            createGrid();
        }
    }
}