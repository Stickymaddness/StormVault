using StormVault.Core;
using StormVault.UI.Utility;
using StormVault.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StormVault.UI
{
    public static class ApplicationState
    {
        public static string Version = "StormVault 0.0.1";

        private static Action loadDefaultView;
        private static Action loadLoadingView;
        private static MatchPersister dataStore;
        private static FileSystemWatcher replayWatcher;

        public static List<Match> Matches { get; private set; }
        public static Settings Config { get; set; }
        internal static MatchDataViewModel MatchViewModel { get; set; }
        internal static LoadingViewModel LoadingVM { get; set; }

        internal static void Initialize(Action LoadDefaultView, Action LoadLoadingView)
        {
            Config = new Settings();
            loadDefaultView = LoadDefaultView;
            loadLoadingView = LoadLoadingView;
            MatchViewModel = new MatchDataViewModel();
            LoadingVM = new LoadingViewModel();
        }

        internal static void LoadDefaultView()
        {
            loadDefaultView();
        }

        internal static void InitializeData()
        {
            dataStore = new MatchPersister(Config.PlayerName);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    LoadingVM.UpdateLoadingText("Checking for Stored Matches");

                    var savedMatches = dataStore.GetMatches();

                    if (savedMatches.Count() > 0)
                        LoadingVM.UpdateLoadingText("Processing Stored Matches");

                    MatchViewModel.ProcessMatches(savedMatches);

                    ReplayMapper mapper = new ReplayMapper(Config.PlayerName);

                    var baseDir = Config.ReplayPath;
                    var files = Directory.GetFiles(baseDir, "*.stormreplay").Where(f => !savedMatches.Select(m => m.FileName).Contains(f.Split('\\').Last()));

                    Parallel.ForEach(files, file =>
                    {
                        var match = mapper.ParseReplay(file);

                        if (match != null)
                        {
                            LoadingVM.UpdateLoadingText("Processing " + match.FileName);
                            MatchViewModel.ProcessMatch(match);
                            dataStore.AddMatch(match);
                        }
                    });

                    dataStore.Save();

                    LoadingVM.UpdateLoadingText("Done!");

                    MatchViewModel.UpdateBindings();

                    loadDefaultView();
                    InitializeReplayWatcher();
                }
                catch (Exception ex)
                {
                    Logger.Log("Error initializing data: " + ex.ToString());
                    MessageBox.Show("Error loading replay data, for assistance please open a ticket at https://github.com/Stickymaddness/StormVault and include your error.log file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private static void InitializeReplayWatcher()
        {
            if (!Config.MonitoringEnabled)
                return;

            replayWatcher = new FileSystemWatcher(Config.ReplayPath);
            replayWatcher.Filter = "*.StormReplay";
            replayWatcher.Created += Replay_Created;
            replayWatcher.EnableRaisingEvents = true;
        }

        static void Replay_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                Thread.Sleep(3000);

                if (!FileInvestigator.IsFileFree(e.FullPath, 25))
                    return;

                ReplayMapper mapper = new ReplayMapper(Config.PlayerName);
                var match = mapper.ParseReplay(e.FullPath);

                if (match != null)
                {
                    MatchViewModel.ProcessMatch(match);
                    dataStore.AddMatch(match);
                }
                dataStore.Save();
                ApplicationState.MatchViewModel.UpdateBindings();

            }
            catch (Exception)
            { }
        }

        internal static void LoadLoadingView()
        {
            loadLoadingView();
        }

        internal static void ToggleMonitoring(bool enabled)
        {
            if (replayWatcher != null)
                replayWatcher.EnableRaisingEvents = enabled;
        }
    }
}
