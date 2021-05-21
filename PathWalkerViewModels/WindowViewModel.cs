using PathfinderSheetModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels
{
    public class WindowViewModel : BaseViewModel ,IHandle<ViewChangedEvent>
    {
        private BaseViewModel _currentView;
        private EventAggregator _eventAggregator = new EventAggregator();
        
        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        public WindowViewModel()
        {
            _eventAggregator.Subscribe(this);
            CurrentView = new StartingViewModel(_eventAggregator);


        }

        public void Handle(ViewChangedEvent message)
        {
            CurrentView = message.ViewModel;

        }
    }
}
