using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PathfinderSheetModels;

namespace PathfinderSheetDataAccess
{
    public sealed class DataLoader
    {
        private static DataLoader _dataLoader;
        private static readonly object _syncLock = new object();


        public ObservableCollection<Spell> Spells { get; set; }
        public ObservableCollection<Race> Races { get; set; }
        public ObservableCollection<CharacterClass> CharacterClasses { get; set; }
        public ObservableCollection<GeneralFeat> Feats { get; set; }
        public ObservableCollection<Size> Sizes { get; set; } = new ObservableCollection<Size>();


        private DataLoader()
        {
            Serializer serializer = new Serializer();
            Spells = serializer.LoadCollection<Spell>("Spells");
            GetClassSpellDictionary(Spells);
            Feats = serializer.LoadCollection<GeneralFeat>("Feats");
            Races = serializer.LoadCollection<Race>("Races");
            CharacterClasses = serializer.LoadCollection<CharacterClass>("CharacterClasses");

            Sizes.Add(new Size(SizeType.Medium));
            Sizes.Add(new Size(SizeType.Small));
            Sizes.Add(new Size(SizeType.Large));

        }

        public static DataLoader GetDataLoader()
        {
            if (_dataLoader == null)
            {
                lock (_syncLock)
                {
                    if (_dataLoader == null)
                    {
                        _dataLoader = new DataLoader();
                    }
                }
                   
            }

            return _dataLoader;
        }

        public void GetClassSpellDictionary(ObservableCollection<Spell> spells)
        {
            List<string> ClassListName = new List<string>
            {
                "Bard","Cleric","Domain","Druid","Paladin","Ranger","Wizard","Alchemist","Inquisitor",
                "Magus","Summoner","Witch","Bloodrager","Shaman","Medium","Mesmerist","Occultist","Psychic",
                "Spiritualist"

            };


            foreach (Spell spell in Spells)
            {
                foreach (var name in ClassListName)
                {
                    var classListName = name;


                    int? nullableSpellLevel = (int?)spell.GetType().GetProperty($"{name}")?.GetValue(spell, null);

                    if (nullableSpellLevel != null)
                    {
                        int spellLevel = (int)nullableSpellLevel;
                        spell.ClassSpellLevelDictionary.Add(classListName, spellLevel);
                    }

                }

            }

        }

      

        

    }
}
