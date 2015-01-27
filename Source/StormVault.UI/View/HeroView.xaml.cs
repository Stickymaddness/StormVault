using StormVault.Core;
using System.Windows.Controls;

namespace StormVault.UI.View
{
    public partial class HeroView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public HeroView()
        {
            InitializeComponent();
        }
    }
}
