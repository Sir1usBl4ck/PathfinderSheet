using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{ 
    public class Save : BaseAttribute, IBonusable, IHandle<AbilityChangedEvent>, IHandle<LevelChangedEvent>
    {
        private int _level;
        private bool _isGood;

        public Save(AttributeType type, Ability ability)
        {
            Ability = ability;
            AttributeType = type;
        }
        public EventAggregator EventAggregator { get; set; }
        public override int BaseScore { get; set; }
        public override int Score => BaseScore + Ability.Modifier + ActiveBonusList.Sum(item => item.Value);
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> ActiveBonusList { get; set; } = new ObservableCollection<Bonus>();
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
        public void RecalculateScore()
        {
            OnPropertyChanged(nameof(Score));

        }

        public Ability Ability { get; set; }

        public int Level
        {
            get => _level;
            set
            {
                _level = value; 
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool IsGood
        {
            get => _isGood;
            set
            {
                _isGood = value; 
                CalculateBaseScore();
            }
        }


        public void  CalculateBaseScore()
        {
            if (IsGood)
            {
                var dLevel = (double)Level;
                var dBaseValue = 2 + (dLevel * 0.5);
                BaseScore = (int)Math.Floor(dBaseValue);
                
            }
            else
            {
                var dLevel = (double)Level;
                var dBaseValue = (dLevel * 0.33);
                BaseScore = (int)Math.Floor(dBaseValue);

            }

        }


        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.AttributeType == Ability.AttributeType)
            {
                Ability = message.Ability;
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(BaseScore));
            }
        }

        public void Handle(LevelChangedEvent message)
        {
            Level = message.Level;
            CalculateBaseScore();
        }
    }

}

