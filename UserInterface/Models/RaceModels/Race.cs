using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.Models
{
    public class Race
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }

        public int AbilityPlus1 { get; set; }
        public int AbilityPlus2 { get; set; }
        public int AbilityMinus { get; set; }

        public List<RacialTrait> RacialTraitList { get; set; } 



    }
}
