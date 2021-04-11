using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class AbilityChangedEvent
    {
        public Ability Ability { get; set; }

        public AbilityChangedEvent(Ability ability)
        {
            Ability = ability;
        }

    }
}
