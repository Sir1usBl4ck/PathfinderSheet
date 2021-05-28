using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{
    public class Ability : BaseAttribute,  IBonusable, IRollable, IHandle<RaceChangedEvent>

    {
        private int _baseScore;

        public Ability(AttributeType type, string name, string abbreviated)
        {
            AttributeType = type;
            Name = name;
            BaseScore = 10;
            Acronym = abbreviated;
        }

        public EventAggregator EventAggregator { get; set; } = new EventAggregator();
        public AttributeType AttributeType { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }

        public override int BaseScore
        {
            get => _baseScore;
            set
            {
                _baseScore = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(Modifier));
                EventAggregator.Publish(new AbilityChangedEvent(this));
            }
        }

        public override int Score => BaseScore + ActiveBonusList.Sum(a => a.Value);
        public int Modifier => (Score - Score % 2) / 2 - 5;
        public ObservableCollection<Bonus> ActiveBonusList { get; set; } = new ObservableCollection<Bonus>();

        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
        public void RecalculateScore()
        {
            OnPropertyChanged(nameof(Score));
        }

        public void Handle(RaceChangedEvent message)
        {
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(Modifier));
            
        }
    }
}