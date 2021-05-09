using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.EventModels;
using UserInterface.Models;

namespace UserInterface.ViewModels
{
    public class WindowViewModel : BaseViewModel, IHandle<ViewChangedEvent>
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
            CurrentView = new PathfinderViewModel(_eventAggregator);


        }

        public void Handle(ViewChangedEvent message)
        {
            CurrentView = message.ViewModel;
            
        }
    }
}
