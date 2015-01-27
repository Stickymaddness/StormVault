using StormVault.Core;
using System.Windows;

namespace StormVault.UI
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log(e.Exception);
            MessageBox.Show("An unhandled exception has occured, for assistance please open a ticket at https://github.com/Stickymaddness/StormVault and include your error.log file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
