using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
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
        Resistance,
        Sacred,
        Shield,
        Size
    }
    public class Bonus
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public BonusType BonusType { get; set; }

        public Bonus()
        {
            Name = "Default";
            Value = 0;
            
        }

    }
}
