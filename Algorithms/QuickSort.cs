using AlgoPaws.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;

namespace AlgoPaws.Algorithms
{
    internal class QuickSort : ISortingAlgorithm
    {
        public string Name => "Quick Sort";

        public int defaultSpeed = 1000;

        public int coefSpeed = 1;

        public async Task Sort(List<NumberItem> items, int speed, CancellationToken cancellationToken)
        {
            coefSpeed = speed;
            int n = items.Count;
            await QuickSortAlgorithm(items, 0, n - 1, cancellationToken);
        }

        private async Task<int> Partition(List<NumberItem> items, int low, int high, CancellationToken cancellationToken)
        {
            int pivotIndex = (low + high) / 2;
            int pivot = items[pivotIndex].Value;
            
            ResetPivotHighlight(items);
            items[pivotIndex].Rectangle.Fill = Brushes.Green;

            await Task.Delay(defaultSpeed / coefSpeed / 2);

            int i = low;
            int j = high;

            while (i <= j)
            {
                cancellationToken.ThrowIfCancellationRequested();

                while (items[i].Value < pivot)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    i++;
                }

                while (items[j].Value > pivot)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    j--;
                }
                if (i >= j)
                {
                    break;
                }

                Highlight(items, i, j);
                Swap(items, i++, j--);

                await Task.Delay(defaultSpeed / coefSpeed / 2);

                ResetHighlight(items, i - 1);
                ResetHighlight(items, j + 1);
            }
            return j;
        }

        public async Task QuickSortAlgorithm(List<NumberItem> items, int low, int high, CancellationToken cancellationToken)
        {
            if (low < high) {
                cancellationToken.ThrowIfCancellationRequested();
                int pi = await Partition(items, low, high, cancellationToken);
                await QuickSortAlgorithm(items, low, pi, cancellationToken);
                await QuickSortAlgorithm(items, pi + 1, high, cancellationToken);
            }
        }

        private void AnimateSwap(Rectangle rect, double toX)
        {
            var anim = new DoubleAnimation
            {
                To = toX,
                Duration = TimeSpan.FromMilliseconds(defaultSpeed / coefSpeed / 2),
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

        private void ResetHighlight(List<NumberItem> items, int index)
        {
            items[index].Rectangle.Fill = Brushes.Blue;
        }

        private void ResetPivotHighlight(List<NumberItem> items)
        {
            foreach (var item in items)
            {
                if (item.Rectangle.Fill == Brushes.Green)
                    item.Rectangle.Fill = Brushes.Blue;
            }
        }
    }
}
