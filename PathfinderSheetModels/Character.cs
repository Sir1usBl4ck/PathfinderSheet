using System;
using System.Collections.ObjectModel;


namespace PathfinderSheetModels
{

    [Serializable]
    public class Character : ObservableObject
    {
        private Race _race;
        private string _name;
        private CharacterClass _characterClass;

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
        public int Level { get; set; }
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
        public int MaxHitPoints { get; set; }
        public int TotalHitPoints { get; set; }
        public int Wounds { get; set; }
        public int NonLethalDamage { get; set; }
        public int Initiative { get; set; }
        public int BaseAttackBonus { get; set; }
        public int CombatManeuverBonus { get; set; }
        public int CombatManeuverDefense { get; set; }

        public ObservableCollection<Ability> Abilities { get; set; } = new ObservableCollection<Ability>();
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();
        public ObservableCollection<Spell> KnownSpells { get; } = new ObservableCollection<Spell>();
        public ObservableCollection<Spell> PreparedSpells { get; set; } = new ObservableCollection<Spell>();
        public ObservableCollection<GeneralFeat> Feats { get; } = new ObservableCollection<GeneralFeat>();
        public ObservableCollection<Attack> AttackList { get; } = new ObservableCollection<Attack>();

       
        
    }
}

