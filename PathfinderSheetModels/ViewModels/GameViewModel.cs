using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using UserInterface.EventModels;
using UserInterface.Models;
using UserInterface.Models.Modifiers;
using UserInterface.Services;
using Size = UserInterface.Models.Size;

namespace UserInterface.ViewModels
{
    public class GameViewModel : BaseViewModel, IHandle<RaceChangedEvent>, IHandle<CharacterClassChangedEvent>, IHandle<CharacterChangedEvent>
    {
        private EventAggregator _eventAggregator;
        private int _spellsLevelFilter;
        private string _stringSpellFilter = string.Empty;
        
        //filter
        public string StringSpellFilter
        {
            get => _stringSpellFilter;
            set
            {
                _stringSpellFilter = value;
                OnPropertyChanged();
                //ClassSpellsView.Refresh();
            }
        }
        public int SpellsLevelFilter
        {
            get => _spellsLevelFilter;
            set
            {
                _spellsLevelFilter = value;
                OnPropertyChanged();
                //ClassSpellsView.Refresh();
            }
        }

        public Character Character { get; set; }
        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        public ObservableCollection<CharacterClass> CharacterClasses { get; } =
            new ObservableCollection<CharacterClass>();
        public ObservableCollection<Size> Sizes { get; } = new ObservableCollection<Size>();
        public ObservableCollection<Spell> Spells { get; } = new ObservableCollection<Spell>();
        public ObservableCollection<int> SpellsPerDay { get; set; }
        public ObservableCollection<int> SpellLevels { get; set; } = new ObservableCollection<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9
        };
        public ObservableCollection<GeneralFeat> Feats { get; } = new ObservableCollection<GeneralFeat>();
        public ObservableCollection<DiceRoll> DiceRolls { get; set; } = new ObservableCollection<DiceRoll>();
        

        public bool AttackViewTrigger { get; set; }
        public bool BuffViewTrigger { get; set; }
        public bool DebuffViewTrigger { get; set; }
        public Attack NewAttack { get; set; }
        public Buff NewBuff { get; set; }
        
        public RollService RollService { get; set; }
        public ICommand AddSpellToCharacterCommand { get; set; }
        public ICommand AddSpellsFilterLevel { get; set; }
        public ICommand PrepareSpellCommand { get; set; }
        public ICommand AddFeatToCharacterCommand { get; set; }
        public ICommand RemoveFeatCommand { get; set; }
        public ICommand RollSkillCommand { get; set; }
        public ICommand RollAttackCommand { get; set; }
        public ICommand RollDamageCommand { get; set; }
        public ICommand RollCommand { get; set; }
        public ICommand CreateNewAttackCommand { get; set; }
        public ICommand AddNewAttackToListCommand { get; set; }
        public ICommand RemoveAttackFromList { get; set; }
        public ICommand AddWoundCommand { get; set; }
        public ICommand RemoveWoundCommand { get; set; }
        public ICommand SwitchToBuffView { get; set; }
        public ICommand AddBonusToBuffListCommand { get; set; }
        public ICommand RemoveBonusFromBuffListCommand { get; set; }
        public ICommand SwitchToConditionView { get; set; }
        public ICommand AddBuffToListCommand { get; set; }

        //public ICollectionView ClassSpellsView { get; set; }
        //public ICollectionView KnownSpellsView { get; set; }
        //public ICollectionView PreparedSpellsView { get; set; }

        
        //----Constructor
        public GameViewModel(EventAggregator eventAggregator)
        {
            var dataLoader = Data.DataLoader.GetDataLoader();
            Spells = dataLoader.Spells;
            GetClassSpellDictionary(Spells);
            
            Feats = dataLoader.Feats;

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            RollService = new RollService();

            AddSpellToCharacterCommand = new RelayCommand<Spell>(AddSpellToCharacterExecute);
            AddSpellsFilterLevel = new RelayCommand<string>(SetSpellFilter);
            PrepareSpellCommand = new RelayCommand<Spell>(AddSpellToPrepared);
            AddFeatToCharacterCommand = new RelayCommand<GeneralFeat>(AddFeatToCharacterExecute);
            RemoveFeatCommand = new RelayCommand<GeneralFeat>(RemoveFeat);
            RollCommand = new RelayCommand<IRollable>(RollExecute);
            RollSkillCommand = new RelayCommand<Skill>(RollSkillExecute);
            RollAttackCommand = new RelayCommand<Attack>(RollAttackExecute);
            RollDamageCommand = new RelayCommand<Attack>(RollDamageExecute);
            CreateNewAttackCommand = new RelayCommand(CreateNewAttack);
            AddNewAttackToListCommand = new RelayCommand(AddNewAttackToList);
            RemoveAttackFromList = new RelayCommand<Attack>(RemoveSelectedAttack);
            AddWoundCommand = new RelayCommand(AddWound);
            RemoveWoundCommand = new RelayCommand(RemoveWound);

            SwitchToBuffView = new RelayCommand(BuffViewExecute);
            AddBonusToBuffListCommand = new RelayCommand(AddBonusToBuffList);
            RemoveBonusFromBuffListCommand = new RelayCommand<Bonus>(RemoveBonusFromBuffList);
            AddBuffToListCommand = new RelayCommand<Buff>(AddBuffToList);

            SwitchToConditionView = new RelayCommand(DebuffViewExecute);
            


        }

