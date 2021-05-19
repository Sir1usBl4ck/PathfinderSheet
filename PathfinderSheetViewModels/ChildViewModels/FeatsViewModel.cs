using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;

namespace PathfinderSheetViewModels.ChildViewModels
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
