namespace Blizzard.Data
{
    public class Equiped_Item
    {
        public Item item { get; set; }
        public Slot slot { get; set; }
        public Quality quality { get; set; }
        public Name name { get; set; }
        public Item_Class item_class { get; set; }
        public Item_Subclass item_subclass { get; set; }
        public Inventory_Type inventory_type { get; set; }
        public Requirements requirements { get; set; }
        public Level level { get; set; }
    }
}
