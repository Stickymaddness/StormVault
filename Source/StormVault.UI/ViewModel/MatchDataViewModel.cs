using StormVault.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StormVault.UI.ViewModel
{
    internal class MatchDataViewModel : INotifyPropertyChanged
    {
        private ConcurrentBag<Match> matches;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public int TotalMatches
        {
            get { return matches.Count; }
        }

        public int TotalWins
        {
            get { return matches.Where(m => m.Win).Count(); }
        }

        public int TotalLosses
        {
            get { return matches.Where(m => m.Win == false).Count(); }
        }

        public int TotalHoursPlayed
        {
            get { return Convert.ToInt32(matches.Select(m => m.Character.TimePlayed).Sum(t => t.TotalHours)); }
        }

        private Dictionary<string, int> matchStatistics = new Dictionary<string, int>();
        public ObservableCollection<MatchStatistic> MatchStatistics
        {
            get
            {
                if (matches.Count() == 0)
                    return new ObservableCollection<MatchStatistic>();

                var result = new ObservableCollection<MatchStatistic>();

                double won = matches.Where(m => m.Win).Count();
                double total = matches.Count();
                double ratio = won / total;

                result.Add(new MatchStatistic("Matches Won", ratio * 100));

                return result;
            }
        }

        public ObservableCollection<MatchStatistic> MapStatistics
        {
            get
            {
                if (matches.Count() == 0)
                    return new ObservableCollection<MatchStatistic>();

                var result = new ObservableCollection<MatchStatistic>();

                foreach (var group in matches.GroupBy(m => m.Map))
                    result.Add(new MatchStatistic(group.Key, group.Count()));

                return result;
            }
        }

        public IEnumerable<HeroStatistic> HeroStatistics
        {
            get
            {
                if (matches.Count() == 0)
                    return new List<HeroStatistic>();

                var result = new List<HeroStatistic>();

                foreach (var group in matches.GroupBy(i => i.Character.Name))
                    result.Add(new HeroStatistic(group));

                return result.OrderByDescending(r => r.Matches);
            }
        }

        public IEnumerable<HeroStatistic> TopThreePlayed
        {
            get
            {
                if (matches.Count() == 0)
                    return new List<HeroStatistic>();

                var result = new List<HeroStatistic>();

                foreach (var group in matches.GroupBy(i => i.Character.Name))
                    result.Add(new HeroStatistic(group));

                return result.OrderByDescending(r => r.Matches).Take(4);
            }
        }

        public IEnumerable<Match> ReplayStatistics
        {
            get { return matches.OrderByDescending(m => m.TimeStamp); }
        }

        internal void ProcessMatch(Match match)
        {
            matches.Add(match);
        }

        internal void UpdateBindings()
        {
            OnPropertyChanged("");
        }

        public MatchDataViewModel()
        {
            matches = new ConcurrentBag<Match>();
        }

        internal void ProcessMatches(IEnumerable<Match> savedMatches)
        {
            Parallel.ForEach(savedMatches, match => matches.Add(match));
        }
    }
}
