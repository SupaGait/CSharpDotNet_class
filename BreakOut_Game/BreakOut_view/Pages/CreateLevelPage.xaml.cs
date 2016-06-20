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
using Windows.UI.Popups;
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
        public List<DrawableObject> levelObjects { get; set; }
        public Button deleteButton { get; }

        public CreateLevelPage() {
            this.InitializeComponent();
            
            // Create the objects
            Level = new Wall();
            drawObjects = new DrawObjects(this, GameScreen, debugMessage);
            selectObjects = new SelectObjects(this, GameScreen, debugMessage);
            levelObjects = new List<DrawableObject>();

            // Start in drawMode
            selectedMode = drawObjects;
            button_draw.BorderThickness = new Thickness(1);
            button_select.BorderThickness = new Thickness(0);
            selectedMode.register();

            GridSize = (int)slider_gridSize.Value;
            HalfGridSize = GridSize / 2;

            deleteButton = button_deleteObject;
        }

        // Set the response of the interface based on the new mode
        private void setMode(PointerMode newMode) {

            // Unrigister the current(old) mode
            selectedMode.unRegister();

            // Select the new mode
            switch (newMode) {
                case PointerMode.DrawMode: {
                        selectedMode = drawObjects;
                        button_draw.BorderThickness = new Thickness(2);
                        button_select.BorderThickness = new Thickness(0);
                        break;
                    }
                case PointerMode.SelectMode: {
                        selectedMode = selectObjects;
                        button_draw.BorderThickness = new Thickness(0);
                        button_select.BorderThickness = new Thickness(2);
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
            string levelName = textBox_levelName.Text + ".xml";

            var dialogSave = new MessageDialog("Save as: " + levelName + "? \n\nSaved in:" + ApplicationData.Current.LocalFolder.Path);
            dialogSave.Title = "Save level";
            dialogSave.Commands.Add(new UICommand { Label = "Save", Id = 0 });
            dialogSave.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var result = await dialogSave.ShowAsync();

            // Save
            if ((int)result.Id == 0) {

                await LevelLoader.save(Level, textBox_levelName.Text + ".xml");
                debugMessage("Level saved.");
            }
        }

        private void clearLevel() {

            foreach (var levelObject in levelObjects) {
                GameScreen.Children.Remove(levelObject.Shape);
            }
            levelObjects.Clear();
            Level.clearWall();
        }

        private async void button_loadLevel_Click(object sender, RoutedEventArgs e) {
            string levelName = textBox_levelName.Text + ".xml";

            var dialogLoad = new MessageDialog("Load: " + levelName + "?");
            dialogLoad.Title = "Load level";
            dialogLoad.Commands.Add(new UICommand { Label = "Load", Id = 0 });
            dialogLoad.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var result = await dialogLoad.ShowAsync();

            // Load
            if ((int)result.Id == 0) {
                // Clear current
                clearLevel();

                // Load the level
                try {
                    Level = await LevelLoader.load(levelName, false);
                }
                catch (Exception) {
                    await new MessageDialog("Error loading level: " + levelName).ShowAsync();
                }

                // Draw the loaded level
                foreach (var brick in Level.Bricks) {
                    var drawObject = drawObjects.createAndAddObject(
                        Shapes.ShapeFactory.objectShape.SimpleBrickShape,
                        brick.Size.X, brick.Size.Y,
                        brick.Position.X, brick.Position.Y);
                    drawObject.Id = brick.Id;
                }

                debugMessage("Level loaded.");
            }
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

        internal Point snapToGrid(Point point) {
            // Snap the X
            int xPos = (int)point.X;
            int xRemaining = xPos % GridSize;
            xPos -= xRemaining;
            if (xRemaining > HalfGridSize) {
                xPos += GridSize;
            }
            // Snap the Y
            int yPos = (int)point.Y;
            int yRemaining = yPos % GridSize;
            yPos -= yRemaining;
            if (yRemaining > HalfGridSize) {
                yPos += GridSize;
            }
            return new Point(xPos, yPos);
        }

        internal void setPointer(Point snappedPoint) {
            // Move the game pointer
            Canvas.SetLeft(gamePointer, snappedPoint.X - gamePointer.Width / 2);
            Canvas.SetTop(gamePointer, snappedPoint.Y - gamePointer.Height / 2);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            createGrid();
            deleteButton.Visibility = Visibility.Collapsed;
        }

        private async void button_clearScreen_Click(object sender, RoutedEventArgs e) {
            var dialogDelAll = new MessageDialog("Delete all bricks?");
            dialogDelAll.Title = "Delete ALL objects";
            dialogDelAll.Commands.Add(new UICommand { Label = "DELETE ALL", Id = 0 });
            dialogDelAll.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var result = await dialogDelAll.ShowAsync();

            // Clear
            if((int)result.Id == 0) {
                clearLevel();
            }          
        }
    }
}