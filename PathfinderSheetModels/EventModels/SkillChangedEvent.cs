
namespace PathfinderSheetModels

{
    public class SkillChangedEvent
    {
        public SkillChangedEvent(Skill skill)
        {
            Skill = skill;
        }

        public Skill Skill { get; set; }

    }
}