        private void AddBuffToList(Buff buff)
        {
            Character.BuffsList.Add(buff);
            foreach (var bonus in buff.BonusList)
            {
                Character.BonusList.Add(bonus);
            }
            _eventAggregator.Publish(new BonusListChangedEvent(Character.BonusList));
            BuffViewTrigger = false;
            OnPropertyChanged(nameof(BuffViewTrigger));
        }

        private void RemoveBonusFromBuffList(Bonus bonus)
        {
            NewBuff.BonusList.Remove(bonus);
        }

        private void AddBonusToBuffList()
        {
            NewBuff.BonusList.Add(NewBuff.NewBonus);
            NewBuff.NewBonus = new Bonus();
        }

        private void BuffViewExecute()
        {
            NewBuff = new Buff();
            NewBuff.Name = "test";
            BuffViewTrigger = true;
            OnPropertyChanged(nameof(BuffViewTrigger));
        }

        private void DebuffViewExecute()
        {
            DebuffViewTrigger = true;
        }

        private void RemoveSelectedAttack(Attack obj)
        {
            Character.AttackList.Remove(obj);
        }

        private void AddWound()
        {
            Character.Wounds += 1;
        }

        private void RemoveWound()
        {
            Character.Wounds -= 1;
        }

        private void AddNewAttackToList()
        {
            Character.AttackList.Add(NewAttack);
            AttackViewTrigger = false;
            OnPropertyChanged(nameof(AttackViewTrigger));
        }

        private void CreateNewAttack()
        {
            NewAttack = new Attack(_eventAggregator);
            AttackViewTrigger = true;
            OnPropertyChanged(nameof(AttackViewTrigger));
        }

        private void SetSpellFilter(string spellLevel)
        {
            if (spellLevel != null)
            {
                var level = Int32.Parse(spellLevel);
                if (SpellsLevelFilter == level && SpellsLevelFilter != 0)
                {

                    //ClassSpellsView.Filter = null;
                    //ClassSpellsView.Filter = FilterSpellsName;
                    //ClassSpellsView.Refresh();


                }
                else
                {
                    //ClassSpellsView.Filter += FilterSpells;
                    //SpellsLevelFilter = level;
                    //ClassSpellsView.Refresh();

                }
            }

        }

        public void GetClassSpellDictionary(ObservableCollection<Spell> spells)
        {
            List<string> ClassListName = new List<string>
            {
                "Bard","Cleric","Domain","Druid","Paladin","Ranger","Wizard","Alchemist","Inquisitor",
                "Magus","Summoner","Witch","Bloodrager","Shaman","Medium","Mesmerist","Occultist","Psychic",
                "Spiritualist"

            };


            foreach (Spell spell in Spells)
            {
                foreach (var name in ClassListName)
                {
                    var classListName = name;


                    int? nullableSpellLevel = (int?)spell.GetType().GetProperty($"{name}")?.GetValue(spell, null);

                    if (nullableSpellLevel != null)
                    {
                        int spellLevel = (int)nullableSpellLevel;
                        spell.ClassSpellLevelDictionary.Add(classListName, spellLevel);
                    }

                }

            }

        }

        private void RemoveFeat(GeneralFeat feat)
        {
            bool alreadyExist = Character.CharacterFeats.Contains(feat);
            if (alreadyExist == true)
            {
                Character.CharacterFeats.Remove(feat);
            }
        }

