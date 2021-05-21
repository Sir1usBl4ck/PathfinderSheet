using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetModels
{
    public class SpecialAbility
    {
        public string Name { get; set; }
        public SpecialAbilityType SpecialAbilityType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
        public string Description { get; set; }
        public int NumberOfUses { get; set; }
        public bool GrantsAttack { get; set; }

    }
}
