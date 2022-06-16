using MowerSim.Logic;
using MowerSim.Logic.Utils;
using System.Windows;

namespace MowerSim.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new Board(20, 20, 20);
            ViewModel.MessageSent += OnMowed;
        }

        #endregion Constructors

        #region Properties

        private Board ViewModel
        {
            get => DataContext as Board;
            set => DataContext = value;
        }

        #endregion Properties

        #region Methods

        private void OnMowed(object sender, DataEventArgs<string> e)
        {
            MessageBox.Show(e.Data, "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void OnResetMowing(object sender, RoutedEventArgs e) => ViewModel.Reset();

        private async void OnStartMowing(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(StartIndex.Text, out int index))
            {
                await ViewModel.StartMowingAsync(index);
            }
            else
            {
                var msg = $"The specified index '{StartIndex.Text}' is not a number.";
                MessageBox.Show(msg, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnStopMowing(object sender, RoutedEventArgs e) => ViewModel.StopMowing();

        #endregion Methods
    }
}