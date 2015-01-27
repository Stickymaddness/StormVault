using StormVault.Core;
using System;
using System.Linq;

namespace StormVault.UI.ViewModel
{
    public class HeroStatistic
    {
        public string Hero { get; set; }
        public int Level { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int WinRatio { get; set; }
        public TimeSpan TimePlayed { get; set; }


        public HeroStatistic(IGrouping<string, Match> group)
        {
            Hero = group.Key;
            Matches = group.Count();
            Wins = group.Where(m => m.Win).Count();
            Losses = group.Where(m => m.Win == false).Count();
            WinRatio = Convert.ToInt32((double)Wins / (double)Matches * 100);
            TimePlayed = group.Select(g => g.Duration).Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal.Add(t));
        }
    }
}
