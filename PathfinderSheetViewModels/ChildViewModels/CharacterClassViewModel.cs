using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;

namespace PathfinderSheetViewModels.ChildViewModels
{
    public class CharacterClassViewModel : ChildViewModel
    {
        public CharacterClassViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            var dataLoader = DataLoader.GetDataLoader();
            CharacterClasses = dataLoader.CharacterClasses;

        }

        public ObservableCollection<CharacterClass> CharacterClasses { get; set; }

    }
}
