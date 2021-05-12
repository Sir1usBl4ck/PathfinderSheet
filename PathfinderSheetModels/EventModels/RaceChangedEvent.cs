namespace PathfinderSheetModels
{
    public class RaceChangedEvent
    {
        public RaceChangedEvent(Race race)
        {
            Race = race;
        }

        public Race Race { get; set; }


    }
}
