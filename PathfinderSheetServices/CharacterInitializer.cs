using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class CharacterInitializer
    {
        public static void InitializeCharacterAbilities(Character character, EventAggregator eventAggregator)
        {
            var Strength = new Ability(AttributeType.Strength);
            var Dexterity = new Ability(AttributeType.Dexterity);
            var Constitution = new Ability(AttributeType.Constitution);
            var Intelligence = new Ability(AttributeType.Intelligence);
            var Wisdom = new Ability(AttributeType.Wisdom);
            var Charisma = new Ability(AttributeType.Charisma);

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
        }

        public static void InitializeCharacterSaves(Character character, EventAggregator eventAggregator)
        {
            var Fortitude = new Save();
            var Reflexes = new Save();
            var Willpower = new Save();

            character.Saves.Add(Fortitude);
            character.Saves.Add(Reflexes);
            character.Saves.Add(Reflexes);
            foreach (var save in character.Saves)
            {
                save.EventAggregator = eventAggregator;
                save.EventAggregator.Subscribe(save);
            }
        }




    }
}
