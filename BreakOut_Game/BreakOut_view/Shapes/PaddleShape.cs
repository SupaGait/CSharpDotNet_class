using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace BreakOut_view.Shapes {
    /*
    class PaddleShape : Shape {
        public PaddleShape(Size size) : base() {
            Height = size.Height;
            Width = size.Width;
            Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
        }
    }
    */
    class ShapeFactory {
        public enum objectShape {
            PaddleShape = 0,
            BallShape,
            SimpleBrickShape,
        };

        static public DrawableObject createShape(objectShape objectShape/*, Size size*/) {
            DrawableObject newObject = new DrawableObject();
            switch (objectShape) {
                case objectShape.PaddleShape: {
                        newObject.Shape = new Rectangle() {
                            Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)),
                            Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255)),
                            StrokeThickness = 1.0
                        };
                        break;
                    }
                case objectShape.BallShape: {
                        newObject.Shape = new Ellipse() {
                            Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0))
                        };
                        break;
                    }
                case objectShape.SimpleBrickShape: {
                        newObject.Shape = new Rectangle() {
                            Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)),
                            Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255)),
                            StrokeThickness = 1.0
                        };
                        break;
                    }
            }
            return newObject;
        }
    }
}
