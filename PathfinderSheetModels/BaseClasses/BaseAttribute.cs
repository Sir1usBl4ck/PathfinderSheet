namespace PathfinderSheetModels
{
    public abstract class BaseAttribute : ObservableObject
    {
        public int BaseScore { get; set; }
        public abstract int Score { get; }
    }
}