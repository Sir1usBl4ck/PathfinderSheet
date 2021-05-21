using System.Collections.Generic;

namespace PathfinderSheetModels
{
    public class Race
    {

        public Race(string name, RaceType type, RaceSubType subType,SizeType size)

        {
            ModifiedAbilities = new List<Bonus>();
            Name = name;
            Type = type;
            SubType = subType;
            SizeType = size;
        }

        public List<Bonus> ModifiedAbilities { get; set; }
        public string Name { get; set; }
        public SizeType SizeType { get; set; }
        public RaceType Type { get; set; }
        public RaceSubType SubType { get; set; }
        
    }
}
