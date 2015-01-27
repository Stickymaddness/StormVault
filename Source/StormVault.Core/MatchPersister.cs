using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StormVault.Core
{
    public class MatchPersister
    {
        private ConcurrentBag<Match> matches = new ConcurrentBag<Match>();
        private bool changes = false;
        private string playerName;
        private string fileName
        {
            get { return playerName + ".json"; }
        }

        public MatchPersister(string playerName)
        {
            this.playerName = playerName;

            if (!File.Exists(fileName))
                return;

            var persistedMatches = DataStore.Fetch<IEnumerable<Match>>(fileName);

            Parallel.ForEach(persistedMatches, match => matches.Add(match));
        }

        public IEnumerable<Match> GetMatches()
        {
            return matches;
        }

        public void AddMatch(Match match)
        {
            matches.Add(match);
            changes = true;
        }

        public void Save()
        {
            if (!changes)
                return;

            DataStore.Store(matches, fileName);
        }
    }
}
