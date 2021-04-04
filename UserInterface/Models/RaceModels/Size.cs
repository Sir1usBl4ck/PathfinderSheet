using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class Size
    {
        public Size(string name, int sizeModifier, int specialSizeModifier)
        {
            Name = name;
            SizeModifier = sizeModifier;
            SpecialSizeModifier = specialSizeModifier;
        }

        public string Name { get; set; }
        public int SizeModifier{ get; set; }
        public int SpecialSizeModifier{ get; set; }

       

    }
}
