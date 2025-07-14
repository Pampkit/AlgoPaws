using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace AlgoPaws.Models
{
    public class UIController
    {
        public void Highlight(List<NumberItem> items, int index)
        {
            items[index].Rectangle.Fill = Brushes.Red;
        }

        public void ResetHighlight(List<NumberItem> items, int index)
        {
            items[index].Rectangle.Fill = Brushes.Blue;
        }

        public void ResetPivotHighlight(List<NumberItem> items)
        {
            foreach (var item in items)
            {
                if (item.Rectangle.Fill == Brushes.Green)
                    item.Rectangle.Fill = Brushes.Blue;
            }
        }

        public void AnimateSwap(Rectangle rect, double toX, double duration)
        {
            var anim = new DoubleAnimation
            {
                To = toX,
                Duration = TimeSpan.FromMilliseconds(duration),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            rect.BeginAnimation(Canvas.LeftProperty, anim);
        }

        public void Swap(List<NumberItem> items, int index1, int index2, int duration)
        {
            var item1 = items[index1];
            var item2 = items[index2];

            var rect1 = item1.Rectangle;
            var rect2 = item2.Rectangle;

            double toX1 = index2 * 20;
            double toX2 = index1 * 20;

            AnimateSwap(rect1, toX1, duration);
            AnimateSwap(rect2, toX2, duration);

            Task.Delay(duration / 2);

            items[index1] = item2;
            items[index2] = item1;
        }
    }
}
