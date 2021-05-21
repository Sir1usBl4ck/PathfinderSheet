using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class CharacterInitializer
    {

        public static void InitializeCharacterAbilities(Character character, EventAggregator eventAggregator)
        {
            var Strength = new Ability(AttributeType.Strength, "Strength", "STR");
            var Dexterity = new Ability(AttributeType.Dexterity, "Dexterity", "DEX");
            var Constitution = new Ability(AttributeType.Constitution, "Constitution", "CON");
            var Intelligence = new Ability(AttributeType.Intelligence, "Intelligence", "INT");
            var Wisdom = new Ability(AttributeType.Wisdom, "Wisdom", "WIS");
            var Charisma = new Ability(AttributeType.Charisma, "Charisma", "CHA");

            character.Abilities.Add(Strength);
            character.Abilities.Add(Dexterity);
            character.Abilities.Add(Constitution);
            character.Abilities.Add(Intelligence);
            character.Abilities.Add(Wisdom);
            character.Abilities.Add(Charisma);

            foreach (var ability in character.Abilities)
            {
                ability.EventAggregator = eventAggregator;
                ability.EventAggregator.Subscribe(ability);

            }
            
            character.Skills.Add(new Skill(Dexterity, "Acrobatics", AttributeType.Acrobatics, false, true));
            character.Skills.Add(new Skill(Intelligence, "Appraise", AttributeType.Appraise));
            character.Skills.Add(new Skill(Charisma, "Bluff", AttributeType.Bluff));
            character.Skills.Add(new Skill(Strength, "Climb", AttributeType.Climb, false, true));
            character.Skills.Add(new Skill(Intelligence, "Craft", AttributeType.Craft));
            character.Skills.Add(new Skill(Charisma, "Diplomacy", AttributeType.Diplomacy));
            character.Skills.Add(new Skill(Dexterity, "Disable Device", AttributeType.DisableDevice, true, true));
            character.Skills.Add(new Skill(Charisma, "Disguise", AttributeType.Disguise));
            character.Skills.Add(new Skill(Dexterity, "Escape Artist", AttributeType.EscapeArtist, false, true));
            character.Skills.Add(new Skill(Dexterity, "Fly", AttributeType.Fly, false, true));
            character.Skills.Add(new Skill(Charisma, "Handle Animal", AttributeType.HandleAnimal, true));
            character.Skills.Add(new Skill(Wisdom, "Heal", AttributeType.Heal));
            character.Skills.Add(new Skill(Charisma, "Intimidate", AttributeType.Intimidate));
            character.Skills.Add(new Skill(Intelligence, "Knowldge Arcana", AttributeType.KnowledgeArcana, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Dungeoneering", AttributeType.KnowledgeDungeoneering, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Engineering", AttributeType.KnowledgeEngineering, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Geography", AttributeType.KnowledgeGeography, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge History", AttributeType.KnowledgeHistory, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Local", AttributeType.KnowledgeLocal, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Nature", AttributeType.KnowledgeNature, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Nobility", AttributeType.KnowledgeNobility, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Planes", AttributeType.KnowledgePlanes, true));
            character.Skills.Add(new Skill(Intelligence, "Knowledge Religion", AttributeType.KnowledgeReligion, true));
            character.Skills.Add(new Skill(Intelligence, "Linguistics", AttributeType.Linguistics, true));
            character.Skills.Add(new Skill(Wisdom, "Perception", AttributeType.Perception));
            character.Skills.Add(new Skill(Charisma, "Perform", AttributeType.Perform));
            character.Skills.Add(new Skill(Wisdom, "Profession", AttributeType.Profession, true));
            character.Skills.Add(new Skill(Dexterity, "Ride", AttributeType.Ride, false, true));
            character.Skills.Add(new Skill(Wisdom, "Sense Motive", AttributeType.SenseMotive));
            character.Skills.Add(new Skill(Dexterity, "Sleight of Hand", AttributeType.SleightOfHand, true, true));
            character.Skills.Add(new Skill(Intelligence, "Spellcraft", AttributeType.Spellcraft, true));
            character.Skills.Add(new Skill(Dexterity, "Stealth", AttributeType.Stealth, false, true));
            character.Skills.Add(new Skill(Wisdom, "Survival", AttributeType.Survival));
            character.Skills.Add(new Skill(Strength, "Swim", AttributeType.Swim, false, true));
            character.Skills.Add(new Skill(Charisma, "Use Magic Device", AttributeType.UseMagicDevice, true));

            foreach (var skill in character.Skills)
            {
                skill.EventAggregator = eventAggregator;
                skill.EventAggregator.Subscribe(skill);
            }

            
            character.Saves.Add(new Save(AttributeType.Fortitude, Constitution));
            character.Saves.Add(new Save(AttributeType.Reflexes, Dexterity));
            character.Saves.Add(new Save(AttributeType.Willpower, Wisdom));
            foreach (var save in character.Saves)
            {
                save.EventAggregator = eventAggregator;
                save.EventAggregator.Subscribe(save);
            }


            character.ArmorClass = new ArmorClass(Dexterity, eventAggregator);
            
            character.ArmorClass.EventAggregator.Subscribe(character.ArmorClass);

        }
        

    }
}
