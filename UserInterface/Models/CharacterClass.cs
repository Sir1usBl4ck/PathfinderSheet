using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace UserInterface.Models
{
    
    [Serializable]
    public class CharacterClass
    {
        public CharacterClass(string name, double baBProgression, int hitDice)
        {
            Name = name;
            BaBProgression = baBProgression;
            HitDice = hitDice;
        }

        public string Name { get; set; }
        public int HitDice { get; set; }
        public double BaBProgression { get; set;  }
        public Save GoodSave { get; set; }
        
        /// <summary>
        /// A collection of skill names that belong to this character class as a core skill
        /// </summary>
        public List<string> ClassSkillNames { get; } = new List<string>();
    }
}
