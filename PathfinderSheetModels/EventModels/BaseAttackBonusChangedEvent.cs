namespace PathfinderSheetModels
{
    public class BaseAttackBonusChangedEvent
    {
        public BaseAttackBonusChangedEvent(int baseAttackBonus)
        {
            BaseAttackBonus = baseAttackBonus;
        }

        public int BaseAttackBonus { get; set; }
    }
}
