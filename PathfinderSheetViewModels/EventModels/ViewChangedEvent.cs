using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetViewModels.EventModels
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
