using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderSheetModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AttributeType
    {
        
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma,

        Fortitude,
        Reflexes,
        Willpower,

        Acrobatics,
        Appraise,
        Bluff,
        Climb,
        Craft,
        Diplomacy,
        DisableDevice,
        Disguise,
        EscapeArtist,
        Fly,
        HandleAnimal,
        Heal,
        Intimidate,
        KnowledgeArcana,
        KnowledgeDungeoneering,
        KnowledgeEngineering,
        KnowledgeGeography,
        KnowledgeHistory,
        KnowledgeLocal,
        KnowledgeNature,
        KnowledgeNobility,
        KnowledgePlanes,
        KnowledgeReligion,
        Linguistics,
        Perception,
        Perform,
        Profession,
        Ride,
        SenseMotive,
        SleightOfHand,
        Spellcraft,
        Stealth,
        Survival,
        Swim,
        UseMagicDevice,

    }
}