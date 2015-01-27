namespace StormVault.UI.ViewModel
{
    public class MatchStatistic
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public MatchStatistic(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
