using System.Collections.Generic;

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
        Native,
        Gnome
    }
    public class Race
    {

        public Race(string name, Type type, SubType subType,SizeType size)

        {
            ModifiedAbilities = new List<AbilityModifier>();
            Name = name;
            Type = type;
            SubType = subType;
            SizeType = size;
        }

        public List<AbilityModifier> ModifiedAbilities { get; set; }
        public string Name { get; set; }
        public SizeType SizeType { get; set; }
        public Type Type { get; set; }
        public SubType SubType { get; set; }



        public List<RacialTrait> RacialTraitList { get; set; }






    }
}