        private void RollExecute(IRollable obj)
        {
            RollService.RollExecute(this.DiceRolls, obj);
        }

        private void AddSpellToCharacterExecute(Spell spell)
        {
            int spellLevel = spell.ClassSpellLevelDictionary[Character.CharacterClass.Name];
            var canLearn = SpellsPerDay[spellLevel]>0;

            if (Character.KnownSpells.Contains(spell) == false && canLearn)
            {
                Character.KnownSpells.Add(spell);
            }
        }

        private void AddSpellToPrepared(Spell spell)
        {
            int spellLevel = spell.ClassSpellLevelDictionary[Character.CharacterClass.Name];
            int spellSlot = SpellsPerDay[spellLevel];
            

            // if spellSlot>0 add spell to PreparedSpell and spellSlot -=1

            if (spellSlot>0)
            {
                Character.PreparedSpells.Add(spell);
                SpellsPerDay[spellLevel] -= 1;
                OnPropertyChanged(nameof(SpellsPerDay));
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

                characterClass.ClassSpells.Clear();

                foreach (var spell in Spells)
                {

                    if (spell.ClassSpellLevelDictionary.ContainsKey(characterClass.Name))
                    {
                        characterClass.ClassSpells.Add(spell);
                    }

                }

            }
        }
        
        private bool FilterSpellsName(object obj)
        {
            if (obj is Spell spell)
            {
                return spell.Name.Contains(StringSpellFilter, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
        
        private bool FilterSpells(object obj)
        {
            if (obj is Spell spell)
            {
                var className = Character.CharacterClass.Name;

                if (spell.ClassSpellLevelDictionary[className].Equals(SpellsLevelFilter) &&
                    spell.Name.Contains(StringSpellFilter, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;

                }

                return false;



            }

            return false;


        }
        
        private void GetSpellsPerDay(CharacterClass characterClass)
        {
            if (Character.CharacterClass.IsCaster)
            {
                var list = characterClass.SpellsPerLevel;
                ObservableCollection<int> dic = new ObservableCollection<int>();
                var i = -1;
            
                foreach (int number in list[Character.Level-1])
                {
                    i++;
                    int bonusSpell = 0;
                    var intModifier = Character.Abilities.FirstOrDefault(a => a.Type.Equals(AbilityType.Intelligence)).Modifier;
                    if (i > 0)
                    {
                        double bonusSpelld = (((intModifier - i) / 4) + 0.5);
                        bonusSpell = Convert.ToInt32(Math.Round(bonusSpelld, MidpointRounding.AwayFromZero));
                    }
                  

                    if (bonusSpell > 0 && number > 0)
                    {
                        dic.Add(number+bonusSpell);
                    }
                    else
                    {
                        dic.Add(number);
                    }
                }
                SpellsPerDay = dic;
            }


        }

        public void Handle(RaceChangedEvent message)
        {
            foreach (var ability in Character.Abilities)
            {
                foreach (var bonus in message.Race.ModifiedAbilities.Where(bonus => bonus.Type == ability.Type))
                {
                    ability.BonusList.Add(bonus);
                }

                ability.CalculateBonus();
            }

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



            }

        }

        public void Handle(CharacterChangedEvent message)
        {
            Character = message.Character;
            Character.ExperienceProgression = Character.ExperienceProgressionList[1];
            GetClassSpells(Character.CharacterClass);
            //ClassSpellsView = CollectionViewSource.GetDefaultView(Character.CharacterClass.ClassSpells);
            //ClassSpellsView = new CollectionViewSource { Source = Character.CharacterClass.ClassSpells }.View;
            //ClassSpellsView.SortDescriptions.Add(new SortDescription(nameof(Spell.Name), ListSortDirection.Ascending));
            //KnownSpellsView = new CollectionViewSource { Source = Character.KnownSpells }.View;
            //KnownSpellsView.SortDescriptions.Add(new SortDescription(nameof(Spell.Name), ListSortDirection.Ascending));
            //PreparedSpellsView = new CollectionViewSource { Source = Character.PreparedSpells }.View;
            //PreparedSpellsView.SortDescriptions.Add(new SortDescription(nameof(Spell.Name), ListSortDirection.Ascending));
            GetSpellsPerDay(Character.CharacterClass);
            Character.PublishLevelChanged();






        }
    }
}



