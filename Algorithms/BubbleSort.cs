using AlgoPaws.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AlgoPaws.Algorithms
{
    internal class BubbleSort : ISortingAlgorithm
    {
        public string Name => "Bubble Sort";

        public int defaultSpeed = 600;

        public int coefSpeed = 1;

        public async Task Sort(List<NumberItem> items, int speed, CancellationToken cancellationToken)
        {
            coefSpeed = speed;
            await BubbleSortAlgorithm(items, cancellationToken);
        }

        public async Task BubbleSortAlgorithm(List<NumberItem> items, CancellationToken cancellationToken)
        {
            int n = items.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Highlight(items, j, j + 1);

                    if (items[j].Value > items[j + 1].Value)
                    {
                        Swap(items, j, j + 1);
                    }

                    await Task.Delay(defaultSpeed / coefSpeed);

                    ResetHighlight(items, j, j + 1);
                }
            }
        }

        private void AnimateSwap(Rectangle rect, double toX)
        {
            var anim = new DoubleAnimation
            {
                To = toX,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            rect.BeginAnimation(Canvas.LeftProperty, anim);
        }

        private void Swap(List<NumberItem> items, int index1, int index2)
        {
            var item1 = items[index1];
            var item2 = items[index2];

            var rect1 = item1.Rectangle;
            var rect2 = item2.Rectangle;

            double toX1 = index2 * 20;
            double toX2 = index1 * 20;

            AnimateSwap(rect1, toX1);
            AnimateSwap(rect2, toX2);

            Task.Delay(defaultSpeed / coefSpeed);

            items[index1] = item2;
            items[index2] = item1;
        }

        private void Highlight(List<NumberItem> items, int index1, int index2)
        {
            items[index1].Rectangle.Fill = Brushes.Red;
            items[index2].Rectangle.Fill = Brushes.Red;
        }

        private void ResetHighlight(List<NumberItem> items, int index1, int index2)
        {
            items[index1].Rectangle.Fill = Brushes.Blue;
            items[index2].Rectangle.Fill = Brushes.Blue;
        }
    }
}
