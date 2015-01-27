using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormVault.UI.ViewModel
{
    public class LoadingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string LoadingText { get; private set; }

        public void UpdateLoadingText(string text)
        {
            LoadingText = text;
            OnPropertyChanged("LoadingText");
        }
    }
}
