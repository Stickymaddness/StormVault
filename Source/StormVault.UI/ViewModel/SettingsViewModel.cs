using System;
using System.ComponentModel;
using System.Windows;

namespace StormVault.UI.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public string PlayerName 
        {
            get { return ApplicationState.Config.PlayerName; }
            set 
            {
                if (PlayerName == value)
                    return;
                
                ApplicationState.Config.PlayerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        public string ReplayPath
        {
            get { return ApplicationState.Config.ReplayPath; }
            set 
            {
                if (ReplayPath == value)
                    return;

                ApplicationState.Config.ReplayPath = value;
                OnPropertyChanged("ReplayPath");
            }
        }

        public bool MonitoringEnabled
        {
            get { return ApplicationState.Config.MonitoringEnabled; }
            set
            {
                if (MonitoringEnabled == value)
                    return;

                OnPropertyChanged("MonitoringEnabled");
                ToggleMonitoring(value);
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        public SettingsViewModel()
        {
            SaveCommand = new DelegateCommand(SaveClicked);
        }

        private void SaveClicked(object obj)
        {
            ApplicationState.Config.Save();
            MessageBox.Show("Updating player name or replay path requires restarting StormVault for changes to reflect", "Settings Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static void ToggleMonitoring(bool value)
        {
            ApplicationState.Config.MonitoringEnabled = value;
            ApplicationState.Config.Save();
            ApplicationState.ToggleMonitoring(value);

            var toggle = value ? "enabled" : "disabled";
            MessageBox.Show("Monitoring " + toggle, "Setting Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
