using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class BonusService 
    {
        public static EventAggregator EventAggregator { get; set; }
        public static Character Character { get; set; }

        public static List<IBonusable> BonusReceivers { get; set; }

        public static void InitializeBonusReceivers()
        {
            BonusReceivers = new List<IBonusable>();
            foreach (var characterAbility in Character.Abilities)
            {
                BonusReceivers.Add(characterAbility);
            }

            foreach (var characterSkill in Character.Skills)
            {
                BonusReceivers.Add(characterSkill);
            }

            foreach (var characterSave in Character.Saves)
            {
                BonusReceivers.Add(characterSave);
            }

            BonusReceivers.Add(Character.ArmorClass);

        }

        public static void ApplyBonusToTargets(ObservableCollection<Bonus> providerBonusList)
        {
            
            foreach (var providedBonus in providerBonusList)
            {
                foreach (var target in BonusReceivers)
                {

                    if (providedBonus.Target == target.AttributeType)
                    {
                        foreach (var bonus in target.BonusList)
                        {
                            if (bonus.BonusSource == providedBonus.BonusSource)
                            {
                                target.BonusList.Remove(bonus);
                            }
                        }
                        target.BonusList.Add(providedBonus);

                        UpdateAppliedBonusList();

                    }
                }
            }

        }

        public static void UpdateAppliedBonusList()
        {
            foreach (var target in BonusReceivers)
            {

                target.ActiveBonusList = new ObservableCollection<Bonus>();

                foreach (BonusType bonusType in Enum.GetValues(typeof(BonusType)))
                {

                    var typedList = target.BonusList.Where(a => a.BonusType == bonusType);

                    if (typedList.Any())
                    {
                        target.ActiveBonusList.Add(typedList.OrderByDescending(a => a.Value).First());
                        target.RecalculateScore();
                        
                        

                    }
                }
            }
        }

        public static void RemoveBonusFromTargets(ObservableCollection<Bonus> providerBonusList)
        {
            foreach (var target in BonusReceivers)
            {
                foreach (var bonus in providerBonusList)
                {
                    if (target.BonusList.Contains(bonus))
                    {
                        target.BonusList.Remove(bonus);
                        target.RecalculateScore();

                    }
                }

            }

            UpdateAppliedBonusList();
        }

        public static void ApplyBonus(ObservableCollection<Bonus> bonusList)
        {
            ApplyBonusToTargets(bonusList);
        }

        public static void RemoveBonus(ObservableCollection<Bonus> bonusList)
        {
            RemoveBonusFromTargets(bonusList);
        }

        public static void AddBonusToList(Bonus newBonus, ObservableCollection<Bonus> bonusList)
        {

            if (bonusList.Any(a => a.BonusSource == newBonus.BonusSource))
            {
                bonusList.Remove(bonusList.FirstOrDefault(a => a.BonusSource == newBonus.BonusSource));
                if (bonusList.Any(a => a.BonusType == (newBonus.BonusType)))
                {
                    if (newBonus.CanStack)
                    {
                        bonusList.Add(newBonus);
                    }
                }
                else
                {
                    bonusList.Add(newBonus);
                }

            }
            bonusList.Add(newBonus);



        }
    }
}
