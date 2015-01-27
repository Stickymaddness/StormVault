using StormVault.UI.ViewModel;
using System;
using System.Windows.Controls;

namespace StormVault.UI.View
{
    public partial class LoadingView : UserControl, IView
    {
        public LoadingView()
        {
            InitializeComponent();
            this.DataContext = ApplicationState.LoadingVM;
            ApplicationState.InitializeData();
        }

        public new Grid Content
        {
            get { return ViewContent; }
        }
    }
}
