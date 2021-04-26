using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using MathNet.Numerics.LinearAlgebra.Solvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserInterface.Data;
using UserInterface.EventModels;
using UserInterface.Models;
using UserInterface.Services;
using UserInterface.Views;
using Size = UserInterface.Models.Size;
using Type = UserInterface.Models.Type;

namespace UserInterface.ViewModels
{
    [Serializable]
    public class PathFinderViewModel : BaseViewModel, IHandle<RaceChangedEvent>, IHandle<CharacterClassChangedEvent>
    {


        //EG:  1- Create private field for EventAggregator 
        private EventAggregator _eventAggregator;
        private CollectionViewSource _classSpellsView;
        private int _classLevelFilter;

        public Character Character { get; set; }
        public ObservableCollection<int> PossibleTotalPoints { get; } = new ObservableCollection<int>();
        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        public ObservableCollection<CharacterClass> CharacterClasses { get; } =
            new ObservableCollection<CharacterClass>();
        public ObservableCollection<Size> Sizes { get; } = new ObservableCollection<Size>();
        public ObservableCollection<Spell> Spells { get; } = new ObservableCollection<Spell>();
        public ObservableCollection<GeneralFeat> Feats { get; } = new ObservableCollection<GeneralFeat>();
        public ObservableCollection<DiceRoll> DiceRolls { get; set; } = new ObservableCollection<DiceRoll>();

        public CollectionViewSource ClassSpellView
        {
            get => _classSpellsView;
            set => _classSpellsView = value;
        }

        public int ClassLevelFilter
        {
            get
            {
                return _classLevelFilter;
            }
            set
            {
                _classLevelFilter = value;
                _classSpellsView?.View.Refresh();

                OnPropertyChanged();
            }
        }

        public RollService RollService { get; set; }


        public ICommand AddSpellToCharacterCommand { get; set; }
        public ICommand AddFeatToCharacterCommand { get; set; }
        public ICommand RollSkillCommand { get; set; }
        public ICommand RollAttackCommand { get; set; }
        public ICommand RollDamageCommand { get; set; }
        public ICommand RollCommand { get; set; }



        //Visibility Properties
        public bool IsEditAbilities { get; set; }
        public bool IsEditSkills { get; set; }




        //----Constructor
        public PathFinderViewModel()
        {

            //EG:  2- Instantiate the EventAggregator
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);
            RollService = new RollService();
            

            AddSpellToCharacterCommand = new RelayCommand<Spell>(AddSpellToCharacterExecute);
            AddFeatToCharacterCommand = new RelayCommand<GeneralFeat>(AddFeatToCharacterExecute);
            RollCommand = new RelayCommand<IRollable>(RollExecute);

            RollSkillCommand = new RelayCommand<Skill>(RollSkillExecute);
            RollAttackCommand = new RelayCommand<Attack>(RollAttackExecute);
            RollDamageCommand = new RelayCommand<Attack>(RollDamageExecute);

            PossibleTotalPoints.Add(15);
            PossibleTotalPoints.Add(20);
            PossibleTotalPoints.Add(25);


            Serializer serializer = new Serializer();
            Spells = serializer.LoadCollection<Spell>("Spells");

            Feats = serializer.LoadCollection<GeneralFeat>("Feats");

            //Races
            var norace = new Race("Select a Race", Type.Humanoid, SubType.Human, SizeType.Medium);

