using Newtonsoft.Json;

namespace UserInterface.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GeneralFeat
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("prerequisites")]
        public string Prerequisites { get; set; }

        [JsonProperty("prerequisite_feats")]
        public string PrerequisiteFeats { get; set; }

        [JsonProperty("benefit")]
        public string Benefit { get; set; }

        [JsonProperty("normal")]
        public string Normal { get; set; }

        [JsonProperty("special")]
        public string Special { get; set; }

        [JsonProperty("teamwork")]
        public bool Teamwork { get; set; }

        [JsonProperty("critical")]
        public bool Critical { get; set; }

        [JsonProperty("grit")]
        public bool Grit { get; set; }

        [JsonProperty("style")]
        public bool Style { get; set; }

        [JsonProperty("performance")]
        public bool Performance { get; set; }

        [JsonProperty("racial")]
        public bool Racial { get; set; }

        [JsonProperty("companion_familiar")]
        public bool CompanionFamiliar { get; set; }

        [JsonProperty("race_name")]
        public string RaceName { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("goal")]
        public string Goal { get; set; }

        [JsonProperty("completion_benefit")]
        public string CompletionBenefit { get; set; }

        [JsonProperty("multiples")]
        public bool Multiples { get; set; }

        [JsonProperty("suggested_traits")]
        public string SuggestedTraits { get; set; }

        [JsonProperty("prerequisite_skills")]
        public string PrerequisiteSkills { get; set; }

        [JsonProperty("panache")]
        public bool Panache { get; set; }

        [JsonProperty("betrayal")]
        public bool Betrayal { get; set; }

        [JsonProperty("targeting")]
        public bool Targeting { get; set; }

        [JsonProperty("esoteric")]
        public bool Esoteric { get; set; }

        [JsonProperty("stare")]
        public bool Stare { get; set; }

        [JsonProperty("weapon_mastery")]
        public bool WeaponMastery { get; set; }

        [JsonProperty("item_mastery")]
        public bool ItemMastery { get; set; }

        [JsonProperty("armor_mastery")]
        public bool ArmorMastery { get; set; }

        [JsonProperty("shield_mastery")]
        public bool ShieldMastery { get; set; }

        [JsonProperty("blood_hex")]
        public bool BloodHex { get; set; }

        [JsonProperty("trick")]
        public bool Trick { get; set; }
    }


}
