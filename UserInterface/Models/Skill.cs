using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserInterface.EventModels;
using UserInterface.Models.Modifiers;
using UserInterface.Services;


namespace UserInterface.Models
{
    public class Skill : ObservableObject, IRollable, IHandle<AbilityChangedEvent>,
        IHandle<LevelChangedEvent>, IHandle<AvailableSkillRanksChanged>, IBonusable
    {
        private int _bonus;
        private ObservableCollection<Bonus> _bonusList;
        private int _rank;
        private bool _trainedOnly;
        private EventAggregator _eventAggregator;
        private AbilityType _abilityType;   // You might need to add a property when talents/traits change the reference ability
        private int _bonusModifier;
        private int _sizeModifier;
        private bool _isClass;
        
        public Skill()
        {
            BonusList = new ObservableCollection<Bonus>();

        }

        //public Ability Ability { get; set; }

        public string Name { get; set; }
        public int BonusToRoll => Bonus;

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
                    //UpdateValue();
                    PublishSkillChange();
                }
                
            }
        }

        public int Bonus
        {
            get => _rank + BonusModifier + _sizeModifier + BonusList.Sum(item => item.Value) + ClassBonus;
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

        public ObservableCollection<Bonus> BonusList
        {
            get { return _bonusList; }
            set { _bonusList = value; }
        }

        public int BonusModifier
        {
            get => _bonusModifier;
            set
            {
                _bonusModifier = value;
                OnPropertyChanged();
            }
        }

        public int ClassBonus => IsClass && _rank >= 1 ? 3 : 0;



        //public void UpdateValue()
        //{
        //    Bonus = _rank + BonusModifier + _sizeModifier + BonusList.Sum(item => item.Value)+ClassBonus;
        //    OnPropertyChanged("ClassBonus");
            
        //}


        public void Handle(AbilityChangedEvent message)
        {
            if (message.Ability.Type == _abilityType)
            {
                BonusModifier = message.Ability.Modifier;
                OnPropertyChanged(nameof(Bonus));
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