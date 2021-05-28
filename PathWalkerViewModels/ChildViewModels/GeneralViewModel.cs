using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathWalkerViewModels.ChildViewModels
{
    public class GeneralViewModel : ChildViewModel
    {

        private ChildViewModel _currentFeatView;
        private ChildViewModel _currentCombatView;
        private ChildViewModel _currentInventoryView;
        private ChildViewModel _currentMagicView;
        private ChildViewModel _currentInfoView;
        private int _tabIndex;
        private bool _isTabControlEnabled = true;

        public GeneralViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
            CurrentCombatView = new CombatViewModel(Character, EventAggregator);
            CurrentMagicView = new MagicViewModel(Character, EventAggregator);
            CurrentInfoView = new CharacterInfoViewModel(Character, EventAggregator);
            WornItemsViewModel = new WornItemsViewModel(Character, EventAggregator);
            CurrentInventoryView = new InventoryViewModel(Character, EventAggregator);
            AddAttackCommand = new RelayCommand(ChangeViewToAddAttack);
            AddFeatCommand = new RelayCommand(ChangeViewToAddFeats);
            
            BackCommand = new RelayCommand(ChangeViewToDefault);
            ChangeInfoToSpellCommand = new RelayCommand(ChangeInfoViewToSpell);
            ChangeViewToAddMagicCommand = new RelayCommand(ChangeViewToAddMagic);

        }

        
        public int TabIndex
        {
            get => _tabIndex;
            set
            {
                _tabIndex = value;
                CurrentInfoView = _tabIndex switch
                {
                    3 => new SpellDetailViewModel(EventAggregator),
                    4 => WornItemsViewModel,
                    _ => new CharacterInfoViewModel(Character, EventAggregator)
                };
            }
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

        public ChildViewModel CurrentInventoryView
        {
            get { return _currentInventoryView; }
            set
            {
                _currentInventoryView = value;
                OnPropertyChanged();
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
            set
            {
                _currentCombatView = value;
                OnPropertyChanged();
            }
        }

        public ChildViewModel CurrentMagicView
        {
            get => _currentMagicView;
            set
            {
                _currentMagicView = value;
                CurrentInfoView = new SpellDetailViewModel(EventAggregator);
                OnPropertyChanged();
            }
        }

        public ChildViewModel CurrentInfoView
        {
            get => _currentInfoView;
            set
            {
                _currentInfoView = value;
                OnPropertyChanged();
            }
        }

        public ChildViewModel WornItemsViewModel { get; set; } 

        public ICommand BackCommand { get; set; }

        public ICommand AddFeatCommand { get; set; }

        public ICommand AddAttackCommand { get; set; }

        public ICommand ChangeInfoToSpellCommand { get; set; }

        public ICommand ChangeViewToAddMagicCommand { get; set; }

        private void ChangeViewToAddMagic()
        {
            CurrentMagicView = new AddMagicViewModel(Character, EventAggregator);
        }

        private void ChangeInfoViewToSpell()
        {
            CurrentInfoView = new SpellDetailViewModel(EventAggregator);

        }

        private void ChangeViewToAddAttack()
        {
            CurrentCombatView = new AddAttackViewModel(Character, EventAggregator);
        }

        private void ChangeViewToDefault()
        {
            IsTabControlEnabled = true;
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
            CurrentCombatView = new CombatViewModel(Character, EventAggregator);
            CurrentInfoView = new CharacterInfoViewModel(Character, EventAggregator);
            CurrentMagicView = new MagicViewModel(Character, EventAggregator);
            TabIndex = _tabIndex;



        }

        private void ChangeViewToAddFeats()
        {
            IsTabControlEnabled = false;
            CurrentFeatView = new AddFeatsViewModel(Character, EventAggregator);
        }

        


    }
}
