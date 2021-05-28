using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetModels
{
    
    public class Inventory 
    {
        public Inventory(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public EventAggregator EventAggregator { get; set; }
        public Armor Armor { get; set; } = new Armor();
        public Shield Shield { get; set; } = new Shield();
        public ObservableCollection<Item> ItemsList { get; set; } = new ObservableCollection<Item>();
        
    }
}
