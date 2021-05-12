using System.Collections.ObjectModel;

namespace PathfinderSheetModels
{
    public interface IBonusable
    {
        public ObservableCollection<Bonus> BonusList { get; set; }
        public string Name { get; set; }
       
    }
}
