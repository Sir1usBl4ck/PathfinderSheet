﻿namespace PathfinderSheetModels
{
    
    public enum BonusType
    {
        Alchemical,
        Armor,
        Circumstance,
        Competence,
        Deflection,
        Dodge,
        Enhancement,
        Inherent,
        Insight,
        Luck,
        Morale,
        NaturalArmor,
        Profane,
        Penalty,
        Race,
        Resistance,
        Sacred,
        Shield,
        Size
    }
    public class Bonus
    {
        public string BonusSource { get; set; }
        public int Value { get; set; }
        public BonusType BonusType { get; set; }
        public bool IsStackable { get; set; }

        public Bonus()
        {
            
        }

    }
}
