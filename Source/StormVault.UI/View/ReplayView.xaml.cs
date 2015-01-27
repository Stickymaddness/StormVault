using System.Windows.Controls;
using System.Windows.Media;

namespace StormVault.UI.View
{
    public partial class ReplayView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public ReplayView()
        {
            InitializeComponent();
        }
    }
}
