using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using PathfinderSheetModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class MagicViewModel : ChildViewModel
    {
        private Spell _selectedSpell;
        private int _spellsLevelFilter;
        private ICollectionView _spellsView;
        private ICollectionView _preparedSpellsView;
        private ObservableCollection<int?> _spellsPerLevel = new ObservableCollection<int?>();


        public MagicViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
            SpellsView = new CollectionViewSource() { Source = Character.KnownSpells }.View;
            SpellsLevelFilter = -1;
            SpellsView.Filter += FilterSpells;
            PreparedSpellsView = new CollectionViewSource() { Source = Character.PreparedSpells }.View;
            PreparedSpellsView.Filter += FilterSpells;
            PropertyGroupDescription groupLevel = new PropertyGroupDescription("ClassLevel");
            PreparedSpellsView.GroupDescriptions.Add(groupLevel);
            AddSpellToPreparedCommand = new RelayCommand<Spell>(AddSpellToPrepared);
            SetLevelFilterCommand = new RelayCommand<int>(SetLevelFilter);
            RemoveSpellFromPreparedCommand = new RelayCommand<Spell>(RemoveSpellFromPrepared);
            if (Character.CharacterClass.IsCaster)
            {
                SpellsPerLevel = new ObservableCollection<int?>(Character.CharacterClass.SpellsPerLevel[Character.Level]);

            }
        }

        public ICollectionView PreparedSpellsView
        {
            get => _preparedSpellsView;
            set
            {
                _preparedSpellsView = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int?> SpellsPerLevel
        {
            get => _spellsPerLevel;
            set
            {
                _spellsPerLevel = value;
                foreach (var spell in Character.PreparedSpells)
                {
                    SpellsPerLevel[spell.ClassSpellLevelDictionary[Character.CharacterClass.Name]] -= 1;
                }
            }
        }


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

        public ICollectionView SpellsView
        {
            get => _spellsView;
            set
            {
                _spellsView = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddSpellToPreparedCommand { get; set; }

        public ICommand SetLevelFilterCommand { get; set; }
        public ICommand RemoveSpellFromPreparedCommand { get; set; }
        private bool FilterSpells(object obj)
        {
            if (obj is not Spell spell) return false;
            if (SpellsLevelFilter == -1 || spell.ClassSpellLevelDictionary[Character.CharacterClass.Name]
                .Equals(SpellsLevelFilter))
            {
                return true;

            }

            return false;
        }

        private void RemoveSpellFromPrepared(Spell obj)
        {
            Character.PreparedSpells.Remove(obj);
            SpellsPerLevel[obj.ClassSpellLevelDictionary[Character.CharacterClass.Name]] += 1;
            SpellsView.Refresh();
            PreparedSpellsView.Refresh();
        }

        private void SetLevelFilter(int obj)
        {

            SpellsLevelFilter = obj;

        }

        private void AddSpellToPrepared(Spell obj)
        {
            if (SpellsPerLevel[obj.ClassLevel] > 0)
            {
                Character.PreparedSpells.Add(obj);
                SpellsPerLevel[obj.ClassSpellLevelDictionary[Character.CharacterClass.Name]] -= 1;
                SpellsView.Refresh();
                PreparedSpellsView.Refresh();
            }


        }


    }
}

