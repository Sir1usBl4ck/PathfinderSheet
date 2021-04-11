using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    
    [Serializable]
    public class CClass
    {
        public CClass(string name, double baBProgression)
        {
            Name = name;
            BaBProgression = baBProgression;
        }

        public string Name { get; set; }

        public double BaBProgression { get; set;  }
        public Save GoodSave { get; set; }
    }
}
