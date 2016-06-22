using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace BreakOut_view.Shapes {
    public class ShapeFactory {
        public enum objectShape {
            PaddleShape = 0,
            BallShape,
            SimpleBrickShape,

            objectShapeSize // Array size
        };

        static Brush[] objectBrushes = new Brush[(int)objectShape.objectShapeSize];

        static private Brush getBrush(objectShape objectShape) {
            
            // Check If already used before, if not, create the brush
            Brush brush = objectBrushes[(int)objectShape];
            if (brush == null) {
                // First use of object, create the brush
                switch (objectShape) {
                    case objectShape.PaddleShape: {
                            BitmapImage paddleBitmapImage = new BitmapImage();
                            paddleBitmapImage.UriSource = new Uri(@"ms-appx:///textures/paddle.png");
                            brush = objectBrushes[(int)objectShape] = new ImageBrush() { ImageSource = paddleBitmapImage };
                            break;
                        }
                    case objectShape.BallShape: {
                            BitmapImage ballBitmapImage = new BitmapImage();
                            ballBitmapImage.UriSource = new Uri(@"ms-appx:///textures/ball.jpg");
                            brush = objectBrushes[(int)objectShape] = new ImageBrush() { ImageSource = ballBitmapImage };
                            break;
                        }
                    case objectShape.SimpleBrickShape: {

                            // Create and add Gradient stops
                            LinearGradientBrush brickLGB = new LinearGradientBrush() { StartPoint = new Point(0.5, 0), EndPoint = new Point(0.5, 1) };
                            brickLGB.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 0, 0), Offset = 1 });
                            brickLGB.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 0, 0) });
                            brickLGB.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(255, 255, 100, 100), Offset = 0.5 });
                            brush = objectBrushes[(int)objectShape] = brickLGB;
                            break;
                        }
                }
            }
            return brush;
        }

        static public DrawableObject createShape(objectShape objectShape) {
            DrawableObject newObject = new DrawableObject();
            switch (objectShape) {
                case objectShape.PaddleShape: {
                        newObject.Shape = new Rectangle() {
                            Fill = getBrush(objectShape)
                        };
                        break;
                    }
                case objectShape.BallShape: {
                        newObject.Shape = new Ellipse() {
                            Fill = getBrush(objectShape)
                        };
                        break;
                    }
                case objectShape.SimpleBrickShape: {
                        newObject.Shape = new Rectangle() {
                            Fill = getBrush(objectShape)
                        };
                        break;
                    }
            }
            return newObject;
        }
    }
}
