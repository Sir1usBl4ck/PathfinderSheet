using System.Collections.ObjectModel;
using System.Diagnostics;
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
            NewSpecialAbility = new SpecialAbility();

        }

        private void AddBonusToSpecialAbility()
        {
            NewBonus.BonusSource = NewSpecialAbility.Name;
            if (NewSpecialAbility.IsActive == false)
            {
                BonusService.AddBonusToList(NewBonus, NewSpecialAbility.BonusList);

            }
            NewSpecialAbility.BonusList.Add(NewBonus);
            NewBonus = new Bonus();
        }

        private void AddSpecialAbilityToList()
        {
            Character.SpecialAbilities.Add(NewSpecialAbility);
            NewSpecialAbility = new SpecialAbility();



        }



        public SpecialAbility NewSpecialAbility { get; set; }
        public Bonus NewBonus { get; set; } = new Bonus();
        public ICommand AddSpecialAbilityToListCommand { get; set; }
        public ICommand AddBonusToSpecialAbilityCommand { get; set; }
    }
}
