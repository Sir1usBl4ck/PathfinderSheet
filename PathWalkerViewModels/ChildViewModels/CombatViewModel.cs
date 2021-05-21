using System.Windows.Input;
using PathfinderSheetModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class CombatViewModel : ChildViewModel
    {
        public CombatViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            DeleteAttackCommand = new RelayCommand<Attack>(DeleteAttack);
        }

        private void DeleteAttack(Attack obj)
        {
            Character.AttackList.Remove(obj);
        }

        public ICommand DeleteAttackCommand { get; set; }



    }
}
