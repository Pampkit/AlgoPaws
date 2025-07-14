using AlgoPaws.ViewModels;
using AlgoPaws.Algorithms;
using System.Windows;
using System.Windows.Controls;

namespace AlgoPaws.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        private String comboBoxState = "Bubble Sort";
        private bool startState = false;
        private String speedState = "1x";

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(VisualizationCanvas);
            viewModel.GenerateArray();

            ComboBox.Items.Add("Bubble Sort");
            ComboBox.Items.Add("Quick Sort");
            ComboBox.SelectedIndex = 0;
            ComboBox.SelectionChanged += onComboBoxSelectionChanged;

            Speed.Items.Add("1x");
            Speed.Items.Add("2x");
            Speed.Items.Add("3x");
            Speed.SelectedIndex = 0;
            Speed.SelectionChanged += onSpeedSelectionChanged;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!startState)
            {
                StartButton.Content = "Стоп";
                viewModel.RunSort(comboBoxState, speedState);
                startState = true;
                GenerateButton.IsEnabled = false;
                ComboBox.IsEnabled = false;
            }
            else
            {
                viewModel.CancelSort();
                startState = false;
                StartButton.Content = "Старт";
                GenerateButton.IsEnabled = true;
                ComboBox.IsEnabled = true;
            }
            
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GenerateArray();
        }

        private void onComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxState = ComboBox.SelectedValue?.ToString();
        }

        private void onSpeedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            speedState = Speed.SelectedValue?.ToString();
        }
    }
}