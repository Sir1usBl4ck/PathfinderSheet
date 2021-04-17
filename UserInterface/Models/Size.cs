using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public enum SizeType
    {
        Small,
        Medium,
        Large
    }
    public class Size
    {

        public Size(SizeType sizeType)
        {
            SizeType = sizeType;
            GetSize();

        }

        public string Name { get; set; }
        public SizeType SizeType { get; set; }
        public int SizeModifier { get; set; }
        public int SpecialSizeModifier { get; set; }
        public int FlySizeModifier { get; set; }
        public int StealthSizeModifier { get; set; }

        public void GetSize()
        {
            switch (SizeType)
            {
                case SizeType.Medium:
                    Name = "Medium";
                    SizeModifier = 0;
                    SpecialSizeModifier = 0;
                    FlySizeModifier = 0;
                    StealthSizeModifier = 0;
                    
                    break;
                case SizeType.Small:
                    Name = "Small";
                    SizeModifier = 1;
                    SpecialSizeModifier = -1;
                    FlySizeModifier = 2;
                    StealthSizeModifier = 4;

                    break;
                case SizeType.Large:
                    Name = "Small";
                    SizeModifier = -1;
                    SpecialSizeModifier = 1;
                    FlySizeModifier = -2;
                    StealthSizeModifier = -4;

                    break;
            }
        }


    }
}
