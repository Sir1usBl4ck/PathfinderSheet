using System.Windows.Input;
using PathfinderSheetModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class FeatsViewModel : ChildViewModel
    {
        public FeatsViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
           
            

        }

        public ICommand AddFeatCommand { get; set; }
        
    }
}
