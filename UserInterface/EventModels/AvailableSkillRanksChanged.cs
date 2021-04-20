using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.EventModels
{
    public class AvailableSkillRanksChanged
    {
        public AvailableSkillRanksChanged(int availableSkillRanks)
        {
            AvailableSkillRanks = availableSkillRanks;
        }

        public int AvailableSkillRanks { get; set; }
    }
}
