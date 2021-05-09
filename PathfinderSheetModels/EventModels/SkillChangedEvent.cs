using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.EventModels
{
    public class SkillChangedEvent
    {
        public SkillChangedEvent(Skill skill)
        {
            Skill = skill;
        }

        public Skill Skill { get; set; }

    }
}
