﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using UserInterface.Data;
using UserInterface.EventModels;


namespace UserInterface.Models
{

    [Serializable]
    public class Character : ObservableObject, IHandle<AbilityChangedEvent>, IHandle<SkillChangedEvent>
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
            { 7, -4 },
            { 8, -2 },
            { 9, -1 },
            { 10, 0 },
            { 11, 1 },
            { 12, 2 },
            { 13, 3 },
            { 14, 5 },
            { 15, 7 },
            { 16, 10 },
            { 17, 13 },
            { 18, 17 }
        };
        private CharacterClass _characterClass;
        private int _maxHitPoints;
        private int _wounds;
        private int _nonLethalDamage;
        private Size _size;
        private Attack _attack1;
        private Attack _attack2;
        private Attack _attack3;

        public Character(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Slow));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Medium));
            ExperienceProgressionList.Add(new ExperienceProgression(Progression.Fast));
            ExperienceProgression = ExperienceProgressionList[1];
            
            Attack1 = new Attack(_eventAggregator);
            Attack2 = new Attack(_eventAggregator);
            Attack3 = new Attack(_eventAggregator);

            //test

            Attack1.Name = "TestAttack";
            Attack1.DiceNumber = 1;
            Attack1.Dice = 6;
            Attack1.CriticalMultiplier = 2;
            


            AttackList.Add(Attack1);
            AttackList.Add(Attack2);
            AttackList.Add(Attack3);
            foreach (var attack in AttackList)
            {
                attack.EventAggregator.Subscribe(attack);
            }
            
            Serializer serializer = new Serializer();

            Abilities = serializer.LoadCollection<Ability>("Abilities");
            foreach (var ability in Abilities)
            {
                ability.EventAggregator = _eventAggregator;
                ability.EventAggregator.Subscribe(ability);
            }

            Skills = serializer.LoadCollection<Skill>("Skills");
            foreach (var skill in Skills)
            {
                skill.EventAggregator = _eventAggregator;
                skill.EventAggregator.Subscribe(skill);
            }

            Saves = serializer.LoadCollection<Save>("Saves");
            foreach (var save in Saves)
            {
                save.EventAggregator = _eventAggregator;
                save.EventAggregator.Subscribe(save);
            }

            BonusList = new ObservableCollection<Bonus>();
            BonusList.Add(new Bonus { BonusSource = "Armor", BonusType = BonusType.Armor, IsStackable = true, Value = 3 });
            ArmorClass = new ArmorClass(_eventAggregator);

            Size = new Size(SizeType.Medium);
            CharacterClass = new CharacterClass();
            PointsLeft = 20;
            Level = 1;
            Name = "NewCharacter";
            Campaign = "NewCampaign";
            MaxHitPoints = 0;
            CurrentHitPoints = 0;
            Wounds = 0;
            NonLethalDamage = 0;

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
        public Race Race
        {
            get => _race;
            set
            {
                _race = value;
                OnPropertyChanged();
                _eventAggregator.Publish(new RaceChangedEvent(_race));
            }
        }
        public Size Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }
        public CharacterClass CharacterClass
        {
            get => _characterClass;
            set
            {
                _characterClass = value;
                OnPropertyChanged();
                UpdateClassSkills(_characterClass);
                _eventAggregator.Publish(new CharacterClassChangedEvent(_characterClass));
            }
        }
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
                RollHitPoints(Level);
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
        private int _availableSkillRanks;
        private ObservableCollection<Bonus> _bonusList;

        public int AvailableSkillRanks
        {
            get
            {
                return _availableSkillRanks;
            }
            set
            {
                _availableSkillRanks = value;
                PublishAvailableSkillRanksChanged();
                OnPropertyChanged();
            }
        }
        public int MaxHitPoints
        {
            get => _maxHitPoints;
            set
            {
                _maxHitPoints = value;
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
                _eventAggregator.Publish(new BaseAttackBonusChangedEvent(_baseAttackBonus));
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
        public ObservableCollection<Ability> Abilities { get; } = new ObservableCollection<Ability>();
        public ObservableCollection<ExperienceProgression> ExperienceProgressionList { get; } = new ObservableCollection<ExperienceProgression>();
        public ObservableCollection<Skill> Skills { get; } = new ObservableCollection<Skill>();
        public ObservableCollection<Save> Saves { get; } = new ObservableCollection<Save>();
        public ObservableCollection<Spell> CharacterSpells { get; } = new ObservableCollection<Spell>();
        public ObservableCollection<GeneralFeat> CharacterFeats { get; } = new ObservableCollection<GeneralFeat>();
        public ObservableCollection<Attack> AttackList { get; } = new ObservableCollection<Attack>();
        private ArmorClass _armorClass;
        private long _xpToLevel;

        public ArmorClass ArmorClass
        {
            get { return _armorClass; }
            set
            {
                _armorClass = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Bonus> BonusList
        {
            get => _bonusList;
            set
            {
                _bonusList = value;
                _eventAggregator.Publish(new BonusListChangedEvent(_bonusList));
            }
        } 

        public Attack Attack1
        {
            get => _attack1;
            set
            {
                _attack1 = value;
                OnPropertyChanged();
            }
        }

        public Attack Attack2
        {
            get => _attack2;
            set
            {
                _attack2 = value;
                OnPropertyChanged();
            }
        }

        public Attack Attack3
        {
            get => _attack3;
            set
            {
                _attack3 = value;
                OnPropertyChanged();
            }
        }

        private long _oldXp;

        public long OldXp
        {
            get => _oldXp;
            set
            {
                _oldXp = value;
                OnPropertyChanged();
            }
        }


        public long XpToLevel
        {
            get => _xpToLevel;
            set
            {
               _xpToLevel = value;
                OnPropertyChanged();
            }
        }


        public int RollHitPoints(int level)
        {
            IDice dice = new Dice();
            dice.Dice(CharacterClass.HitDice);
            DiceResult result = dice.Roll(new RandomDieRoller());
            int constitutionModifier = Abilities.FirstOrDefault(a => a.Type == AbilityType.Constitution).Modifier;
            List<int> rolledLevels = new List<int>();

            if (level > 1)
            {
                _maxHitPoints = 0;
                for (int i = 1; i <= level; i++)
                {
                    if (result.Value > (CharacterClass.HitDice / 2))
                    {
                        rolledLevels.Add(result.Value + constitutionModifier);
                    }
                    rolledLevels.Add((CharacterClass.HitDice / 2) + constitutionModifier);
                }

                var rolledLevelSum = rolledLevels.Sum();
                return rolledLevelSum + CharacterClass.HitDice;
            }
            return CharacterClass.HitDice + constitutionModifier;
        }
        public void SetBab()
        {
            if (CharacterClass != null) //this is ugly, refactor somehow
            {
                double dBaseAttackBonus = Level * CharacterClass.BaBProgression;
                BaseAttackBonus = (int)Math.Floor(dBaseAttackBonus);
            }
        }
        public void SetExperience()
        {
            Experience = ExperienceProgression.ExperienceTable[Level];
            XpToLevel = ExperienceProgression.ExperienceTable[Level+1];
        } //Move to Experience Class?
        public void UpdateCharacterClassSaves()
        {
            foreach (var save in Saves)
            {
                if (CharacterClass.GoodSave.SaveType == save.SaveType)
                {
                    save.IsGood = true;
                    save.SetBonus();
                }
                else
                {
                    save.IsGood = false;
                    save.SetBonus();
                }

            }
        }
        public void UpdateClassSkills(CharacterClass characterClass)
        {
            foreach (var skill in Skills)
                if (characterClass.ClassSkillNames.Contains(skill.Name))
                    skill.IsClass = true;
        }
        public void ApplyRaceSize()
        {


            Skills.FirstOrDefault(a => a.Name.Equals("Stealth")).SizeModifier = Size.StealthSizeModifier;
            Skills.FirstOrDefault(a => a.Name.Equals("Fly")).SizeModifier = Size.FlySizeModifier;


        }
        public void UpdateAbilities()
        {
            foreach (var ability in Abilities)
            {
                if (Race != null) //this sucks, change it somehow
                {
                    var raceBonus = Race.ModifiedAbilities.FirstOrDefault(a => a.Type == ability.Type);

                    if (raceBonus != null) //this sucks too.
                        ability.Score = ability.BaseScore + raceBonus.Bonus + ability.Bonus;
                    else
                        ability.Score = ability.BaseScore + ability.Bonus;
                }
            }
        }
        public void UpdateAvailableSkillRanks()
        {
            var intModifier = Abilities.FirstOrDefault(a => a.Type == AbilityType.Intelligence).Modifier;
            var totalRanks = Skills.Sum(value => value.Rank);

            AvailableSkillRanks = (CharacterClass.SkillRanksPerLevel + intModifier) * Level - totalRanks;
            OnPropertyChanged(nameof(AvailableSkillRanks));
        }
        public void UpdateExperienceTab()
        {
           
            var experience = Experience;

            if (experience > XpToLevel)
            {
                while (experience > XpToLevel)
                {
                    Level++;
                    XpToLevel = ExperienceProgression.ExperienceTable[Level];
                }
            }
            else
            {
                while (experience < XpToLevel)
                {
                    Level--;
                    XpToLevel = ExperienceProgression.ExperienceTable[Level];
                }
            }
        }
        public void LevelUp()
        {
            if (Level > 1)
            {
                OldXp = XpToLevel;
                UpdateExperienceTab();
                UpdateAvailableSkillRanks();
            }


        } // Move to ViewModel
        public void Handle(AbilityChangedEvent message)
        {
            var ability = message.Ability;                          //Move 
            var oldPointCost = ability.PointCost;               //All
            ability.PointCost = _pointBuyCost[ability.BaseScore];   //This
            PointsLeft -= ability.PointCost - oldPointCost;         //to Ability?
            UpdateAbilities();
            UpdateAvailableSkillRanks();
            OnPropertyChanged(nameof(AvailableSkillRanks));
            OnPropertyChanged("Initiative");


        }
        public void PublishLevelChanged()
        {
            _eventAggregator.Publish(new LevelChangedEvent(Level));
        }
        public void PublishAvailableSkillRanksChanged()
        {
            _eventAggregator.Publish(new AvailableSkillRanksChanged(AvailableSkillRanks));

        }
        public void Handle(SkillChangedEvent message)
        {
            AvailableSkillRanks -= message.Skill.Rank;

            OnPropertyChanged(nameof(AvailableSkillRanks));
        }
    }
}

