using AlgoPaws.Algorithms;
using AlgoPaws.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgoPaws.ViewModels
{
    public class MainViewModel
    {
        private const int RectWidth = 20;
        private readonly Random random = new Random();
        public List<NumberItem> Items { get; private set; } = new List<NumberItem>();

        private readonly Canvas canvas;

        private readonly Dictionary<string, ISortingAlgorithm> _algorithms;

        private CancellationTokenSource _cancellationTokenSource;

        public MainViewModel(Canvas canvas)
        {
            this.canvas = canvas;
            _algorithms = new Dictionary<string, ISortingAlgorithm>
            {
                { "Quick Sort", new QuickSort() },
                { "Bubble Sort", new BubbleSort() },
            };
        }

        public void GenerateArray(int count = 30)
        {
            Items.Clear();
            canvas.Children.Clear();

            for (int i = 0; i < count; i++)
            {
                int value = random.Next(10, 300);
                Rectangle rect = new Rectangle
                {
                    Width = RectWidth - 2,
                    Height = value,
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                Canvas.SetLeft(rect, i * RectWidth);
                Canvas.SetBottom(rect, 0);

                canvas.Children.Add(rect);
                Items.Add(new NumberItem(value, rect));
            }
        }

        public async Task RunSort(string algName)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            if (_algorithms.TryGetValue(algName, out var algorithm))
            {
                try
                {
                    await algorithm.Sort(Items, _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        foreach (var item in Items)
                        {
                            item.Rectangle.Fill = Brushes.Blue;
                        }
                    });
                }
            }
        }

        public void CancelSort()
        {
            _cancellationTokenSource?.Cancel();
        }

        public List<string> AvailableAlgorithms => _algorithms.Keys.ToList();
    }
}