using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace StormVault.UI.View
{
    public partial class AboutView : UserControl, IView
    {
        public new Grid Content
        {
            get { return ViewContent; }
        }

        public AboutView()
        {
            InitializeComponent();
        }

        private void GithubLink_Click(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
