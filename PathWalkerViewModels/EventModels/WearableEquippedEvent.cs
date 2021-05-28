using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;

namespace PathWalkerViewModels.EventModels
{
    public class WearableEquippedEvent
    {
        public WearableEquippedEvent(Item item, bool isEquipped)
        {
            Item = item;
            IsEquipped = isEquipped;
        }
        public Item Item { get; set; }
        public bool IsEquipped { get; set; }
    }
}
