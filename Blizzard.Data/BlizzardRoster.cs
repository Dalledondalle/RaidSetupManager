namespace Blizzard.Data
{
    public class BlizzardRoster
    {
        public string href { set
            {
                roster = BlizzardAPI.GetRoster(value);
            } 
        }
        public Roster roster { get; set; }
    }
}