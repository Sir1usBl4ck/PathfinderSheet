using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathWalkerViewModels.ChildViewModels
{
    public class AddAttackViewModel : ChildViewModel
    {
        public AddAttackViewModel(Character character, EventAggregator eventAggregator)
        {
            Character = character;
            EventAggregator = eventAggregator;
            NewAttack = new Attack(EventAggregator);
            NewAttack.AttackAbility = Character.Abilities[0];
            NewAttack.DamageAbility = Character.Abilities[0];
            NewAttack.EventAggregator.Subscribe(NewAttack);
            AttackService = new AttackService(character, EventAggregator);
            AttackService.EventAggregator.Subscribe(AttackService);
            AttackService.BaseAttackBonus = character.BaseAttackBonus;
            AttackService.Subscribe(NewAttack);
            AddAttackToCharacterCommand = new RelayCommand(AddAttackToCharacter);
        }

        public AttackService AttackService { get; set; }
        private void AddAttackToCharacter()
        {
            Character.AttackList.Add(NewAttack);
            AttackService.Unsubscribe(NewAttack);
            NewAttack = new Attack(EventAggregator);
            OnPropertyChanged(nameof(NewAttack));
        }

        public Attack NewAttack { get; set; } 

        public ICommand AddAttackToCharacterCommand { get; set; }
    }
}
