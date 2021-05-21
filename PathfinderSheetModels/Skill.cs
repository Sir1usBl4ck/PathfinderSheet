using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;


namespace PathfinderSheetModels
{
    public class Skill : BaseAttribute, IBonusable, IRollable, IHandle<AbilityChangedEvent>
    {
        private int _baseScore;
        private bool _isClass;

        public Skill(Ability ability, string name, AttributeType attributeType, bool isTrainedOnly = false,
            bool hasArmorCheckPenalty = false)
        {
            Ability = ability;
            BaseScore = 0;
            Name = name;
            AttributeType = attributeType;
            IsTrainedOnly = isTrainedOnly;
            IsClass = false;
            HasArmorCheckPenalty = hasArmorCheckPenalty;
            
        }

        public EventAggregator EventAggregator { get; set; }
        public Ability Ability { get; set; }
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
        public bool IsTrainedOnly { get; set; }
        public bool HasArmorCheckPenalty { get; set; }

        public bool IsClass
        {
            get => _isClass;
            set { _isClass = value; OnPropertyChanged();}
        }

        public string Name { get; set; }

        public override int BaseScore
        {
            get => _baseScore;
            set { 
                _baseScore = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(Score));

            }
        }

        public override int Score =>
            BaseScore + BonusList.Sum(a => a.Value) + Ability.Modifier + (IsClass && BaseScore >= 1 ? 3 : 0);

        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.AttributeType == Ability.AttributeType)
            {
                Ability = message.Ability;
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(BaseScore));
            }
        }
    }
    
}