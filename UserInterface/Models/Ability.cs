using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserInterface.EventModels;
using UserInterface.Models.Modifiers;
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
    public class Ability : ObservableObject, IBonusable, IHandle<BonusListChangedEvent>

    {
        private string _name;
        private int _baseScore;
        private EventAggregator _eventAggregator;
        private int _bonus;
        private ObservableCollection<Bonus> _bonusList;
        public Ability()
        {
            BaseScore = 10;
            BonusList = new ObservableCollection<Bonus>();


        }
        public AbilityType Type { get; set; }
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
                OnPropertyChanged("Score");
                OnPropertyChanged("Modifier");
                PublishAbilityChange();

            }
        }
        public int Score => _baseScore + _bonusList.Sum(a => a.Value);
        public int Modifier => (Score - Score % 2) / 2 - 5;
        public int PointCost { get; set; }
        public int Bonus
        {
            get => _bonusList.Sum(item => item.Value);
            set
            {

                _bonus = value;
                OnPropertyChanged();
                OnPropertyChanged("Score");
                OnPropertyChanged("Modifier");
            }
        }
        public ObservableCollection<Bonus> BonusList
        {
            get => _bonusList;
            set
            {
                _bonusList = value;
                OnPropertyChanged();
                OnPropertyChanged("Score");
            }
        }
        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set => _eventAggregator = value;
        }
        public ObservableCollection<int> BaseValues { get; } = new ObservableCollection<int>();
        public string Icon { get; set; }
        public void CalculateBonus()
        {
            Bonus = _bonusList.Sum(item => item.Value);
        }
        private void PublishAbilityChange()
        {
            _eventAggregator?.Publish(new AbilityChangedEvent(this)); 

        }
        public void Handle(BonusListChangedEvent message)
        {
           
        }
    }
}