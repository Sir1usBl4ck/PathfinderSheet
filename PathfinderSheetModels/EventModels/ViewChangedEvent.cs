using UserInterface.ViewModels;

namespace UserInterface.EventModels
{
    public class ViewChangedEvent
    {
        public ViewChangedEvent(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public BaseViewModel ViewModel { get; set; }
    }
}
