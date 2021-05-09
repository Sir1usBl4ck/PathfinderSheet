using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class CharacterChangedEvent
    {
        public CharacterChangedEvent(Character character)
        {
            Character = character;
        }

        public Character Character { get; set; }
    }
}
