using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class InventorySlotViewModel : ChildViewModel
    {
        private Item _item;

        public InventorySlotViewModel(ItemSlot slot, Character character, EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
            Character = character;
            Slot = slot;
            WearItemCommand = new RelayCommand<WearableItem>(WearItem);
            UnEquipItemCommand = new RelayCommand<WearableItem>(UnEquipItem);
            _wearableItems = new CollectionViewSource{ Source=Character.Inventory.ItemsList}.View;
            _wearableItems.Filter += WearableFilter;
        }

        public bool WearableFilter(object obj)
        {
            if (obj is WearableItem item)
            {
                if (item.Slot == Slot)
                {
                    return true;
                }
                    
            }

            return false;
        }
        private void WearItem(WearableItem obj)
        {
            if (obj != null)
            {
                Item = obj;
                IsEquipped = true;
                EventAggregator.Publish(new WearableEquippedEvent(_item, true));
                BonusService.ApplyBonus(obj.BonusList);
            }

        }

        private void UnEquipItem(WearableItem obj)
        {
            BonusService.RemoveBonus(obj.BonusList);
            EventAggregator.Publish(new WearableEquippedEvent(obj, false));
            Item = null;
            IsEquipped = false;

        }

        private ICollectionView _wearableItems;
        private bool _isEquipped;

        public ICommand WearItemCommand { get; set; }
        public ICommand UnEquipItemCommand { get; set; }

        public bool IsEquipped
        {
            get => _isEquipped;
            set
            {
                _isEquipped = value;
                OnPropertyChanged();
            }
        }

        public ItemSlot Slot { get; set; }
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView WearableItems
        {
            get
            {
                _wearableItems = new CollectionViewSource {Source = Character.Inventory.ItemsList}.View;
                _wearableItems.Filter += WearableFilter;
                return _wearableItems;
            }
        }
    }
}