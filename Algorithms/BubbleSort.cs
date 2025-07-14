using AlgoPaws.Models;
using System.Windows;

namespace AlgoPaws.Algorithms
{
    internal class BubbleSort : ISortingAlgorithm
    {
        public string Name => "Bubble Sort";

        public int defaultSpeed = 600;

        public int coefSpeed = 1;

        UIController controller = new UIController();

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
                    controller.Highlight(items, j);
                    controller.Highlight(items, j + 1);

                    if (items[j].Value > items[j + 1].Value)
                    {
                        int duration = defaultSpeed / coefSpeed;
                        controller.Swap(items, j, j + 1, duration);
                    }

                    await Task.Delay(defaultSpeed / coefSpeed);

                    controller.ResetHighlight(items, j);
                    controller.ResetHighlight(items, j + 1);
                }
            }
        }
    }
}
