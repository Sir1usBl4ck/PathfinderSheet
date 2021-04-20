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
    public class CharacterClass : ObservableObject
    {
        private ObservableCollection<Spell> _classSpells;

        public CharacterClass()
        {
            BaBProgression = 0;
            HitDice = 1;
            GoodSave = new Save();
            ClassSpells = new ObservableCollection<Spell>();


        }
        public CharacterClass(string name, double baBProgression, int hitDice)
        {
            Name = name;
            BaBProgression = baBProgression;
            HitDice = hitDice;
            ClassSpells = new ObservableCollection<Spell>();



        }

        public string Name { get; set; }
        public int HitDice { get; set; }
        public double BaBProgression { get; set; }
        public Save GoodSave { get; set; }
        public List<string> ClassSkillNames { get; } = new List<string>();
        public int SkillRanksPerLevel { get; set; }

        public ObservableCollection<Spell> ClassSpells
        {
            get => _classSpells;
            set
            {
                _classSpells = value;
                OnPropertyChanged();
                
            }
        }
    }
}
