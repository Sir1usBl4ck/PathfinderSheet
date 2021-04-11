using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AbilityModifier
    {
        public AbilityModifier(AbilityType type, int bonus)
        {
            Type = type;
            Bonus = bonus;
        }

        public AbilityType Type { get; set; }
        public int Bonus { get; set; }


    }
}
