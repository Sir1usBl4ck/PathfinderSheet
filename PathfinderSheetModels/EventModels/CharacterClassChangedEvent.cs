namespace PathfinderSheetModels
{
    public class CharacterClassChangedEvent
    {
        public CharacterClass CharacterClass { get; set; }

        public CharacterClassChangedEvent(CharacterClass characterClass)
        {
            CharacterClass = characterClass;
        }
    }
}
