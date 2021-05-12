namespace PathfinderSheetModels
{
    public class AvailableSkillRanksChanged
    {
        public AvailableSkillRanksChanged(int availableSkillRanks)
        {
            AvailableSkillRanks = availableSkillRanks;
        }

        public int AvailableSkillRanks { get; set; }
    }
}
