using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{
    public class ArmorClass : BaseAttribute, IBonusable, IHandle<AbilityChangedEvent>
    {
        public ArmorClass(Ability ability, EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Ability = ability;
        }

        public EventAggregator EventAggregator { get; set; }
        public Ability Ability { get; set; }
        public override int BaseScore { get; set; } = 10;
        public override int Score => BaseScore + BonusList.Sum(a => a.Value) + Ability.Modifier;
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
        public int TouchScore => CalculateTouchScore();
        public int FlatFootedScore => CalculateFlatFootedScore();

        private int CalculateFlatFootedScore()
        {
            var flatFootedList = BonusList
                .Where(b => (b.BonusType == BonusType.Deflection))
                .Where(b => b.BonusType == BonusType.Armor)
                .Where(b => b.BonusType == BonusType.Shield)
                .Where(b => b.BonusType == BonusType.NaturalArmor);
            return BaseScore + flatFootedList.Sum(a => a.Value);

        }

        private int CalculateTouchScore()
        {
            var touchList =
                BonusList.Where(b => (b.BonusType == BonusType.Deflection)).Where(b => b.BonusType == BonusType.Dodge);
            return BaseScore + touchList.Sum(a => a.Value) + Ability.Modifier;
        }


        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.AttributeType == Ability.AttributeType)
            {
                Ability = message.Ability;
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(TouchScore));
            }
        }
    }


}
