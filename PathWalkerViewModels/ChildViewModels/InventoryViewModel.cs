using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using PathfinderSheetModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels.ChildViewModels
{
   


    public class InventoryViewModel : ChildViewModel, IHandle<WearableEquippedEvent>
    {
        public InventoryViewModel(Character character, EventAggregator eventAggregator)
        {

            EventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            Character = character;

            #region Debug

            var belt = new WearableItem {Name = "Test Belt", Slot = ItemSlot.Belt};
            belt.BonusList.Add(new Bonus{BonusSource = belt.Name, Target = AttributeType.Strength, BonusType = BonusType.Alchemical, Value = 2});
            Character.Inventory.ItemsList.Add(belt);

            var shoulder = new WearableItem { Name = "Test Shoulder", Slot = ItemSlot.Shoulders };
            shoulder.BonusList.Add(new Bonus { BonusSource = shoulder.Name, Target = AttributeType.Strength, BonusType = BonusType.Alchemical, Value = 4});
            Character.Inventory.ItemsList.Add(shoulder);

            var cloak = new WearableItem {Name = "Cloak of Resistance +1", Slot = ItemSlot.Shoulders};
            cloak.BonusList.Add(new Bonus{ BonusSource = cloak.Name, Target = AttributeType.Willpower, BonusType = BonusType.Enhancement, Value = 1});
            cloak.BonusList.Add(new Bonus { BonusSource = cloak.Name, Target = AttributeType.Reflexes, BonusType = BonusType.Enhancement, Value = 1 });
            cloak.BonusList.Add(new Bonus { BonusSource = cloak.Name, Target = AttributeType.Fortitude, BonusType = BonusType.Enhancement, Value = 1 });
            Character.Inventory.ItemsList.Add(cloak);

            Character.Inventory.Armor = new Armor
            {
                ArmorClass = 12,
                CheckPenalty = 4,
                Name = "ChainMail",
                Weight = 120,
                MaxDexterity = 4,
                SpellFailurePercentage = 10,
                MaxSpeed = 16,
                Type = ArmorType.Medium
            };
            #endregion

            WandsView = new CollectionView(Character.Inventory.ItemsList.Where(a => a is Wand));
            WearablesView = new CollectionView(Character.Inventory.ItemsList.Where(a => a is WearableItem));
            PotionsView = new CollectionView(Character.Inventory.ItemsList.Where(a => a is Potion));
            ScrollsView = new CollectionView(Character.Inventory.ItemsList.Where(a => a is Scroll));

            AddNewItemToInventoryCommand = new RelayCommand(AddNewItemToInventory);
            AddNewWearableToInventoryCommand = new RelayCommand(AddNewWearableToInventory);
            AddBonusToWearableCommand = new RelayCommand(AddBonusToWearable);
            DeleteItemCommand = new RelayCommand<Item>(DeleteItemFromInventory);
            AddNewArmorToInventoryCommand = new RelayCommand(AddNewArmorToInventory);
        }


        public ICollectionView WandsView { get; set; }
        public ICollectionView WearablesView { get; set; }
        public ICollectionView PotionsView { get; set; }
        public ICollectionView ScrollsView { get; set; }
        public bool IsInventoryDialogOpen { get; set; }
        public Item NewItem { get; set; } = new Item();
        public Potion NewPotion { get; set; } = new Potion();
        public Scroll NewScroll { get; set; } = new Scroll();
        public Wand NewWand { get; set; } = new Wand();
        public WearableItem NewWearable { get; set; } = new WearableItem();
        public Armor NewArmor { get; set; } = new Armor();
        public Bonus NewBonus { get; set; } = new Bonus();

        public Armor EquippedArmor { get; set; } = new Armor();

        public ICommand AddNewItemToInventoryCommand { get; set; }
        public ICommand AddNewWearableToInventoryCommand { get; set; }
        public ICommand AddBonusToWearableCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddNewArmorToInventoryCommand { get; set; }

        private void DeleteItemFromInventory(Item obj)
        {
            Character.Inventory.ItemsList.Remove(obj);
            PotionsView.Refresh();
            WandsView.Refresh();
            WearablesView.Refresh();
            ScrollsView.Refresh();

        }

        private void AddBonusToWearable()
        {
            NewBonus.BonusSource = NewWearable.Name;
            NewWearable.BonusList.Add(NewBonus);
            NewBonus = new Bonus();
            OnPropertyChanged(nameof(NewBonus));
            OnPropertyChanged(nameof(NewWearable));
        }
        private void AddNewWearableToInventory()
        {
            Character.Inventory.ItemsList.Add(NewWearable);
            NewWearable = new WearableItem();
            IsInventoryDialogOpen = false;
            OnPropertyChanged(nameof(NewWearable));
            OnPropertyChanged(nameof(IsInventoryDialogOpen));
        }

        private void AddNewItemToInventory()
        {
            Character.Inventory.ItemsList.Add(NewItem);
            NewItem = new Item();
            IsInventoryDialogOpen = false;
            OnPropertyChanged(nameof(NewItem));
            OnPropertyChanged(nameof(IsInventoryDialogOpen));
        }

        private void AddNewArmorToInventory()
        {
            Character.Inventory.ItemsList.Add(NewArmor);
            NewArmor = new Armor();
            IsInventoryDialogOpen = false;
            OnPropertyChanged(nameof(NewArmor));
            OnPropertyChanged(nameof(IsInventoryDialogOpen));
        }

        public void Handle(WearableEquippedEvent message)
        {
            foreach (Item item in Character.Inventory.ItemsList)
            {
                if (item is WearableItem wearable )
                {
                    if (item is Armor armor && message.IsEquipped)
                    {
                        if (message.IsEquipped)
                        {
                            EquippedArmor = armor;
                            EquippedArmor.IsWorn = true;
                            OnPropertyChanged(nameof(EquippedArmor));
                        }

                        if (message.IsEquipped == false)
                        {
                            EquippedArmor = new Armor();
                            OnPropertyChanged(nameof(EquippedArmor));

                        }

                    }

                    if (wearable == message.Item)
                    {
                        if (message.IsEquipped)
                        {
                            wearable.IsWorn = true;
                            
                        }

                        if (message.IsEquipped == false)
                        {
                            wearable.IsWorn = false;

                        }
                    }
                }

                
            }
            
        }
    }
}
