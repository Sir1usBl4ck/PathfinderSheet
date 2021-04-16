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
        Willpower
    }
    [Serializable]
    public class Save : ObservableObject, IHandle<AbilityChangedEvent>, IHandle<LevelChangedEvent>
    {
        public Save(SaveType saveType)
        {
            SaveType = saveType;
            BaseValue = 0;
            IsGood = false;



        }
        public Save(SaveType saveType, Ability ability, string name, EventAggregator eventAggregator)
        {
            SaveType = saveType;
            BaseValue = 0;
            Ability = ability;
            Name = name;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            IsGood = false;
        }


        private EventAggregator _eventAggregator;
        private int _bonus;
        private int _level;
        public string Name { get; set; }
        public SaveType SaveType { get; set; }

        public int Bonus
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
        public Ability Ability { get; set; }

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

        public void SetBonus()
        {
            if (IsGood)
            {
                var dLevel = (double)Level;
                var dBaseValue = 2 + (dLevel * 0.5);
                BaseValue = (int)Math.Floor(dBaseValue);
                _bonus = BaseValue + Ability.Modifier;


            }
            else
            {
                var dLevel = (double)Level;
                var dBaseValue = (dLevel * 0.33);
                BaseValue = (int)Math.Floor(dBaseValue);
                _bonus = BaseValue + Ability.Modifier;

            }
        }

        public void Handle(AbilityChangedEvent message) //TODO the same as Skills
        {
            if (message.Ability.Type == Ability.Type)
            {
                Ability.Modifier = message.Ability.Modifier;
            }

            OnPropertyChanged(nameof(Bonus));
        }

        public void Handle(LevelChangedEvent message)
        {
            Level = message.Level;
            OnPropertyChanged(nameof(Level));
        }
    }


}

