using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class BonusListChangedEvent
    {
        public ObservableCollection<Bonus> BonusList { get; set; }

        public BonusListChangedEvent(ObservableCollection<Bonus> bonusList)
        {
            BonusList = bonusList;
        }
    }
}
