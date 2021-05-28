using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathWalkerViewModels.ChildViewModels
{
    public class FeatsViewModel : ChildViewModel
    {
        public FeatsViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
            AddSpecialAbilityViewModel = new AddSpecialAbilitiesViewModel(Character, EventAggregator);
            ActivateSpecialAbilityCommand = new RelayCommand<SpecialAbility>(ActivateSpecialAbility);
        }

        private void ActivateSpecialAbility(SpecialAbility obj)
        {
            obj.IsCurrentlyActive = true;
        }

        public ChildViewModel AddSpecialAbilityViewModel { get; set; }
        
        public ICommand ActivateSpecialAbilityCommand { get; set; }
    }
}
