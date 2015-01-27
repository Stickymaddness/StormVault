using System;

namespace StormVault.Core
{
    public class Hero
    {
        public string Name { get; set; }
        public TimeSpan TimePlayed { get; set; }

        public Hero(string name, TimeSpan timePlayed)
        {
            this.Name = name;
            TimePlayed = timePlayed;
        }

        public void Update(Hero hero)
        {
            TimePlayed += hero.TimePlayed;
        }
    }
}