            var elf = new Race("Elf", Type.Humanoid, SubType.Elf, SizeType.Medium);
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Dexterity, 2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Intelligence, 2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution, -2));

            var dwarf = new Race("Dwarf", Type.Humanoid, SubType.Dwarf, SizeType.Medium);
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution, 2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Wisdom, 2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Charisma, -2));

            var gnome = new Race("Gnome", Type.Humanoid, SubType.Gnome, SizeType.Small);
            gnome.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution, 2));
            gnome.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Charisma, 2));
            gnome.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Strength, -2));

            Races.Add(norace);
            Races.Add(elf);
            Races.Add(dwarf);
            Races.Add(gnome);

            Sizes.Add(new Size(SizeType.Medium));
            Sizes.Add(new Size(SizeType.Small));
            Sizes.Add(new Size(SizeType.Large));

            var barbarian = new CharacterClass("Barbarian", 1, 12);
            barbarian.GoodSave = new Save(SaveType.Fortitude);
            barbarian.ClassSkillNames.Add("Acrobatics");
            barbarian.ClassSkillNames.Add("Climb");
            barbarian.ClassSkillNames.Add("Craft");
            barbarian.ClassSkillNames.Add("Handle Animal");
            barbarian.ClassSkillNames.Add("Intimidate");
            barbarian.ClassSkillNames.Add("Knowledge (nature)");
            barbarian.ClassSkillNames.Add("Perception");
            barbarian.ClassSkillNames.Add("Ride");
            barbarian.ClassSkillNames.Add("Survival");
            barbarian.ClassSkillNames.Add("Swim");
            barbarian.SkillRanksPerLevel = 4;

            var Wizard = new CharacterClass("Wizard", 0.5, 6);
            Wizard.GoodSave = new Save(SaveType.Willpower);
            Wizard.IsCaster = true;
            Wizard.IsPreparedCaster = true;
            Wizard.SkillRanksPerLevel = 2;

            CharacterClasses.Add(barbarian);
            CharacterClasses.Add(Wizard);

            Character = new Character(_eventAggregator);

            Character.Race = Races[0];
            Character.Size = Sizes[0];
            Character.ExperienceProgression = Character.ExperienceProgressionList[1];
            _classSpellsView = new CollectionViewSource();
            Character.CharacterClass = CharacterClasses[1];
            
        }


        private void RollExecute(IRollable obj)
        {
            RollService.RollExecute(this.DiceRolls, obj);
        }

        private void CharacterSpellsLvl1Filter(object sender, FilterEventArgs e)
        {
            Spell spell = e.Item as Spell;

            if (spell?.ClassLevel == ClassLevelFilter)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }


        private void AddSpellToCharacterExecute(Spell spell)
        {
            bool alreadyExist = Character.CharacterSpells.Contains(spell);
            if (alreadyExist == false)
            {
                Character.CharacterSpells.Add(spell);
            }
        }
        private void AddFeatToCharacterExecute(GeneralFeat feat)
        {
            bool alreadyExist = Character.CharacterFeats.Contains(feat);
            if (alreadyExist == false)
            {
                Character.CharacterFeats.Add(feat);
            }
        }

        private void RollSkillExecute(Skill skill)
        {
            IDice dice = new Dice();
            dice.Dice(20).Constant(skill.Bonus);
            DiceResult result = dice.Roll(new RandomDieRoller());
            DiceRoll roll = new DiceRoll
            {
                Expression = result.DiceExpression,
                Total = result.Value,
                Sender = skill.Name,
                DiceResult = Convert.ToInt32(result.RollsDisplayText)
            };

            DiceRolls.Insert(0, roll);


        }
        private void RollAttackExecute(Attack attack)
        {
            var roll = new DiceRoll();
            roll.RollAttack(attack);
            DiceRolls.Insert(0, roll);
            if (roll.DiceResult >= attack.ThreatRange)
            {
                roll.Special = "*CRITICAL*";
                var confirmDice = new DiceRoll();
                confirmDice.RollAttack(attack);
                confirmDice.Special = "*CONFIRM*";
                DiceRolls.Insert(1, confirmDice);
            }
        }
        private void RollDamageExecute(Attack attack)
        {
            var roll = new DiceRoll();
            roll.RollDamage(attack);
            DiceRolls.Insert(0, roll);
        }

        public void GetClassSpells(CharacterClass characterClass)
        {

            if (characterClass.IsCaster)
            {

                Character.CharacterClass.ClassSpells.Clear();

                foreach (var spell in Spells)
                {

                    object? spellObj = spell.GetType().GetProperty($"{characterClass.Name}").GetValue(spell, null);

                    if (spellObj != null)
                    {
                        var spellClassLevel = (int)spellObj;
                        spell.ClassLevel = spellClassLevel;
                        Character.CharacterClass.ClassSpells.Add(spell);
                    }

                }
                ClassSpellView.Source = Character.CharacterClass.ClassSpells;
                ClassSpellView.Filter += CharacterSpellsLvl1Filter;
                ClassSpellView.View.Refresh();

            }
        }
        public void Handle(RaceChangedEvent message)
        {
            Character.UpdateAbilities();
            Character.UpdateCharacterClassSaves();
            Character.Size = Sizes.FirstOrDefault(a => a.SizeType.Equals(Character.Race.SizeType));
            Character.ApplyRaceSize();
        }

        public void Handle(CharacterClassChangedEvent message)
        {
            if (Character != null)
            {
                Character?.UpdateCharacterClassSaves();
                Character?.SetBab();
                Character?.UpdateAvailableSkillRanks();
                GetClassSpells(message.CharacterClass);
            }

        }
    }
}

