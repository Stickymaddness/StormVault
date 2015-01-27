using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace StormVault.UI.ViewModel
{
    public class FirstRunViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set
            {
                if (value != playerName)
                {
                    playerName = value;
                    OnPropertyChanged("PlayerName");
                }
            }
        }

        private string replayPath;
        public string ReplayPath
        {
            get { return replayPath; }
            set
            {
                if (value != replayPath)
                {
                    replayPath = value;
                    OnPropertyChanged("ReplayPath");
                }
            }
        }

        public DelegateCommand BrowseCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public FirstRunViewModel()
        {
            BrowseCommand = new DelegateCommand(BrowseClicked);
            SaveCommand = new DelegateCommand(SaveClicked);
        }

        private void SaveClicked(object obj)
        {
            ApplicationState.Config.PlayerName = playerName;
            ApplicationState.Config.ReplayPath = replayPath;
            ApplicationState.Config.Save();

            ApplicationState.LoadLoadingView();
        }

        private void BrowseClicked(object obj)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Heroes of the Storm");

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                    ReplayPath = folderBrowser.SelectedPath;
            }
        }
    }
}
