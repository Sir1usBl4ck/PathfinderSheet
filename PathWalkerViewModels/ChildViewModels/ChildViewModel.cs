using PathfinderSheetModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class ChildViewModel : ObservableObject
    {
        public Character Character { get; set; }
        public EventAggregator EventAggregator { get; set; }
    }
}
