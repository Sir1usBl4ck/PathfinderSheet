namespace PathfinderSheetModels
{
    public abstract class BaseAttribute : ObservableObject
    {
        public abstract int BaseScore { get; set; }

        public abstract int Score { get; }
    }
}