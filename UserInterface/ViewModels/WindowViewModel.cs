using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentView;

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public void ChangeView(BaseViewModel viewModel)
        {
            CurrentView = viewModel;
        }


        public WindowViewModel()
        {
            CurrentView = new PathFinderViewModel();
        }
    }
}
