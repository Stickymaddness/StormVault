using StormVault.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace StormVault.UI.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainView;
        private static Dictionary<string, IView> screens = new Dictionary<string, IView>();
        private static bool initialized = false;

        public DelegateCommand MenuButtonCommand { get; set; }

        public MainWindowViewModel(MainWindow mainView)
        {
            this.mainView = mainView;

            MenuButtonCommand = new DelegateCommand(LoadScreen);
            ApplicationState.Initialize(LoadDefaultView, LoadLoadingView);
            LoadView(GetInitialScreen());
        }

        private void LoadScreen(object obj)
        {
            var key = obj.ToString();
            LoadView(screens[key]);
        }

        public void LoadView(IView view)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    mainView.MainRegion.Children.Clear();
                    mainView.MainRegion.Children.Add(view as UserControl);
                }));
        }

        private void InitScreens()
        {
            screens.Add("Replay", new ReplayView { DataContext = ApplicationState.MatchViewModel });
            screens.Add("Map", new MapView { DataContext = ApplicationState.MatchViewModel });
            screens.Add("Hero", new HeroView { DataContext = ApplicationState.MatchViewModel });
            screens.Add("Setting", new SettingsView());
            screens.Add("About", new AboutView());
        }

        private static IView GetInitialScreen()
        {
            if (ApplicationState.Config.UserSettingsEmpty)
                return new FirstRunView();

            return new LoadingView();
        }

        private void LoadLoadingView()
        {
            LoadView(new LoadingView());
        }

        private void LoadDefaultView()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    if (!initialized)
                        InitScreens();

                    LoadView(screens.First().Value); 
                    mainView.MenuBar.Visibility = Visibility.Visible;

                    initialized = true;
                }));
        }
    }
}
