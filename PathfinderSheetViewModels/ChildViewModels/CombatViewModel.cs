using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetModels;

namespace PathfinderSheetViewModels.ChildViewModels
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
