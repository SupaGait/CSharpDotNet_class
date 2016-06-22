using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace BreakOut_view {
    public class DrawableObject {
        public Shape Shape { get; set; }
        public Transform transform { get; set; }
        public long Id { get; set; }
    }
}
