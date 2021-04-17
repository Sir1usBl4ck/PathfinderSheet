using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class RaceChangedEvent
    {
        public RaceChangedEvent(Race race)
        {
            Race = race;
        }

        public Race Race { get; set; }


    }
}
