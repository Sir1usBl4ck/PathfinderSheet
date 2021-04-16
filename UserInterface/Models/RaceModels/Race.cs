using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.Models
{
    public enum Type
    {
        Humanoid,
        Outsider
    }

    public enum SubType
    {
        Elf,
        Human,
        Dwarf,
        Native
    }
    public class Race
    {

        public Race(string name, Type type, SubType subType)

        {
            ModifiedAbilities = new List<AbilityModifier>();
            Name = name;
            Type = type;
            SubType = subType;
        }

        public List<AbilityModifier> ModifiedAbilities { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }
        public Type Type { get; set; }
        public SubType SubType { get; set; }



        public List<RacialTrait> RacialTraitList { get; set; }






    }
}
