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
            }
        }


        public Race Race { get; set; }
        public CharacterClass CharacterClass { get; set; }

        public float BabProgress { get; set; }
        public int CombatManeuversBonus { get; set; }
        public int CombatManeuversDefense { get; set; }


        public ObservableCollection<Ability> Abilities { get; } = new ObservableCollection<Ability>();
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();

        public Character()
        {
            
            Abilities.Add(new Ability("Strength",AbilityType.Strenght));
            Abilities.Add(new Ability("Dexterity",AbilityType.Dexterity));
            Abilities.Add(new Ability("Constitution", AbilityType.Constitution));
            Abilities.Add(new Ability("Intelligence", AbilityType.Intelligence));
            Abilities.Add(new Ability("Wisdom", AbilityType.Wisdom));
            Abilities.Add(new Ability("Charisma", AbilityType.Charisma));

            
            Skills.Add(new Skill("Acrobatics",false,Abilities[01],true));
            Skills.Add(new Skill("Appraise", false, Abilities[03]));
            Skills.Add(new Skill("Bluff", false, Abilities[05]));
            Skills.Add(new Skill("Climb", false, Abilities[00],true));
            Skills.Add(new Skill("Craft", false, Abilities[03]));
            Skills.Add(new Skill("Diplomacy", false, Abilities[05]));
            Skills.Add(new Skill("Disable Device", false, Abilities[01], true));
            Skills.Add(new Skill("Disguise", false, Abilities[05]));
            Skills.Add(new Skill("Escape Artist", false, Abilities[01], true));
            Skills.Add(new Skill("Fly", false, Abilities[01], true));
            Skills.Add(new Skill("Handle Animal", false, Abilities[05]));
            Skills.Add(new Skill("Heal", false, Abilities[04]));
            Skills.Add(new Skill("Intimidate", false, Abilities[05]));
            Skills.Add(new Skill("Kwnoledge (arcana)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (dungeoneering)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (engineering)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (geography)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (history)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (local)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (nature)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (nobility)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (planes)", false, Abilities[03]));
            Skills.Add(new Skill("Kwnoledge (religion)", false, Abilities[03]));
            Skills.Add(new Skill("Linguistics", false, Abilities[03]));
            Skills.Add(new Skill("Perception", false, Abilities[04]));
            Skills.Add(new Skill("Perform", false, Abilities[05]));
            Skills.Add(new Skill("Profession", false, Abilities[05]));
            Skills.Add(new Skill("Ride", false, Abilities[01],true));
            Skills.Add(new Skill("Sense Motive", false, Abilities[04]));
            Skills.Add(new Skill("Sleight of Hand", false, Abilities[01], true));
            Skills.Add(new Skill("Spellcraft", false, Abilities[03]));
            Skills.Add(new Skill("Stealth", false, Abilities[01],true));
            Skills.Add(new Skill("Survival", false, Abilities[04]));
            Skills.Add(new Skill("Swim", false, Abilities[00],true));
            Skills.Add(new Skill("Use Magic Device", false, Abilities[05]));
            
            Saves.Add(new Save(Abilities[02], SaveType.Fortitude));
            Saves.Add(new Save(Abilities[01], SaveType.Reflexes));
            Saves.Add(new Save(Abilities[04], SaveType.Willpower));

            

        }

        public void GetBab()
        {
            var floatLevel = (float) Level;
            var floatBab = (float) _baseAttackBonus*BabProgress;
            _baseAttackBonus = (int) floatBab;
        }
        
    }
}
