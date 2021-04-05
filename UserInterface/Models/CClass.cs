using System;
using System.Collections.Generic;
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
        public int Level { get; set; }
        public ObservableCollection<Save> ClassSaves { get; } = new ObservableCollection<Save>();
        public ObservableCollection<Ability> ClassAbilities { get; } = new ObservableCollection<Ability>();

    }
}
