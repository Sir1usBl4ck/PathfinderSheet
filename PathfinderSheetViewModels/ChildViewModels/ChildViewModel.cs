using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;

namespace PathfinderSheetViewModels.ChildViewModels
{
    public class ChildViewModel : ObservableObject
    {
        public Character Character { get; set; }
        public EventAggregator EventAggregator { get; set; }
    }
}
