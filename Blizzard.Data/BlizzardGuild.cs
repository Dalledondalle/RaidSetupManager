namespace Blizzard.Data
{
    public class BlizzardGuild
    {
        public string name { get; set; }
        public int id { get; set; }
        public Faction faction { get; set; }
        public int member_count { get; set; }
        public Realm realm { get; set; }
        public BlizzardRoster roster { get; set; }
    }
}
