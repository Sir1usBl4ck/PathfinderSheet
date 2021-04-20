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
    public class Skill : ObservableObject, IHandle<AbilityChangedEvent>, IHandle<LevelChangedEvent>, IHandle<AvailableSkillRanksChanged>
    {
        private int _bonus;
        private List<Bonus> _bonusList;
        private int _rank;
        private bool _trainedOnly;
        private EventAggregator _eventAggregator;
        private AbilityType _abilityType;   // You might need to add a property when talents/traits change the reference ability
        private int _bonusModifier;
        private int _sizeModifier;
        private bool _isClass;


        public Skill()
        {
            Name = "noName";
            BonusList = new List<Bonus>();

        }

        //public Ability Ability { get; set; }

        public string Name { get; set; }
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

                if (value<=Level && value<=AvailableSkillRank)
                {
                    _rank = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Bonus));
                    UpdateValue();
                    PublishSkillChange();
                }
                
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

        public bool IsClass
        {
            get => _isClass;
            set
            {
                _isClass = value;
                OnPropertyChanged();
            }
        }

        public bool TrainedOnly
        {
            get => _trainedOnly;
            set { _trainedOnly = value; OnPropertyChanged(); }
        }

        public bool ArmorCheckPenalty { get; set; }
        public int Level { get; set; }
        public int AvailableSkillRank { get; set; }

        public EventAggregator EventAggregator
        {
            get => _eventAggregator;
            set
            {
                _eventAggregator = value;
                
            }
        }

        public int SizeModifier
        {
            get => _sizeModifier;
            set
            {
                _sizeModifier = value;
                OnPropertyChanged();
            }
        }

        public List<Bonus> BonusList
        {
            get { return _bonusList; }
            set { _bonusList = value; }
        }

        
        public void UpdateValue()
        {
            Bonus = _rank + _bonusModifier + _sizeModifier + BonusList.Sum(item => item.Value);
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

        private void PublishSkillChange()
        {
            _eventAggregator?.Publish(new SkillChangedEvent(this));  // is there a better way to handle the Null _eventAggregator?

        }

        public void Handle(LevelChangedEvent message)
        {
            Level = message.Level;
        }

        public void Handle(AvailableSkillRanksChanged message)
        {
            AvailableSkillRank = message.AvailableSkillRanks;
        }
    }
}