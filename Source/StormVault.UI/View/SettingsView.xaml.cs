using StormVault.UI.ViewModel;
using System.Windows.Controls;

namespace StormVault.UI.View
{
    public partial class SettingsView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }
    }
}
