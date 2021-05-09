﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class AbilityModifier : Bonus
    {
        public AbilityModifier(AbilityType type, int value)
        {
            Type = type;
            Value = value;
            IsStackable = false;
            BonusType = BonusType.Race;
            BonusSource = "Race";
        }

        public AbilityType Type { get; set; }
        

    }
}
