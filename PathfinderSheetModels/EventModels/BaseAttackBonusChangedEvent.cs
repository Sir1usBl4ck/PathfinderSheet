using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.EventModels
{
    public class BaseAttackBonusChangedEvent
    {
        public BaseAttackBonusChangedEvent(int baseAttackBonus)
        {
            BaseAttackBonus = baseAttackBonus;
        }

        public int BaseAttackBonus { get; set; }
    }
}
