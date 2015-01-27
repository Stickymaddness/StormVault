using Heroes.ReplayParser;
using StormVault.Core;
using System;
using System.Linq;

namespace StormVault.Core
{
    public class ReplayMapper
    {
        private const string Details = "replay.details";
        private const string Messages = "replay.messages";
        private const string InitData = "replay.initData";
        private const string TrackerEvents = "replay.tracker.events";
        private const string AttributeEvents = "replay.attributes.events";

        private readonly string accountName;
        public ReplayMapper(string accountName)
        {
            this.accountName = accountName;
        }

        public Match ParseReplay(string path)
        {
            using (var archive = Nmpq.MpqArchive.Open(path))
            {
                try
                {
                    Replay replay = new Replay();

                    ReplayInitData.Parse(replay, archive.ReadFile(InitData));
                    ReplayTrackerEvents.Parse(replay, archive.ReadFile(TrackerEvents));
                    ReplayDetails.Parse(replay, archive.ReadFile(Details));

                    ReplayAttributeEvents.Parse(replay, archive.ReadFile(AttributeEvents));

                    var player = replay.Players.First(p => p.Name == accountName);

                    Match match = new Match();

                    match.FileName = path.Split('\\').Last();
                    match.Map = replay.Map;
                    match.Win = player.IsWinner;
                    match.Character = new Hero(player.Character, replay.ReplayLength);
                    match.TimeStamp = replay.Timestamp;

                    replay = null;

                    return match;
                }
                catch (Exception ex)
                {
                    Logger.Log(string.Format("Error mapping {0}, exception details : {1}", path, ex.ToString()));
                    return null;
                }
            }
        }
    }
}
