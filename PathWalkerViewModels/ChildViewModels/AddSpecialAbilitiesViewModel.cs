using System.Collections.ObjectModel;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathWalkerViewModels.ChildViewModels
{
    public class AddSpecialAbilitiesViewModel : ChildViewModel
    {
        public AddSpecialAbilitiesViewModel(Character character, EventAggregator eventAggregator)
        {

            Character = character;
            EventAggregator = eventAggregator;
            AddSpecialAbilityToListCommand = new RelayCommand(AddSpecialAbilityToList);
            AddBonusToSpecialAbilityCommand = new RelayCommand(AddBonusToSpecialAbility);
            SpecialAbilities = Character.SpecialAbilities;

        }

        private void AddBonusToSpecialAbility()
        {
            NewBonus.BonusSource = NewSpecialAbility.Name;
            BonusService.AddBonusToList(NewBonus,NewSpecialAbility.BonusList);
            NewBonus = new Bonus();
        }

        private void AddSpecialAbilityToList()
        {
            Character.SpecialAbilities.Add(NewSpecialAbility);
            NewSpecialAbility = new SpecialAbility();
        }

        public ObservableCollection<SpecialAbility> SpecialAbilities { get; set; } =
            new ObservableCollection<SpecialAbility>();

        public SpecialAbility NewSpecialAbility { get; set; } = new SpecialAbility();
        public Bonus NewBonus { get; set; } = new Bonus();
        public ICommand AddSpecialAbilityToListCommand { get; set; }
        public ICommand AddBonusToSpecialAbilityCommand { get; set; }
    }
}
