namespace PathfinderSheetModels
{
    public class CharacterChangedEvent
    {
        public CharacterChangedEvent(Character character)
        {
            Character = character;
        }

        public Character Character { get; set; }
    }
}
