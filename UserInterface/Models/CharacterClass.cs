using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace UserInterface.Models
{

    [Serializable]
    public class CharacterClass : ObservableObject
    {
        private ICollectionView _classSpellsView;

        public string Name { get; set; }
        public int HitDice { get; set; }
        public double BaBProgression { get; set; }
        public Save GoodSave { get; set; }
        public List<string> ClassSkillNames { get; } = new List<string>();
        public int SkillRanksPerLevel { get; set; } 
        public ObservableCollection<Spell> ClassSpells { get; } = new ObservableCollection<Spell>();
        public bool IsCaster { get; set; }
        public bool IsPreparedCaster { get; set; }
        public bool IsSpontaneousCaster { get; set; }
        [JsonProperty("SpellsPerLevel")]
        public List<List<int?>> SpellsPerLevel { get; set; }

        public ICollectionView ClassSpellsView
        {
            get => _classSpellsView;
            set => _classSpellsView = value;
        }

        public CharacterClass()
        {
            BaBProgression = 0;
            HitDice = 1;
            GoodSave = new Save();
            ClassSpellsView = CollectionViewSource.GetDefaultView(ClassSpells);

        }
        public CharacterClass(string name, double baBProgression, int hitDice)
        {
            Name = name;
            BaBProgression = baBProgression;
            HitDice = hitDice;
            
        }
    }
}
