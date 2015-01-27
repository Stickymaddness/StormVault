using StormVault.UI.ViewModel;
using System.Windows.Controls;

namespace StormVault.UI.View
{
    public partial class FirstRunView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public FirstRunView()
        {
            InitializeComponent();
            this.DataContext = new FirstRunViewModel();
        }
    }
}
