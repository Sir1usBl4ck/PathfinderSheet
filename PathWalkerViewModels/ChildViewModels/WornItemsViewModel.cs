using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PathfinderSheetModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class WornItemsViewModel : ChildViewModel
    {
        private InventorySlotViewModel _armorSlot;
        private InventorySlotViewModel _shieldSlot;
        private InventorySlotViewModel _eyesSlot;
        private InventorySlotViewModel _shouldersSlot;
        private InventorySlotViewModel _bodySlot;
        private InventorySlotViewModel _handsSlot;
        private InventorySlotViewModel _ring1Slot;
        private InventorySlotViewModel _ring2Slot;
        private InventorySlotViewModel _neckSlot;
        private InventorySlotViewModel _headSlot;
        private InventorySlotViewModel _headbandSlot;
        private InventorySlotViewModel _chestSlot;
        private InventorySlotViewModel _beltSlot;
        private InventorySlotViewModel _armsSlot;
        private InventorySlotViewModel _feetSlot;
        private int _wornItemsWeight;


        public WornItemsViewModel(Character character, EventAggregator eventAggregator)
        {

            EventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            Character = character;

            ArmorSlot = new InventorySlotViewModel(ItemSlot.Armor, Character, EventAggregator);
            ShieldSlot = new InventorySlotViewModel(ItemSlot.Shield, Character, EventAggregator);
            EyesSlot = new InventorySlotViewModel(ItemSlot.Eyes, Character, EventAggregator);
            ShouldersSlot = new InventorySlotViewModel(ItemSlot.Shoulders, Character, EventAggregator);
            BeltSlot = new InventorySlotViewModel(ItemSlot.Belt, Character, EventAggregator);
            BodySlot = new InventorySlotViewModel(ItemSlot.Body, Character, EventAggregator);
            HandsSlot = new InventorySlotViewModel(ItemSlot.Hands, Character, EventAggregator);
            Ring1Slot = new InventorySlotViewModel(ItemSlot.Ring1, Character, EventAggregator);
            Ring2Slot = new InventorySlotViewModel(ItemSlot.Ring2, Character, EventAggregator);
            NeckSlot = new InventorySlotViewModel(ItemSlot.Neck, Character, EventAggregator);
            HeadSlot = new InventorySlotViewModel(ItemSlot.Head, Character, EventAggregator);
            HeadbandSlot = new InventorySlotViewModel(ItemSlot.Headband, Character, EventAggregator);
            ChestSlot = new InventorySlotViewModel(ItemSlot.Chest, Character, EventAggregator);
            ArmsSlot = new InventorySlotViewModel(ItemSlot.Arms, Character, EventAggregator);
            FeetSlot = new InventorySlotViewModel(ItemSlot.Feet, Character, EventAggregator);
        }

        public bool IsInventoryDialogOpen { get; set; }

        public int WornItemsWeight
        {
            get => _wornItemsWeight;
            set
            {
                _wornItemsWeight = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel ArmorSlot
        {
            get { return _armorSlot; }
            set
            {
                
                _armorSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel ShieldSlot
        {
            get { return _shieldSlot; }
            set
            {
                _shieldSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel EyesSlot
        {
            get { return _eyesSlot; }
            set
            {
                _eyesSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel ShouldersSlot
        {
            get { return _shouldersSlot; }
            set
            {
                _shouldersSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel BodySlot
        {
            get { return _bodySlot; }
            set
            {
                _bodySlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel HandsSlot
        {
            get { return _handsSlot; }
            set
            {
                _handsSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel Ring1Slot
        {
            get { return _ring1Slot; }
            set
            {
                _ring1Slot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel Ring2Slot
        {
            get { return _ring2Slot; }
            set
            {
                _ring2Slot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel NeckSlot
        {
            get { return _neckSlot; }
            set
            {
                _neckSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel HeadSlot
        {
            get { return _headSlot; }
            set
            {
                _headSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel HeadbandSlot
        {
            get { return _headbandSlot; }
            set
            {
                _headbandSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel ChestSlot
        {
            get { return _chestSlot; }
            set
            {
                _chestSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel BeltSlot
        {
            get { return _beltSlot; }
            set
            {
               _beltSlot = value;
               OnPropertyChanged();
            }
        }

        public InventorySlotViewModel ArmsSlot
        {
            get { return _armsSlot; }
            set
            {
                _armsSlot = value;
                OnPropertyChanged();
            }
        }

        public InventorySlotViewModel FeetSlot
        {
            get { return _feetSlot; }
            set
            {
                _feetSlot = value;
                OnPropertyChanged();
            }
        }
    }
}
