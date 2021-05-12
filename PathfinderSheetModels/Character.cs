using System;
using System.Collections.ObjectModel;


namespace PathfinderSheetModels
{

    [Serializable]
    public class Character : ObservableObject
    {
        public Character(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            
        }

        public EventAggregator EventAggregator { get; set; }
        public string Name { get; set; }
        public long Experience { get; set; }
        public int Level { get; set; }
        public Race Race { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public Size Size { get; set; }
        public ArmorClass ArmorClass { get; set; } 
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
        public ObservableCollection<GeneralFeat> CharacterFeats { get; } = new ObservableCollection<GeneralFeat>();
        public ObservableCollection<Attack> AttackList { get; } = new ObservableCollection<Attack>();

       
        
    }
}

