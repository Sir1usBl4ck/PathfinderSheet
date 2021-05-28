using System.Collections.ObjectModel;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;
using PathWalkerViewModels.ChildViewModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private ChildViewModel _currentChildView;

        public CharacterViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            CurrentChildView = new GeneralViewModel(Character,EventAggregator);
            BackToMainMenuCommand = new RelayCommand(ChangeViewToMainMenu);
        }

        private void ChangeViewToMainMenu()
        {
            EventAggregator.Publish(new ViewChangedEvent(new StartingViewModel(EventAggregator)));
        }

        public Character Character { get; set; }
        public ObservableCollection<DiceRoll> DiceRolls { get; set; }
        public EventAggregator EventAggregator { get; set; }
        public ICommand BackToMainMenuCommand { get; set; }
        public ChildViewModel CurrentChildView
        {
            get => _currentChildView;
            set { _currentChildView = value;
                _currentChildView.EventAggregator = EventAggregator;
                _currentChildView.Character = Character;
                OnPropertyChanged(); }
        }
    }
}
