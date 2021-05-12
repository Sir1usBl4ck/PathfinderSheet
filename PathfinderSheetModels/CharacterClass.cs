using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace PathfinderSheetModels
{

    [Serializable]
    public class CharacterClass : ObservableObject
    {

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
        
        public CharacterClass()
        {
            BaBProgression = 0;
            HitDice = 1;
            GoodSave = new Save();

        }
       
    }
}
