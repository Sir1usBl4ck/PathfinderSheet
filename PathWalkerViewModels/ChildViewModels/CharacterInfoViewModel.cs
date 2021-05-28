using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathWalkerViewModels.ChildViewModels
{
    public class CharacterInfoViewModel : ChildViewModel
    {
        private long[] _xpTable;
        public CharacterInfoViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            XpTable = ExperienceService.Table(Progression.Medium);


        }
        public long[] XpTable
        {
            get => _xpTable;
            set
            {
                _xpTable = value;
                OnPropertyChanged(nameof(OldXpToLevel));
                OnPropertyChanged(nameof(XpToLevel));
            }
        }

        public long OldXpToLevel => Character.Level != 1 ? 0 : XpTable[Character.Level];

        public long XpToLevel => XpTable[Character.Level + 1];
    }
}
