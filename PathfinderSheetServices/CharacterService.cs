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
            SetClassSkills(character.CharacterClass, character);
            SetBab(character);
            SetGoodSave(character.Saves, character.CharacterClass);

        }

        public static void SetGoodSave(ObservableCollection<Save> saves, CharacterClass characterClass)
        {
            foreach (var save in saves)
            {
                if (save.AttributeType == characterClass.GoodSave)
                {
                    save.IsGood = true;
                }
            }
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
        public static void WriteClassSpellLevelInSpells(Character character, ICollection<Spell> spells)
        {
            foreach (var spell in spells)
            {
                if (spell.ClassSpellLevelDictionary.ContainsKey(character.CharacterClass.Name))
                {
                    spell.ClassLevel = spell.ClassSpellLevelDictionary[character.CharacterClass.Name];

                }

            }
        }


    }
}
