using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Views;


namespace UserInterface.Models
{
    [Serializable]
    public class Character : ObservableObject
    {   
        
        private string _name;
        private string _campaign;
        private int _experience;
        private int _level;
        private int _baseAttackBonus;


        public Character()
        {
            BabProgress = 1;
            Level = 1;

            Abilities.Add(new Ability("Strength", AbilityType.Strenght));
            Abilities.Add(new Ability("Dexterity", AbilityType.Dexterity));
            Abilities.Add(new Ability("Constitution", AbilityType.Constitution));
            Abilities.Add(new Ability("Intelligence", AbilityType.Intelligence));
            Abilities.Add(new Ability("Wisdom", AbilityType.Wisdom));
            Abilities.Add(new Ability("Charisma", AbilityType.Charisma));


            Skills.Add(new Skill("Acrobatics", false, Abilities[01], true));
            Skills.Add(new Skill("Appraise", false, Abilities[03]));
            Skills.Add(new Skill("Bluff", false, Abilities[05]));
            Skills.Add(new Skill("Climb", false, Abilities[00], true));
            Skills.Add(new Skill("Craft", false, Abilities[03]));
            Skills.Add(new Skill("Diplomacy", false, Abilities[05]));
            Skills.Add(new Skill("Disable Device", true, Abilities[01], true));
            Skills.Add(new Skill("Disguise", false, Abilities[05]));
            Skills.Add(new Skill("Escape Artist", false, Abilities[01], true));
            Skills.Add(new Skill("Fly", false, Abilities[01], true));
            Skills.Add(new Skill("Handle Animal", true, Abilities[05]));
            Skills.Add(new Skill("Heal", false, Abilities[04]));
            Skills.Add(new Skill("Intimidate", false, Abilities[05]));
            Skills.Add(new Skill("Kwnoledge (arcana)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (dungeoneering)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (engineering)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (geography)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (history)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (local)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (nature)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (nobility)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (planes)", true, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (religion)", true, Abilities[03]));
            Skills.Add(new Skill("Linguistics", true, Abilities[03]));
            Skills.Add(new Skill("Perception", false, Abilities[04]));
            Skills.Add(new Skill("Perform", false, Abilities[05]));
            Skills.Add(new Skill("Profession", true, Abilities[05]));
            Skills.Add(new Skill("Ride", false, Abilities[01], true));
            Skills.Add(new Skill("Sense Motive", false, Abilities[04]));
            Skills.Add(new Skill("Sleight of Hand", true, Abilities[01], true));
            Skills.Add(new Skill("Spellcraft", false, Abilities[03]));
            Skills.Add(new Skill("Stealth", false, Abilities[01], true));
            Skills.Add(new Skill("Survival", false, Abilities[04]));
            Skills.Add(new Skill("Swim", false, Abilities[00], true));
            Skills.Add(new Skill("Use Magic Device", true, Abilities[05]));

            Saves.Add(new Save(SaveType.Fortitude,Abilities[02]));
            Saves.Add(new Save(SaveType.Reflexes,Abilities[01]));
            Saves.Add(new Save(SaveType.Willpower,Abilities[04]));


        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Campaign
        {
            get => _campaign;
            set
            {
                _campaign = value;
                OnPropertyChanged();
            }
        }

        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
                GetBab();
                OnPropertyChanged("BaseAttackBonus");
            }
        }

        public double BabProgress { get; set; }

        public int BaseAttackBonus
        {
            get => _baseAttackBonus;
            set
            {
                _baseAttackBonus = value; 
                OnPropertyChanged();
            }
        }


        public Race Race { get; set; }
        public CClass CClass { get; set; }

        public int CombatManeuversBonus { get; set; }
        public int CombatManeuversDefense { get; set; }


        public ObservableCollection<Ability> Abilities { get; } = new ObservableCollection<Ability>();

        public int PointBuy { get; set; }
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();

       

        public void GetBab()
        {
            double dBaseAttackBonus = Level * BabProgress;
            BaseAttackBonus = (int) Math.Floor(dBaseAttackBonus);
        }
        
    }
}
