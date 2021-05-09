using System.Collections.ObjectModel;

namespace UserInterface.Models.Modifiers
{
    public interface IBonusable
    {
        public ObservableCollection<Bonus> BonusList { get; set; }
        public string Name { get; set; }
       
    }
}
