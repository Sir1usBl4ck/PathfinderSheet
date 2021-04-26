using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserInterface.EventModels;
using UserInterface.Services;

namespace UserInterface.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AbilityType
    {
        [EnumMember(Value = "Strength")]
        Strength,
        [EnumMember(Value = "Dexterity")]
        Dexterity,
        [EnumMember(Value = "Constitution")]
        Constitution,
        [EnumMember(Value = "Intelligence")]
        Intelligence,
        [EnumMember(Value = "Wisdom")]
        Wisdom,
        [EnumMember(Value = "Charisma")]
        Charisma
    }
    [Serializable]
    public class Ability : ObservableObject
    {
        private string _name;
        private int _score;
        private int _modifier;
        private int _baseScore;
        private ObservableCollection<int> _baseValues;
        private EventAggregator _eventAggregator;
        private int _bonus;
        private List<Bonus> _bonusList;

        public Ability()
        {
            BaseScore = 10;
            BaseValues.Add(7);
            BaseValues.Add(8);
            BaseValues.Add(9);
            BaseValues.Add(10);
            BaseValues.Add(11);
            BaseValues.Add(12);
            BaseValues.Add(13);
            BaseValues.Add(14);
            BaseValues.Add(15);
            BaseValues.Add(16);
            BaseValues.Add(17);
            BaseValues.Add(18);
        }

        public AbilityType Type { get; set; }
        public ObservableCollection<int> BaseValues { get; } = new ObservableCollection<int>();
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int BaseScore
        {
            get => _baseScore;
            set
            {
                _baseScore = value;
                OnPropertyChanged();
                PublishAbilityChange();

            }
        }
        public int Score
        {
            get => _score;

            set
            {
                _score = value;
                OnPropertyChanged();
                Modifier = (_score - _score % 2) / 2 - 5;

            }
        }
        public int Modifier
        {
            get => _modifier;
            set
            {
                _modifier = value;
                OnPropertyChanged();
            }
        }
        public int PointCost { get; set; }
        public int Bonus
        {
            get { return _bonus; }
            set
            {
                _bonus = value;
            }
        }
        public List<Bonus> BonusList
        {
            get => _bonusList;
            set
            {
                _bonusList = value;
                Bonus = BonusList.Sum(item => item.Value);
                OnPropertyChanged();
                OnPropertyChanged("Score");
            }
        }
        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set => _eventAggregator = value;
        }

        private void PublishAbilityChange()
        {
            _eventAggregator?.Publish(new AbilityChangedEvent(this));  // is there a better way to handle the Null _eventAggregator?

        }




    }
}