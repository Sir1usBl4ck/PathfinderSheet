using System;

namespace PathfinderSheetModels
{
    public enum TypeOfAttack
    {
        Melee,
        Ranged,
        Spell,
    }

    public class Attack : ObservableObject, IHandle<BaseAttackBonusChangedEvent>, IHandle<AbilityChangedEvent>
    {
        private int _attackBonus;
        private string _name;
        private int _diceNumber;
        private int _damageBonus;
        private int _dice;
        private int _attackAbilityModifier;
        private int _damageAbilityModifier;
        private bool _isRanged;
        private bool _isTwoHanded;
        private int _criticalMultiplier;
        private EventAggregator _eventAggregator;
        
        public Attack(EventAggregator eventAggregator)
        {
            Name = "Insert Name";
            AttackType = TypeOfAttack.Melee;
            AttackAbilityType = AbilityType.Strength;
            DamageAbilityType = AbilityType.Strength;
            ThreatRange = 19;
            EventAggregator = eventAggregator;

        }
        public TypeOfAttack AttackType { get; set; }
        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set => _eventAggregator = value;
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; OnPropertyChanged();
            }
        }
        public int Dice
        {
            get => _dice;
            set
            {
                _dice = value;
                OnPropertyChanged();
            }
        }
        public int DiceNumber
        {
            get { return _diceNumber; }
            set
            {
                _diceNumber = value; OnPropertyChanged();
            }
        }
        public int CriticalMultiplier
        {
            get => _criticalMultiplier;
            set
            {
                _criticalMultiplier = value;
                OnPropertyChanged();
            }
        }
        public int MagicWeaponBonus { get; set; }
        public AbilityType AttackAbilityType { get; set; }
        public AbilityType DamageAbilityType { get; set; }
        public bool IsRapidShot { get; set; }
        public bool IsTwoHanded
        {
            get => _isTwoHanded;
            set
            {
                _isTwoHanded = value;
                OnPropertyChanged();
                UpdateDamageBonus();

            }
        }
        public bool IsPowerAttack { get; set; }
        public bool IsComposite { get; set; }
        public bool IsSneakAttack { get; set; }
        public int ThreatRange { get; set; }
        public int BaseAttackBonus { get; set; }
        public int AttackAbilityModifier
        {
            get => _attackAbilityModifier;
            set
            {
                _attackAbilityModifier = value;
                OnPropertyChanged();
                OnPropertyChanged("AttackBonus");
            }
        }
        public int DamageAbilityModifier
        {
            get => _damageAbilityModifier;
            set
            {
                _damageAbilityModifier = value;
                OnPropertyChanged();
            }
        }
        public int SizeModifier { get; set; } // TODO
        public int AttackBonus
        {
            get => _attackBonus;
            set
            {
                _attackBonus = value;
                OnPropertyChanged();

            }
        }
        public int DamageBonus
        {
            get => _damageBonus;
            set
            {
                _damageBonus = value;
                OnPropertyChanged();
            }
        }
        public void UpdateAttackBonus()
        {
            AttackBonus = BaseAttackBonus + AttackAbilityModifier + SizeModifier;
        }
        public void UpdateDamageBonus()
        {
            if (IsTwoHanded && DamageAbilityType == AbilityType.Strength)
            {
                DamageBonus = Convert.ToInt32(DamageAbilityModifier * 1.5);
            }
            else
            {
                DamageBonus = DamageAbilityModifier;

            }
        }
        public void UpdateAttackAbilityModifier(int modifier, AbilityType type)
        {
            if (AttackAbilityType == type)
            {
                AttackAbilityModifier = modifier;
            }
        }
        public void UpdateDamageAbilityModifier(int modifier, AbilityType type)
        {
            if (DamageAbilityType == type)
            {
                DamageAbilityModifier = modifier;

            }
        }
        public void Handle(BaseAttackBonusChangedEvent message)
        {
            BaseAttackBonus = message.BaseAttackBonus;
            UpdateAttackBonus();

        }
        public void Handle(AbilityChangedEvent message)
        {
            UpdateDamageAbilityModifier(message.Ability.Modifier, message.Ability.Type);
            UpdateAttackAbilityModifier(message.Ability.Modifier, message.Ability.Type);
            UpdateAttackBonus();
            UpdateDamageBonus();

        }

       
    }
}
