using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderSheetServices
{
    public enum Progression
    {
        Slow,
        Medium,
        Fast
    }

    public class ExperienceService
    {

        public static long[] Table(Progression progression)
        {
            var fast = new long[]
            {
                0, 0, 1300, 3300, 6000, 10000, 15000, 23000, 34000, 50000,
                71000, 105000, 145000, 210000, 295000, 425000, 600000,
                850000, 1200000, 1700000, 2400000,
            };
            var medium = new long[]
            {
                0, 0, 2000, 5000, 9000, 15000, 23000, 35000, 51000, 75000,
                105000, 155000, 220000, 315000, 445000, 635000, 890000,
                1300000, 1800000, 2550000, 3600000
            };
            var slow = new long[]
            {
                0, 0, 3000, 7500, 14000, 23000, 35000, 53000, 77000, 115000,
                160000, 235000, 330000, 475000, 665000, 955000, 1350000,
                1900000, 2700000, 3850000, 5350000
            };
            switch (progression)
            {
                case Progression.Fast:
                    return fast;
                case Progression.Medium:
                    return medium;
                case Progression.Slow:
                    return slow;

            }
            return medium;
        }
    }
}