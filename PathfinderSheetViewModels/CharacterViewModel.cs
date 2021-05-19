using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;
using PathfinderSheetViewModels.ChildViewModels;

namespace PathfinderSheetViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        private ChildViewModel _currentChildView;

        public CharacterViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            CurrentChildView = new GeneralViewModel();
            

        }
        public Character Character { get; set; }
        public ObservableCollection<DiceRoll> DiceRolls { get; set; }
        public EventAggregator EventAggregator { get; set; }

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
