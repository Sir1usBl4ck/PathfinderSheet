using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{
    public class Ability : BaseAttribute,  IBonusable, IRollable

    {
       public Ability(AttributeType type)
        {
            AttributeType = type;
            BaseScore = 10;
        }

        public EventAggregator EventAggregator { get; set; }
        public AttributeType AttributeType { get; set; }
        public string Name { get; set; }
        public override int Score => BaseScore + BonusList.Sum(a => a.Value);
        public int Modifier => (Score - Score % 2) / 2 - 5;
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();

    }
}