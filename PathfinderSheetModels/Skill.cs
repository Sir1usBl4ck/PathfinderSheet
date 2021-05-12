using System.Collections.ObjectModel;
using System.Linq;


namespace PathfinderSheetModels
{
    public class Skill : BaseAttribute, IBonusable, IRollable
    {
        public Skill(Ability ability)
        {
            Ability = ability;
        }

        public Ability Ability { get; set; }
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; }
        public bool IsTrainedOnly { get; set; }
        public bool HasArmorCheckPenalty { get; set; }
        public bool IsClass { get; set; }
        public string Name { get; set; }

        public override int Score =>
            BaseScore + BonusList.Sum(a => a.Value) + Ability.Modifier + (IsClass && BaseScore >= 1 ? 3 : 0);
    }
    
}