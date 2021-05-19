using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class CharacterService
    {

        public static void ApplyClass(Character character)
        {
            SetClassSkills(character.CharacterClass,character);
            SetBab(character);
        }

        public static void SetClassSkills(CharacterClass characterClass, Character character)
        {
            foreach (var skill in character.Skills)
            {
                if (characterClass.ClassSkillNames.Contains(skill.AttributeType))
                {
                    skill.IsClass = true;
                }
            }

        }


        

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
