using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetModels.EventModels
{
    public class AttackChangedEvent
    {
        public AttackChangedEvent(Attack attack)
        {
            Attack = attack;
        }

        public Attack Attack { get; set; }
    }
}
