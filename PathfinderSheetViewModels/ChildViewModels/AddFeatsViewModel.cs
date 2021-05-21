using System.Collections.ObjectModel;
using System.Windows.Input;
using PathfinderSheetDataAccess;
using PathfinderSheetModels;

namespace PathfinderSheetViewModels.ChildViewModels
{
    public class AddFeatsViewModel : ChildViewModel
    {
        public AddFeatsViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            var dataLoader = DataLoader.GetDataLoader();
            Feats = dataLoader.Feats;
            AddFeatToCharacterCommand = new RelayCommand<GeneralFeat>(AddFeatToCharacter);

        }

        private void AddFeatToCharacter(GeneralFeat obj)
        {
            if (!Character.Feats.Contains(obj))
                Character.Feats.Add(obj);
        }

        public ObservableCollection<GeneralFeat> Feats { get; set; }
        public ICommand AddFeatToCharacterCommand { get; set; }




    }
}
