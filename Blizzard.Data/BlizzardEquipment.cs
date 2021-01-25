using System.Threading;

namespace Blizzard.Data
{
    public class BlizzardEquipment
    {
        public string href 
        { 
            set
            {
                equipment = BlizzardAPI.GetEquipment(value);
            }
        }
        public BlizzardEquipment equipment { get; set; }

        public Equiped_Item[] equipped_items;
    }
}
