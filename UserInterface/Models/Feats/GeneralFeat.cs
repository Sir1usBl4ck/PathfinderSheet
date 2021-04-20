using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    class GeneralFeat : ObservableObject
    {
        public string Name { get; set; }
        public Bonus Bonus { get; set; }
        public string Description { get; set; }

    }
}
