using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AbilityModifier
    {
        public AbilityModifier(AbilityType abilityType, int bonus)
        {
            AbilityType = abilityType;
            Bonus = bonus;
        }

        public AbilityType AbilityType { get; set; }
        public int Bonus { get; set; }


    }
}
