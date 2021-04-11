using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.EventModels
{
    public class LevelChangedEvent
    {
        public LevelChangedEvent(int level)
        {
            Level = level;
        }

        public int Level { get; set; }
    }
}
