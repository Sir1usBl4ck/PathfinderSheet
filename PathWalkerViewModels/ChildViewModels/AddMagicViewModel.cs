using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;
using PathfinderSheetServices;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class AddMagicViewModel : ChildViewModel
    {
        private ICollectionView _spellsView;
        private string _stringSpellFilter;
        private int _spellsLevelFilter;

        public AddMagicViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            Spells = DataLoader.GetDataLoader().Spells;
            CharacterService.WriteClassSpellLevelInSpells(Character,Spells);
            SpellsView = new CollectionViewSource
            {
                Source = Spells.Where(a =>
a.ClassSpellLevelDictionary.ContainsKey(Character.CharacterClass.Name))
            }.View;
            SpellsView.Filter += FilterSpells;
            SpellsLevelFilter = -1;
            AddSpellToCharacterCommand = new RelayCommand<Spell>(AddSpellToCharacter);
            SetLevelFilterCommand = new RelayCommand<int>(SetLevelFilter);
            RemoveSpellFromCharacterCommand = new RelayCommand<Spell>(RemoveSpellFromCharacter);
        }

        private Spell _selectedSpell;

        public Spell SelectedSpell
        {
            get { return _selectedSpell; }
            set
            {
                _selectedSpell = value;
                OnPropertyChanged();
                EventAggregator.Publish(new SelectedSpellChangedEvent(_selectedSpell));
            }
        }

        public string StringSpellFilter
        {
            get => _stringSpellFilter;
            set
            {
                _stringSpellFilter = value;
                OnPropertyChanged();
                SpellsView.Refresh();
            }
        }

        public int SpellsLevelFilter
        {
            get => _spellsLevelFilter;
            set
            {
                _spellsLevelFilter = value;
                OnPropertyChanged();
                SpellsView.Refresh();
            }
        }

        public ObservableCollection<Spell> Spells { get; set; }

        public ICollectionView SpellsView
        {
            get => _spellsView;
            set
            {
                _spellsView = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddSpellToCharacterCommand { get; set; }

        public ICommand SetLevelFilterCommand { get; set; }

        public ICommand RemoveSpellFromCharacterCommand { get; set; }

        private bool FilterSpells(object obj)
        {
            if (obj is Spell spell)
            {
                if (Character.KnownSpells.Contains(spell))
                {
                    return false;
                }

                if (SpellsLevelFilter == -1 || spell.ClassSpellLevelDictionary[Character.CharacterClass.Name]
                    .Equals(SpellsLevelFilter))
                {
                    if (string.IsNullOrEmpty(StringSpellFilter))
                    {
                        return true;
                    }

                    return spell.Name.Contains(StringSpellFilter, StringComparison.InvariantCultureIgnoreCase);
                }

                return false;

            }

            return false;
        }

        private void RemoveSpellFromCharacter(Spell obj)
        {
            Character.KnownSpells.Remove(obj);
            SpellsView.Refresh();
        }

        private void SetLevelFilter(int level)
        {

            SpellsLevelFilter = level;

        }

        private void AddSpellToCharacter(Spell obj)
        {
            if (Character.KnownSpells.Contains(obj))
            {
                return;
            }

            Character.KnownSpells.Add(obj);
            SpellsView.Refresh();
        }

    }

}
