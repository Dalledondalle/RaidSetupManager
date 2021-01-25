using System;
using System.Collections.Generic;
using System.Text;

namespace RaidSetupManager.Domain
{
    public class Character
    {
        public string Name { get; set; }
        public string Realm { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
        public string Guild { get; set; }
        public int GuildRank { get; set; }
        public int Level { get; set; }
        public int EquppedItemLevel { get; set; }
        public int AverageItemLevel { get; set; }
        public DateTime LastLogin { get; set; }

        //Equipment

    }
}
