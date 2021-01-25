using System;
using System.Collections.Generic;
using System.Text;

namespace Blizzard.Data
{
    public class BlizzardCharacter
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int equipped_item_level { get; set; }
        public int average_item_level { get; set; }
        public Race race { get; set; }
        public BlizzardEquipment equipment { get; set; }
        public Character_Class character_class { get; set; }
        public Guild guild { get; set; }
        public Realm realm { get; set; }
        public double last_login_timestamp { get; set; }
    }
}
