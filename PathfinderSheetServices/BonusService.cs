using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class BonusService
    {
        public static void AddBonusToList(Bonus newBonus, ObservableCollection<Bonus> bonusList)
        {

            if (bonusList.Any(a => a.BonusSource == newBonus.BonusSource ))
            {
                bonusList.Remove(bonusList.FirstOrDefault(a => a.BonusSource == newBonus.BonusSource));
                if (bonusList.Any(a => a.BonusType == (newBonus.BonusType)))
                {
                    if (newBonus.CanStack)
                    {
                        bonusList.Add(newBonus);
                    }
                }
                else
                {
                    bonusList.Add(newBonus);
                }

            }
            bonusList.Add(newBonus);



        }
    }
}
