using System;
using System.Collections.ObjectModel;
using System.Linq;
using PathfinderSheetModels.EventModels;

namespace PathfinderSheetModels
{
    public enum TypeOfAttack
    {
        Melee,
        Ranged,
        Spell,
    }

    public class Attack : ObservableObject
    {
        private int _attackBonus;
        private string _name;
        private int _damageBonus;
        private bool _isTwoHanded;
        private Ability _attackAbility;
        private Ability _damageAbility;
        private int _magicWeaponBonus;
        private bool _isPowerAttack;
        private bool _isOffHand;


        public Attack(EventAggregator eventAggregator)
        {
            AttackType = TypeOfAttack.Melee;
            ThreatRange = 19;
            EventAggregator = eventAggregator;

        }

        public TypeOfAttack AttackType { get; set; }
        public EventAggregator EventAggregator { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; OnPropertyChanged();
            }
        }
        public int Dice { get; set; }
        public int DiceNumber { get; set; }
        public int CriticalMultiplier { get; set; }
        public int MagicWeaponBonus
        {
            get => _magicWeaponBonus;
            set
            {
                _magicWeaponBonus = value;
                OnPropertyChanged();
                EventAggregator.Publish(new AttackChangedEvent(this));

            }
        }
        public Ability AttackAbility
        {
            get => _attackAbility;
            set
            {
                _attackAbility = value;
                EventAggregator.Publish(new AttackChangedEvent(this));


            }
        }
        public Ability DamageAbility
        {
            get => _damageAbility;
            set
            {
                _damageAbility = value;
                EventAggregator.Publish(new AttackChangedEvent(this));
            }
        }
        public bool IsRapidShot { get; set; }
        public bool IsTwoHanded
        {
            get => _isTwoHanded;
            set
            {
                _isTwoHanded = value;
                EventAggregator.Publish(new AttackChangedEvent(this));


            }
        }
        public bool IsComposite { get; set; }
        public bool IsOffHand
        {
            get { return _isOffHand; }
            set
            {
                _isOffHand = value;
                EventAggregator.Publish(new AttackChangedEvent(this));

            }
        }
        public bool IsPowerAttack
        {
            get { return _isPowerAttack; }
            set
            {
                _isPowerAttack = value;
                EventAggregator.Publish(new AttackChangedEvent(this));
            }
        }
        public bool IsSneakAttack { get; set; }
        public int ThreatRange { get; set; }
        public int AttackAbilityModifier { get; set; }
        public int DamageAbilityModifier { get; set; }
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
        public ObservableCollection<Bonus> AttackBonusList { get; set; } = new ObservableCollection<Bonus>();
        public ObservableCollection<Bonus> DamageBonusList { get; set; } = new ObservableCollection<Bonus>();




    }
}
