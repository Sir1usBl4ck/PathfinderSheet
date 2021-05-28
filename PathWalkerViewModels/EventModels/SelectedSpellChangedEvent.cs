using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;

namespace PathWalkerViewModels.EventModels
{
    public class SelectedSpellChangedEvent
    {
        public SelectedSpellChangedEvent(Spell spell)
        {
            Spell = spell;
        }

        public Spell Spell { get; set; }
    }
}
