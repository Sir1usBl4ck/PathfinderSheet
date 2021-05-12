using System;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class CharacterService
    {
        
        public static void SetBab(Character character)
        {
            if (character.CharacterClass != null) 
            {
                double dBaseAttackBonus = character.Level * character.CharacterClass.BaBProgression;
                character.BaseAttackBonus = (int)Math.Floor(dBaseAttackBonus);
            }
        }


    }
}
