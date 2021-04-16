using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using UserInterface.Data;
using UserInterface.EventModels;


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
        private int _currentHitPoints;
        private Race _race;
        private ExperienceProgression _experienceProgression;
        private EventAggregator _eventAggregator;
        private Dictionary<int, int> _pointBuyCost = new()
        {
            {7,-4},{8,-2},{9,-1},{10,0},{11,1},{12,2},{13,3},{14,5},{15,7},{16,10},{17,13},{18,17}
        };
        private CharacterClass _CharacterClass;
        private int _MaxHitPoints;
        private int _wounds;
        private int _nonLethalDamage;


        //--Constructor
        public Character(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Slow));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Medium));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Fast));
            ExperienceProgression = ExperienceProgressionList[1];


            Abilities.Add(new Ability("Strength", AbilityType.Strength, _eventAggregator));
            Abilities.Add(new Ability("Dexterity", AbilityType.Dexterity, _eventAggregator));
            Abilities.Add(new Ability("Constitution", AbilityType.Constitution, _eventAggregator));
            Abilities.Add(new Ability("Intelligence", AbilityType.Intelligence, _eventAggregator));
            Abilities.Add(new Ability("Wisdom", AbilityType.Wisdom, _eventAggregator));
            Abilities.Add(new Ability("Charisma", AbilityType.Charisma, _eventAggregator));
            Serializer serializer = new Serializer();



            //Skills.Add(new Skill("Acrobatics", false, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Appraise", false, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Bluff", false, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Climb", false, AbilityType.Strength, true, _eventAggregator));
            //Skills.Add(new Skill("Craft", false, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Diplomacy", false, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Disable Device", true, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Disguise", false, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Escape Artist", false, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Fly", false, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Handle Animal", true, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Heal", false, AbilityType.Intelligence, _eventAggregator));
            //Skills.Add(new Skill("Intimidate", false, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (arcana)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (dungeoneering)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (engineering)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (geography)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (history)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (local)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (nature)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (nobility)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (planes)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Knowledge (religion)", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Linguistics", true, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Perception", false, AbilityType.Intelligence, _eventAggregator));
            //Skills.Add(new Skill("Perform", false, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Profession", true, AbilityType.Wisdom, _eventAggregator));
            //Skills.Add(new Skill("Ride", false, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Sense Motive", false, AbilityType.Intelligence, _eventAggregator));
            //Skills.Add(new Skill("Sleight of Hand", true, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Spellcraft", false, AbilityType.Constitution, _eventAggregator));
            //Skills.Add(new Skill("Stealth", false, AbilityType.Dexterity, true, _eventAggregator));
            //Skills.Add(new Skill("Survival", false, AbilityType.Intelligence, _eventAggregator));
            //Skills.Add(new Skill("Swim", false, AbilityType.Strength, true, _eventAggregator));
            //Skills.Add(new Skill("Use Magic Device", true, AbilityType.Wisdom, _eventAggregator));
            //serializer.SerializeSkills(Skills);

            Skills = serializer.LoadSkills();
            foreach (var skill in Skills)
            {
                skill.EventAggregator = _eventAggregator;
                skill.EventAggregator.Subscribe(skill);
            }


            //Saves.Add(new Save(SaveType.Fortitude, AbilityType.Constitution, "FOR", _eventAggregator));
            //Saves.Add(new Save(SaveType.Reflexes, AbilityType.Dexterity, "REF", _eventAggregator));
            //Saves.Add(new Save(SaveType.Willpower, AbilityType.Intelligence, "WILL", _eventAggregator));
            PointsLeft = 20;
            Level = 1;
            Name = "NewCharacter";
            Campaign = "NewCampaign";
            Wounds = 0;
            NonLethalDamage = 0;

        }

        #region General

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

        #endregion

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
        public CharacterClass CharacterClass
        {
            get => _CharacterClass;
            set
            {
                _CharacterClass = value;
                _eventAggregator.Publish(new CharacterClassChangedEvent());
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
                SetBab();
                OnPropertyChanged();
                OnPropertyChanged("BaseAttackBonus");
                OnPropertyChanged("MaxHitPoints");
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

        public int MaxHitPoints
        {
            get => RollHitPoints(Level);
            set
            {
               _MaxHitPoints = RollHitPoints(Level);
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints
        {
            get { return MaxHitPoints - Wounds; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged();
                
            }
        }

        public int Wounds
        {
            get => _wounds;
            set
            {
                _wounds = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentHitPoints");
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

        public int BaseAttackBonus
        {
            get => _baseAttackBonus;
            set
            {
                _baseAttackBonus = value;
                OnPropertyChanged();
            }
        }
        public int Initiative
        {
            get => Abilities.FirstOrDefault(a => a.Type == AbilityType.Dexterity).Modifier;
        }
        public int CombatManeuverBonus => (Abilities.FirstOrDefault(a => a.Type == AbilityType.Strength).Modifier + BaseAttackBonus);
        public int CombatManeuverDefense =>
            Abilities.FirstOrDefault(a => a.Type == AbilityType.Strength).Modifier +
            Abilities.FirstOrDefault(a => a.Type == AbilityType.Dexterity).Modifier +
            BaseAttackBonus;

        #endregion

        #region Collections

        public ObservableCollection<Ability> Abilities { get; } = new ObservableCollection<Ability>();
        public ObservableCollection<ExperienceProgression> ExperienceProgressionList { get; } = new ObservableCollection<ExperienceProgression>();
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();

        #endregion

        #region Methods


        //foreach level above 1 roll a dice and add bonuses, add the result to the array of RolledLevels
        // Level = HitDice + sum RolledLevels
        public int RollHitPoints(int level)
        {
            IDice dice = new Dice();
            dice.Dice(CharacterClass.HitDice);
            DiceResult result = dice.Roll(new RandomDieRoller());
            int constitutionModifier = Abilities.FirstOrDefault(a => a.Type == AbilityType.Constitution).Modifier;
            List<int> rolledLevels = new List<int>();

            if (level > 1)
            {
                _MaxHitPoints= 0;
                for (int i = 1; i <= level; i++)
                {
                    if (result.Value > (CharacterClass.HitDice/2))
                    {
                        rolledLevels.Add(result.Value + constitutionModifier);
                    }
                    rolledLevels.Add((CharacterClass.HitDice/2) + constitutionModifier);
                }

                var rolledLevelSum = rolledLevels.Sum();
                return rolledLevelSum + CharacterClass.HitDice ;
            }
            return CharacterClass.HitDice + constitutionModifier;
        }

        public void SetBab()
        {
            if (CharacterClass!=null) //this is ugly, refactor somehow
            {
                double dBaseAttackBonus = Level * CharacterClass.BaBProgression;
                BaseAttackBonus = (int)Math.Floor(dBaseAttackBonus);
            }
        } 
        public void SetExperience()
        {
            Experience = ExperienceProgression.ExperienceTable[Level];
        } //Move to Experience Class?
        public void UpdateCharacterClassSaves()
        {
                foreach (var save in Saves)
                {
                    if (CharacterClass.GoodSave.SaveType == save.SaveType)
                    {
                        save.IsGood = true;
                    }
                }
        } 
        public void UpdateClassSkills(CharacterClass characterClass)
        {
            foreach (var skill in Skills)
                if (characterClass.ClassSkillNames.Contains(skill.Name))
                    skill.IsClass = true;
        }
        public void UpdateRaceAbilities()
        {
            foreach (var ability in Abilities)
            {
                if (Race!=null) //this sucks, change it somehow
                {
                    var raceBonus = Race.ModifiedAbilities.FirstOrDefault(a => a.Type == ability.Type);

                    if (raceBonus != null) //this sucks too.
                        ability.Score = ability.BaseScore + raceBonus.Bonus;
                    else
                        ability.Score = ability.BaseScore;
                }
            }
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

        } // Move to ViewModel

        #endregion

        //--- Handlers

        public void Handle(AbilityChangedEvent message)
        {
            var ability = message.Ability;                          //Move 
            var oldPointCost = ability.PointCost;               //All
            ability.PointCost = _pointBuyCost[ability.BaseScore];   //This
            PointsLeft -= ability.PointCost - oldPointCost;         //to Ability?
            UpdateRaceAbilities();
            OnPropertyChanged("Initiative");
            

        }  

        //--- Publisher
        public void PublishLevelChanged()
        {
            _eventAggregator.Publish(new LevelChangedEvent(Level));
        }
    }
}

