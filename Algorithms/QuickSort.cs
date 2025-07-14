using AlgoPaws.Models;
using System.Windows.Media;

namespace AlgoPaws.Algorithms
{
    internal class QuickSort : ISortingAlgorithm
    {
        public string Name => "Quick Sort";

        public int defaultSpeed = 1000;

        public int coefSpeed = 1;

        UIController controller = new UIController();

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

            controller.ResetPivotHighlight(items);
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

                controller.Highlight(items, i);
                controller.Highlight(items, j);

                int duration = defaultSpeed / coefSpeed;
                controller.Swap(items, i++, j--, duration);

                await Task.Delay(defaultSpeed / coefSpeed / 2);

                controller.ResetHighlight(items, i - 1);
                controller.ResetHighlight(items, j + 1);
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
    }
}
