using System.Collections.ObjectModel;

namespace PathfinderSheetModels
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
