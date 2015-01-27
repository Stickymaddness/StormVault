using StormVault.Core;
using System.Windows.Controls;

namespace StormVault.UI.View
{
    public partial class MapView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public MapView()
        {
            InitializeComponent();
        }
    }
}
