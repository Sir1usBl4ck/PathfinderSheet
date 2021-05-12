namespace PathfinderSheetModels
{
    public class LevelChangedEvent
    {
        public LevelChangedEvent(int level)
        {
            Level = level;
            
        }

        public int Level { get; set; }
        
    }
}
