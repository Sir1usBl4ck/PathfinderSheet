using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserInterface.EventModels;


namespace UserInterface.Models
{
    public class Skill : ObservableObject, IHandle<AbilityChangedEvent>
    {
        private int _bonus;
        private int _rank;
        private bool _trainedOnly;
        private EventAggregator _eventAggregator;
        
        private AbilityType _abilityType;
        private int _bonusModifier;
        


        //public Ability Ability { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AbilityType AbilityType
        {
            get => _abilityType;
            set => _abilityType = value;
        }

        public int Rank
        {
            get => _rank;
            set
            {
                _rank = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bonus));
                UpdateValue();
            }
        }

        public int Bonus
        {
            get => _bonus;
            set
            {
                _bonus = value;
                OnPropertyChanged();
            }
        }

        public bool IsClass { get; set; }

        public bool TrainedOnly
        {
            get => _trainedOnly;
            set { _trainedOnly = value; OnPropertyChanged(); }
        }

        public bool ArmorCheckPenalty { get; set; }

        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set
            {
                _eventAggregator = value;
                
            }
        }


        public Skill(string name, bool trainedOnly, AbilityType abilityType, bool armorCheckPenalty, EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _abilityType = abilityType;
            Name = name;
            IsClass = false;
            ArmorCheckPenalty = armorCheckPenalty;
            TrainedOnly = trainedOnly;
        }

        public Skill(string name, bool trainedOnly, AbilityType abilityType, EventAggregator eventAggregator)
        : this(name, trainedOnly, abilityType, false, eventAggregator)
        {
        }
        [JsonConstructor]
        public Skill(string name,AbilityType abilityType, int rank, int bonus, bool isClass, bool trainedOnly, bool armorCheckPenalty)
        {
            Name = name;
            AbilityType = _abilityType;
            Rank = rank;
            Bonus = bonus;
            IsClass = isClass;
            TrainedOnly = trainedOnly;
            ArmorCheckPenalty = isClass;
        }

        public void UpdateValue()
        {
            Bonus = _rank + _bonusModifier;
            if (IsClass)
                Bonus += 3;


        }
        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.Type == _abilityType)
            {
                _bonusModifier = message.Ability.Modifier;
                UpdateValue();
            }


        }
    }
}