using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathfinderSheetViewModels.ChildViewModels
{
    public class GeneralViewModel : ChildViewModel
    {
        private long[] _xpTable;
        private ChildViewModel _currentFeatView;
        private ChildViewModel _currentCombatView;
        private bool _isTabControlEnabled = true;

        public GeneralViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            XpTable = ExperienceService.Table(Progression.Medium);
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
            CurrentCombatView = new CombatViewModel(Character, EventAggregator);
            AddAttackCommand = new RelayCommand(ChangeViewToAddAttack);
            AddFeatCommand = new RelayCommand(ChangeViewToAddFeats);
            AddSpecialAbilityCommand = new RelayCommand(ChangeViewToAddSpecialAbility);
            BackCommand = new RelayCommand(ChangeViewToFeats);

        }

        private void ChangeViewToAddAttack()
        {
           CurrentCombatView = new AddAttackViewModel(Character, EventAggregator);
        }

        private void ChangeViewToAddSpecialAbility()
        {
            IsTabControlEnabled = false;
            CurrentFeatView = new AddSpecialAbilitiesViewModel(Character, EventAggregator);
        }

        private void ChangeViewToFeats()
        {
            IsTabControlEnabled = true;
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
            CurrentCombatView = new CombatViewModel(Character, EventAggregator);
        }

        private void ChangeViewToAddFeats()
        {
            IsTabControlEnabled = false;
            CurrentFeatView = new AddFeatsViewModel(Character, EventAggregator);
        }

        public bool IsTabControlEnabled
        {
            get => _isTabControlEnabled;
            set
            {
                _isTabControlEnabled = value;
                OnPropertyChanged(nameof(IsTabControlEnabled));
            }
        }

        public long[] XpTable
        {
            get => _xpTable;
            set
            {
                _xpTable = value;
                OnPropertyChanged(nameof(OldXpToLevel));
                OnPropertyChanged(nameof(XpToLevel));
            }
        }

        public ChildViewModel CurrentFeatView
        {
            get => _currentFeatView;
            set { _currentFeatView = value; OnPropertyChanged(); }
        }

        public ChildViewModel CurrentCombatView
        {
            get => _currentCombatView;
            set { _currentCombatView = value; OnPropertyChanged(); }
        }

        public long OldXpToLevel => Character.Level != 1 ? 0 : XpTable[Character.Level];
        public long XpToLevel => XpTable[Character.Level + 1];
        public long CurrentXp { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand AddFeatCommand { get; set; }
        public ICommand AddSpecialAbilityCommand { get; set; }
        public ICommand ResetDefaultViewsCommand { get; set; }
        public ICommand AddAttackCommand { get; set; }
    }
}
