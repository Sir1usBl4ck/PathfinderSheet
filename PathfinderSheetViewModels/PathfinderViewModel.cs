using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetViewModels.EventModels;

namespace PathfinderSheetViewModels
{
    public class PathfinderViewModel : BaseViewModel
    {
        private EventAggregator _eventAggregator;

       
        public PathfinderViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            ChangeViewCommand = new RelayCommand(ChangeViewExecute);
            CreateCharacterCommand = new RelayCommand(CreateCharacterExecute);
        }

        public ICommand CreateCharacterCommand { get; set; }
        public ICommand ChangeViewCommand { get; set; }

        private void ChangeViewExecute()
        {
            _eventAggregator.Publish(new ViewChangedEvent(new GameViewModel(_eventAggregator)));
        }


        private void CreateCharacterExecute()  
        {
            _eventAggregator.Publish(new ViewChangedEvent(new CharacterCreatorViewModel(_eventAggregator)));
        }

       


    }
}
