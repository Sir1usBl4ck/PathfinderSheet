using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class CharacterClassChangedEvent
    {
        public CharacterClass CharacterClass { get; set; }

        public CharacterClassChangedEvent(CharacterClass characterClass)
        {
            CharacterClass = characterClass;
        }
    }
}
