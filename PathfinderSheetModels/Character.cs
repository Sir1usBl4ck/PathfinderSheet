using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;


namespace PathfinderSheetModels
{

    [Serializable]
    public class Character : ObservableObject
    {
        private Race _race;
        private string _name;
        private CharacterClass _characterClass;
        private int _wounds;
        private int _maxHitPoints;
        private int _baseAttackBonus;
        private int _nonLethalDamage;
        private int _level;

        public Character(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Level = 1;

        }

        public EventAggregator EventAggregator { get; set; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged();}
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                EventAggregator.Publish(new LevelChangedEvent(_level));
            }
        }

        public Race Race
        {
            get => _race;
            set { _race = value;
                EventAggregator.Publish(new RaceChangedEvent(_race));

            }
        }

        public CharacterClass CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                EventAggregator.Publish(new CharacterClassChangedEvent(_characterClass));
            }
        }

        public Size Size { get; set; }

        public ArmorClass ArmorClass { get; set; }

        public int MaxHitPoints
        {
            get => _maxHitPoints;
            set
            {
                _maxHitPoints = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int CurrentHitPoints => MaxHitPoints - Wounds;

        public int Wounds
        {
            get => _wounds;
            set
            {
                _wounds = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int NonLethalDamage
        {
            get => _nonLethalDamage;
            set
            {
                _nonLethalDamage = value; 
                OnPropertyChanged();
            }
        }

        public int Initiative { get; set; }

        public int BaseAttackBonus
        {
            get => _baseAttackBonus;
            set
            {
                _baseAttackBonus = value;
                EventAggregator.Publish(new BaseAttackBonusChangedEvent(BaseAttackBonus));
            }

        }

        public long Experience { get; set; }

        public int CombatManeuverBonus { get; set; }

        public int CombatManeuverDefense { get; set; }

        public Inventory Inventory { get; set; }

        public ObservableCollection<Ability> Abilities { get; set; } = new ObservableCollection<Ability>();

        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();

        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();

        public ObservableCollection<Spell> KnownSpells { get; set; } = new ObservableCollection<Spell>();

        public ObservableCollection<Spell> PreparedSpells { get; set; } = new ObservableCollection<Spell>();

        public ObservableCollection<SpecialAbility> SpecialAbilities { get;} =
            new ObservableCollection<SpecialAbility>();

        public ObservableCollection<GeneralFeat> Feats { get; } = new ObservableCollection<GeneralFeat>();

        public ObservableCollection<Attack> AttackList { get; } = new ObservableCollection<Attack>();

       
        
    }
}

