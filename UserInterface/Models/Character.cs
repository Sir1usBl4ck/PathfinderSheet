using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using UserInterface.EventModels;
using UserInterface.Views;


namespace UserInterface.Models
{

    [Serializable]
    public class Character : ObservableObject, IHandle<AbilityChangedEvent>
    {

        private string _name;
        private string _campaign;
        private long _experience;
        private int _level;
        private int _baseAttackBonus;
        private int _pointsLeft;
        private Race _race;
        private ExperienceProgression _experienceProgression;
        private EventAggregator _eventAggregator;
        private Dictionary<int, int> _pointBuyCost = new Dictionary<int, int>()
        {
            {7,-4},{8,-2},{9,-1},{10,0},{11,1},{12,2},{13,3},{14,5},{15,7},{16,10},{17,13},{18,17}
        };
        private CClass _cClass;

        //--Constructor
        public Character(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Slow));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Medium));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Fast));
            ExperienceProgression = ExperienceProgressionList[1];
            
            
            Abilities.Add(new Ability("Strength", AbilityType.Strenght, _eventAggregator));
            Abilities.Add(new Ability("Dexterity", AbilityType.Dexterity, _eventAggregator));
            Abilities.Add(new Ability("Constitution", AbilityType.Constitution, _eventAggregator));
            Abilities.Add(new Ability("Intelligence", AbilityType.Intelligence, _eventAggregator));
            Abilities.Add(new Ability("Wisdom", AbilityType.Wisdom, _eventAggregator));
            Abilities.Add(new Ability("Charisma", AbilityType.Charisma, _eventAggregator));

            //Skills
            Skills.Add(new Skill("Acrobatics", false, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Appraise", false, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Bluff", false, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Climb", false, Abilities[00], true, _eventAggregator));
            Skills.Add(new Skill("Craft", false, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Diplomacy", false, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Disable Device", true, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Disguise", false, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Escape Artist", false, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Fly", false, Abilities[01], true ,_eventAggregator));
            Skills.Add(new Skill("Handle Animal", true, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Heal", false, Abilities[04], _eventAggregator));
            Skills.Add(new Skill("Intimidate", false, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (arcana)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (dungeoneering)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (engineering)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (geography)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (history)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (local)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (nature)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (nobility)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (planes)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Kwnoledge (religion)", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Linguistics", true, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Perception", false, Abilities[04], _eventAggregator));
            Skills.Add(new Skill("Perform", false, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Profession", true, Abilities[05], _eventAggregator));
            Skills.Add(new Skill("Ride", false, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Sense Motive", false, Abilities[04], _eventAggregator));
            Skills.Add(new Skill("Sleight of Hand", true, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Spellcraft", false, Abilities[03], _eventAggregator));
            Skills.Add(new Skill("Stealth", false, Abilities[01], true, _eventAggregator));
            Skills.Add(new Skill("Survival", false, Abilities[04], _eventAggregator));
            Skills.Add(new Skill("Swim", false, Abilities[00], true, _eventAggregator));
            Skills.Add(new Skill("Use Magic Device", true, Abilities[05], _eventAggregator));

            Saves.Add(new Save(SaveType.Fortitude, Abilities[02], "FOR", _eventAggregator));
            Saves.Add(new Save(SaveType.Reflexes, Abilities[01], "REF", _eventAggregator));
            Saves.Add(new Save(SaveType.Willpower, Abilities[04], "WILL", _eventAggregator));
            BabProgress = 1;
            PointsLeft = 20;
            Level = 1;



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

        #region Race and Class

        public Race Race
        {
            get => _race;
            set
            {
                _race = value;
                OnPropertyChanged();
                _eventAggregator.Publish(new RaceChangedEvent());
            }
        }

        public CClass CClass
        {
            get => _cClass;
            set
            {
                _cClass = value;
                _eventAggregator.Publish(new CClassChangedEvent());
            }
        }

        #endregion



        #region Levelling Stuff

        public long Experience
        {
            get
            {
                return _experience;

            }
            set
            {
                _experience = value;
                LevelUp();
                OnPropertyChanged();
                OnPropertyChanged("Level");
            }
        }

        public ExperienceProgression ExperienceProgression
        {
            get => _experienceProgression;
            set
            {
                _experienceProgression = value;
                OnPropertyChanged();
                SetExperience();
                OnPropertyChanged("Experience");
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                PublishLevelChanged();
                SetExperience();
                OnPropertyChanged();
                SetBab();
                OnPropertyChanged("BaseAttackBonus");
            }
        }

        public int PointsLeft
        {
            get => _pointsLeft;
            set
            {
                _pointsLeft = value;
                OnPropertyChanged();
            }
        }

        #endregion


        
        #region Combat Stuff

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

        #endregion


        #region Collections

        public ObservableCollection<Ability> Abilities { get; } = new ObservableCollection<Ability>();
        public ObservableCollection<ExperienceProgression> ExperienceProgressionList { get; } = new ObservableCollection<ExperienceProgression>();
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();

        #endregion

        //----Methods

        public void SetBab()
        {
            double dBaseAttackBonus = Level * BabProgress;
            BaseAttackBonus = (int)Math.Floor(dBaseAttackBonus);
        }

        public void SetExperience()
        {
            Experience = ExperienceProgression.ExperienceTable[Level];
        }

        public void LevelUp()
        {
            var xpToLevel = ExperienceProgression.ExperienceTable[Level];
            var experience = Experience;

            if (experience > xpToLevel)
            {
                while (experience > xpToLevel)
                {
                    Level++;
                    xpToLevel = ExperienceProgression.ExperienceTable[Level];
                }
            }
            else
            {
                while (experience < xpToLevel)
                {
                    Level--;
                    xpToLevel = ExperienceProgression.ExperienceTable[Level];
                }
            }
            
        }

        //--- Handlers

        public void Handle(AbilityChangedEvent message)
        {
            var ability = message.Ability;
            var oldPointCost = ability.PointCost;
            ability.PointCost = _pointBuyCost[ability.BaseScore];
            PointsLeft -= ability.PointCost - oldPointCost;


        }

        //--- Publisher
        public void PublishLevelChanged()
        {
            _eventAggregator.Publish(new LevelChangedEvent(Level));
        }
    }
}

