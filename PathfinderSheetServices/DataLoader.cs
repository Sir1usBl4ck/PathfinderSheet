using System.Collections.ObjectModel;
using PathfinderSheetModels;

namespace PathfinderSheetServices
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

    }
}
