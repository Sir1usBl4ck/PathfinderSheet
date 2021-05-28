using System.Collections.ObjectModel;

namespace PathfinderSheetModels
{
    public enum ArmorType
    {
        Light,
        Medium,
        Heavy
    }
    public enum ItemSlot
    {
        Armor,
        Shield,
        Head,
        Headband,
        Eyes,
        Neck,
        Shoulders,
        Chest,
        Belt,
        Body,
        Arms,
        Feet,
        Ring1,
        Ring2,
        Hands
    }

    public class Item : ObservableObject
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; } = 1;

    }

    public class WearableItem : Item
    {
        private bool _isWorn;
        public ItemSlot Slot { get; set; }

        public bool IsWorn
        {
            get => _isWorn;
            set
            {
                _isWorn = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
    }

    public class Armor : WearableItem
    {
        public Armor()
        {
            Slot = ItemSlot.Armor;
        }
        public int ArmorClass { get; set; }
        public int MaxSpeed { get; set; }
        public int CheckPenalty { get; set; }
        public int MaxDexterity { get; set; }
        public ArmorType Type { get; set; }
        public int SpellFailurePercentage { get; set; }

    }

    public class Shield : WearableItem
    {
        public Shield()
        {
            Slot = ItemSlot.Shield;
        }
        public int ArmorClass { get; set; }
        public int CheckPenalty { get; set; }
        public int SpellFailurePercentage { get; set; }
    }

    public interface IConsumable 
    {

    }
    public class Potion : Item, IConsumable
    {
        
    }
    public class Scroll : Item, IConsumable
    {

    }


    public class Wand : Item, IConsumable
    {
       public int Charges { get; set; }

    }
}
