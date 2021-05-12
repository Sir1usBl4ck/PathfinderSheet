namespace PathfinderSheetModels
{
    public class AbilityChangedEvent
    {
        public Ability Ability { get; set; }

        public AbilityChangedEvent(Ability ability)
        {
            Ability = ability;
        }

    }
}
