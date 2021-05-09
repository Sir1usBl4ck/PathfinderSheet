using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UserInterface.EventModels;
using UserInterface.Models.Modifiers;
using UserInterface.Services;

namespace UserInterface.Models
{
    public class ArmorClass :ObservableObject,IBonusable, IHandle<AbilityChangedEvent>, IHandle<BonusListChangedEvent>
    {
        private int _total;
        private int _dexterityModifier;
        private ObservableCollection<Bonus> _bonusList;
        private int _flatFooted;
        private int _touch;
        private EventAggregator _eventAggregator;

        public EventAggregator EventAggregator
        {
            get { return _eventAggregator; }    
            set { _eventAggregator = value; }
        }
        
        public int Total
        {
            get => _total;
            set
            {
                _total = value;
                _total = 10 + DexterityModifier + BonusList.Sum(a => a.Value);
                OnPropertyChanged();

            }
        }

        public int DexterityModifier
        {
            get => _dexterityModifier;
            set
            {
                _dexterityModifier = value;
                OnPropertyChanged();
                OnPropertyChanged("Total");
            }
        }

        public ObservableCollection<Bonus> BonusList
        {
            get => _bonusList;
            set
            {
                _bonusList = value;
                OnPropertyChanged();
                OnPropertyChanged("Total");
            }
        }

        public string Name { get; set; } = "ArmorClass";

        public int Touch
        {
            get => _touch;
            set
            {
                _touch = value;
                var touchList =
                    BonusList.Where(b => (b.BonusType == BonusType.Deflection)).
                        Where(b => b.BonusType == BonusType.Dodge);
                _touch = 10 + touchList.Sum(a => a.Value);
                OnPropertyChanged();
            }
        }

        public int FlatFooted
        {
            get => _flatFooted;
            set
            {
                var flatFootedList = BonusList
                        .Where(b => (b.BonusType == BonusType.Deflection))
                        .Where(b => b.BonusType == BonusType.Armor)
                        .Where(b => b.BonusType == BonusType.Shield)
                        .Where(b => b.BonusType == BonusType.NaturalArmor);
                _flatFooted = 10 + flatFootedList.Sum(a => a.Value);
                OnPropertyChanged();
            }
        }

        public ArmorClass(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            BonusList = new ObservableCollection<Bonus>();
        }

        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.Type == AbilityType.Dexterity)
            {
                DexterityModifier = message.Ability.Modifier;
            }

        }

        public void Handle(BonusListChangedEvent message)
        {
           
        }
    }
}
