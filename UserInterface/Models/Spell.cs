using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UserInterface.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Spell
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("subschool")]
        public string Subschool { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }

        [JsonProperty("spell_level")]
        public string SpellLevel { get; set; }

        [JsonProperty("casting_time")]
        public string CastingTime { get; set; }

        [JsonProperty("components")]
        public string Components { get; set; }

        [JsonProperty("costly_components")]
        public int CostlyComponents { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("effect")]
        public string Effect { get; set; }

        [JsonProperty("targets")]
        public string Targets { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("dismissible")]
        public bool Dismissible { get; set; }

        [JsonProperty("shapeable")]
        public bool Shapeable { get; set; }

        [JsonProperty("saving_throw")]
        public string SavingThrow { get; set; }

        [JsonProperty("spell_resistance")]
        public string SpellResistance { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("verbal")]
        public bool Verbal { get; set; }

        [JsonProperty("somatic")]
        public bool Somatic { get; set; }

        [JsonProperty("material")]
        public bool Material { get; set; }

        [JsonProperty("focus")]
        public bool Focus { get; set; }

        [JsonProperty("divine_focus")]
        public bool DivineFocus { get; set; }

        [JsonProperty("sor")]
        public int? Sorcerer { get; set; }

        [JsonProperty("wiz")]
        public int? Wizard { get; set; }

        [JsonProperty("cleric")]
        public int? Cleric { get; set; }

        [JsonProperty("druid")]
        public int? Druid { get; set; }

        [JsonProperty("ranger")]
        public int? Ranger { get; set; }

        [JsonProperty("bard")]
        public int? Bard { get; set; }

        [JsonProperty("paladin")]
        public int? Paladin { get; set; }

        [JsonProperty("alchemist")]
        public int? Alchemist { get; set; }

        [JsonProperty("summoner")]
        public int? Summoner { get; set; }

        [JsonProperty("witch")]
        public int? Witch { get; set; }

        [JsonProperty("inquisitor")]
        public int? Inquisitor { get; set; }

        [JsonProperty("oracle")]
        public int? Oracle { get; set; }

        [JsonProperty("antipaladin")]
        public int? Antipaladin { get; set; }

        [JsonProperty("magus")]
        public int? Magus { get; set; }

        [JsonProperty("adept")]
        public int? Adept { get; set; }

        [JsonProperty("deity")]
        public string Deity { get; set; }

        [JsonProperty("SLA_Level")]
        public int? SLALevel { get; set; }

        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }

        [JsonProperty("acid")]
        public bool Acid { get; set; }

        [JsonProperty("air")]
        public bool Air { get; set; }

        [JsonProperty("chaotic")]
        public bool Chaotic { get; set; }

        [JsonProperty("cold")]
        public bool Cold { get; set; }

        [JsonProperty("curse")]
        public bool Curse { get; set; }

        [JsonProperty("darkness")]
        public bool Darkness { get; set; }

        [JsonProperty("death")]
        public bool Death { get; set; }

        [JsonProperty("disease")]
        public bool Disease { get; set; }

        [JsonProperty("earth")]
        public bool Earth { get; set; }

        [JsonProperty("electricity")]
        public bool Electricity { get; set; }

        [JsonProperty("emotion")]
        public bool Emotion { get; set; }

        [JsonProperty("evil")]
        public bool Evil { get; set; }

        [JsonProperty("fear")]
        public bool Fear { get; set; }

        [JsonProperty("fire")]
        public bool Fire { get; set; }

        [JsonProperty("force")]
        public bool Force { get; set; }

        [JsonProperty("good")]
        public bool Good { get; set; }

        [JsonProperty("language_dependent")]
        public bool LanguageDependent { get; set; }

        [JsonProperty("lawful")]
        public bool Lawful { get; set; }

        [JsonProperty("light")]
        public bool Light { get; set; }

        [JsonProperty("mind_affecting")]
        public bool MindAffecting { get; set; }

        [JsonProperty("pain")]
        public bool Pain { get; set; }

        [JsonProperty("poison")]
        public bool Poison { get; set; }

        [JsonProperty("shadow")]
        public bool Shadow { get; set; }

        [JsonProperty("sonic")]
        public bool Sonic { get; set; }

        [JsonProperty("water")]
        public bool Water { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mythic")]
        public int Mythic { get; set; }

        [JsonProperty("bloodrager")]
        public int? Bloodrager { get; set; }

        [JsonProperty("shaman")]
        public int? Shaman { get; set; }

        [JsonProperty("psychic")]
        public int? Psychic { get; set; }

        [JsonProperty("medium")]
        public int? Medium { get; set; }

        [JsonProperty("mesmerist")]
        public int? Mesmerist { get; set; }

        [JsonProperty("occultist")]
        public int? Occultist { get; set; }

        [JsonProperty("spiritualist")]
        public int? Spiritualist { get; set; }

        [JsonProperty("skald")]
        public int? Skald { get; set; }

        [JsonProperty("investigator")]
        public int? Investigator { get; set; }

        [JsonProperty("hunter")]
        public int? Hunter { get; set; }

        [JsonProperty("summoner_unchained")]
        public int? SummonerUnchained { get; set; }

        public int SelectedClassLevel { get; set; }

        public List<string> ClassList { get; set; }
    }




}
