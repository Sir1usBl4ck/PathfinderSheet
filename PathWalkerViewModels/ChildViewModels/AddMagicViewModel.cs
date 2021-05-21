using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;

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
            SpellsView = CollectionViewSource.GetDefaultView(Spells);
            SpellsView.Filter += FilterClassSpellsOnly;
            FilterDescriptionCommand = new RelayCommand(FilterSpellsByDescription);
            FilterNameCommand = new RelayCommand(FilterSpellsByName);
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
        public ICommand FilterNameCommand { get; set; }
        public ICommand FilterDescriptionCommand { get; set; }

        private void FilterSpellsByName()
        {
            SpellsView.Filter = null;
            SpellsView.Filter += FilterClassSpellsOnly;
            SpellsView.Filter += FilterSpellsName;
        }

        private void FilterSpellsByDescription()
        {
            SpellsView.Filter = null;
            SpellsView.Filter += FilterClassSpellsOnly;
            SpellsView.Filter += FilterSpellsDescription;
        }

        private bool FilterSpellsDescription(object obj)
        {
            if (string.IsNullOrEmpty(StringSpellFilter))
            {
                return true;
            }

            if (obj is Spell spell)
            {
                return spell.Description.Contains(StringSpellFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private bool FilterSpellsName(object obj)
        {
            if (string.IsNullOrEmpty(StringSpellFilter))
            {
                return true;
            }
            if (obj is Spell spell)
            {
                return spell.Name.Contains(StringSpellFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private bool FilterClassSpellsOnly(object obj)
        {

            if (obj is Spell spell)
            {
                if (spell.ClassSpellLevelDictionary.ContainsKey(Character.CharacterClass.Name))
                {
                    return false;

                }
                
            }
            return true;
        }


    }



}
