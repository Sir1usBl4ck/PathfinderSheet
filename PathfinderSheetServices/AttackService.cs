using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;
using PathfinderSheetModels.EventModels;

namespace PathfinderSheetServices
{
    public class AttackService :IHandle<AbilityChangedEvent>, IHandle<LevelChangedEvent>, IHandle<BaseAttackBonusChangedEvent>, IHandle<AttackChangedEvent>
    {

        public AttackService(Character character,EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

        }
        public EventAggregator EventAggregator { get; set; }
        public static List<Attack> Attacks { get; set; } = new List<Attack>();
        public static int BaseAttackBonus { get; set; }
        public static int Level { get; set; }
        public static void CalculateDamageBonus(Attack attack)
        {
            

            if (attack.AttackType == TypeOfAttack.Melee)
            {
                var meleeBonusSum = attack.DamageBonusList.Where(a => a.Target == AttributeType.MeleeAttack)
                    .Sum(a => a.Value);
                var powerAttackBonus = 0;
                if (attack.IsPowerAttack)
                {
                    powerAttackBonus = Convert.ToInt32(2+(BaseAttackBonus /4));
                    if (attack.IsOffHand)
                    {
                        powerAttackBonus /= 2;
                    }
                }

                if (attack.IsTwoHanded && attack.AttackAbility.AttributeType == AttributeType.Strength)
                {
                    attack.DamageBonus = Convert.ToInt32(attack.DamageAbility.Modifier * 1.5) +
                                         attack.MagicWeaponBonus +
                                         meleeBonusSum + powerAttackBonus;
                }
                else
                {
                    attack.DamageBonus = attack.DamageAbility.Modifier + attack.MagicWeaponBonus +
                                         meleeBonusSum + powerAttackBonus; ;
                }
            }

            if (attack.AttackType == TypeOfAttack.Spell)
            {
                var spellBonusSum = attack.DamageBonusList.Where(a => a.Target == AttributeType.SpellAttack)
                    .Sum(a => a.Value);
                attack.DamageBonus = spellBonusSum;

            }

            if (attack.AttackType == TypeOfAttack.Ranged)
            {

               //TODO Add Logic for Ranged AttackDamage
            }
        }

        public static void CalculateAttackBonus(Attack attack)
        {
            var powerAttackMalus = 0;
            if (attack.IsPowerAttack)
            {
                powerAttackMalus = Convert.ToInt32(1+(BaseAttackBonus /4));
            }

            attack.AttackBonus = BaseAttackBonus + attack.AttackAbility.Modifier + attack.MagicWeaponBonus +
                                 attack.AttackBonusList.Sum(a => a.Value)+ powerAttackMalus;
        }

        public static void Subscribe(Attack attack)
        {
            if (!Attacks.Contains(attack))
            {
                Attacks.Add(attack);
                
            }

        }

        public void Handle(LevelChangedEvent message)
        {
            Level = message.Level;
            foreach (var attack in Attacks)
            {
                CalculateDamageBonus(attack);
                CalculateAttackBonus(attack);
            }
        }

        public void Handle(BaseAttackBonusChangedEvent message)
        {
            BaseAttackBonus = message.BaseAttackBonus;
            foreach (var attack in Attacks)
            {
                CalculateDamageBonus(attack);
                CalculateAttackBonus(attack);
            }
        }

        public void Handle(AttackChangedEvent message)
        {
            foreach (var attack in Attacks)
            {
                CalculateDamageBonus(attack);
                CalculateAttackBonus(attack);
            }
        }

        public void Handle(AbilityChangedEvent message)
        {
            foreach (var attack in Attacks)
            {
                if (attack.DamageAbility.AttributeType == message.Ability.AttributeType)
                {
                    attack.DamageAbility = message.Ability;
                    CalculateDamageBonus(attack);
                    CalculateAttackBonus(attack);
                }
            }
        }

        public void Unsubscribe(Attack newAttack)
        {
            Attacks.Remove(newAttack);
        }
    }
}
