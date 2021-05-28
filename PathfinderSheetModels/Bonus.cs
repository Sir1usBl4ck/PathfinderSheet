using System.Runtime.Serialization.Formatters;

namespace PathfinderSheetModels
{
    public class Bonus
    {
        public string BonusSource { get; set; }
        public int Value { get; set; } = 1;
        public BonusType BonusType { get; set; }
        public bool CanStack
        {
            get
            {
                switch (BonusType)
                {
                    case BonusType.Circumstance:
                       return true;
                    case BonusType.Dodge:
                        return true;
                }
                return false;
            }
        }

        public AttributeType Target { get; set; }
        public Bonus()
        {
            
        }

    }
}
