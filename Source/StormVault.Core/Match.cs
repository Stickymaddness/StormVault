using System;
using System.Collections.Generic;

namespace StormVault.Core
{
    public class Match
    {
        public string FileName { get; set; }
        public bool Win { get; set; }
        public string Map { get; set; }
        public Hero Character { get; set; }
        public DateTime TimeStamp { get; set; }

        public string HeroName
        {
            get { return Character.Name; }
        }

        public TimeSpan Duration
        {
            get { return Character.TimePlayed; }
        }
    }
}
