namespace PathfinderSheetModels
{
    public class Bonus
    {
        public string BonusSource { get; set; }
        public int Value { get; set; }
        public BonusType BonusType { get; set; }
        public bool CanStack { get; set; }
        public IBonusable Target { get; set; }
        public Bonus()
        {
            
        }

    }
}
