using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{
    public class ArmorClass : BaseAttribute, IBonusable
    {
        public ArmorClass(Ability ability)
        {
            Ability = ability;
        }
        public Ability Ability { get; set; }
        public override int BaseScore { get; set; }
        public override int Score => BaseScore + BonusList.Sum(a => a.Value) + Ability.Modifier;
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; }
        public int TouchScore => CalculateTouchScore();
        public int FlatFootedScore => CalculateFlatFootedScore();
        private int CalculateFlatFootedScore()
        {
            var flatFootedList = BonusList
                                .Where(b => (b.BonusType == BonusType.Deflection))
                                .Where(b => b.BonusType == BonusType.Armor)
                                .Where(b => b.BonusType == BonusType.Shield)
                                .Where(b => b.BonusType == BonusType.NaturalArmor);
            return 10 + flatFootedList.Sum(a => a.Value);

        }
        private int CalculateTouchScore()
        {
            var touchList =
                            BonusList.Where(b => (b.BonusType == BonusType.Deflection)).
                                Where(b => b.BonusType == BonusType.Dodge);
            return 10 + touchList.Sum(a => a.Value);
        }

        
        
    }


   
}
