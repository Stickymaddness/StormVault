using StormVault.UI.ViewModel;
using System.Windows;

namespace StormVault.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
            this.Title = ApplicationState.Version;
        }
    }
}
