using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetModels
{
    public interface IBonusable
    {
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; }
    }
}
