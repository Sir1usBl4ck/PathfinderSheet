using System.Windows.Input;
using PathfinderSheetModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels
{
    public class StartingViewModel : BaseViewModel
    {
        private EventAggregator _eventAggregator;

       
        public StartingViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CreateCharacterCommand = new RelayCommand(CreateCharacterExecute);
        }

        public ICommand CreateCharacterCommand { get; set; }
        
        private void CreateCharacterExecute()  
        {
            _eventAggregator.Publish(new ViewChangedEvent(new CharacterCreatorViewModel(_eventAggregator)));
        }

       


    }
}
