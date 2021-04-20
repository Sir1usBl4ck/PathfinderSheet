using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Xps;
using UserInterface.EventModels;

namespace UserInterface.Models
{

    public enum SaveType
    {
        Fortitude,
        Reflexes,
        Willpower,
        NoType
    }
    [Serializable]
    public class Save : ObservableObject, IHandle<AbilityChangedEvent>, IHandle<LevelChangedEvent>
    {
        public Save()
        {
            BonusList = new List<Bonus>();
            SaveType = SaveType.NoType;
            IsGood = false;
        }
        public Save(SaveType saveType)
        {
            SaveType = saveType;
            BaseValue = 0;
            IsGood = false;



        }
        public Save(SaveType saveType, AbilityType abilityType, string name, EventAggregator eventAggregator)
        {
            SaveType = saveType;
            BaseValue = 0;
            AbilityType = abilityType;
            Name = name;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            IsGood = false;
        }


        private EventAggregator _eventAggregator;

        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set => _eventAggregator = value;
        }

        private int _score;
        private int _level;
        public string Name { get; set; }
        public SaveType SaveType { get; set; }
        public int Score
        {
            get
            {
                SetBonus();
                return _bonus;
            }
            set
            {
                _bonus = value;

                OnPropertyChanged();
            }
        }
        public int BaseValue { get; set; }
        public AbilityType AbilityType { get; set; }
        private int _abilityModifier;

        public int AbilityModifier
        {
            get { return _abilityModifier; }
            set { _abilityModifier = value; }
        }

        public bool IsGood { get; set; }
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
                OnPropertyChanged("Bonus");
            }
        }

        private int _bonus;
        public int Bonus
        {
            get { return _bonus; }
            set
            {
                _bonus = value;
            }
        }
        private List<Bonus> _bonusList;
        public List<Bonus> BonusList
        {
            get => _bonusList;
            set
            {
                _bonusList = value;
                OnPropertyChanged();
                OnPropertyChanged("Score");
            }
        }


        public void SetBonus()
        {
            if (IsGood)
            {
                var dLevel = (double)Level;
                var dBaseValue = 2 + (dLevel * 0.5);
                BaseValue = (int)Math.Floor(dBaseValue);
                _bonus = BaseValue + AbilityModifier + _bonusList.Sum(item => item.Value);
                OnPropertyChanged(nameof(Bonus));
            }
            else
            {
                var dLevel = (double)Level;
                var dBaseValue = (dLevel * 0.33);
                BaseValue = (int)Math.Floor(dBaseValue);
                _bonus = BaseValue + AbilityModifier + _bonusList.Sum(item => item.Value);
                OnPropertyChanged(nameof(Bonus));
            }

        }
        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.Type == AbilityType)
            {
                AbilityModifier = message.Ability.Modifier;
            }
            SetBonus();
            OnPropertyChanged(nameof(Bonus));
        }
        public void Handle(LevelChangedEvent message)
        {
            Level = message.Level;
            SetBonus();
        }

        
    }


}

