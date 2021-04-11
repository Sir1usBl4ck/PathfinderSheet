using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserInterface.EventModels;


namespace UserInterface.Models
{
    [Serializable]
    public class Skill : ObservableObject, IHandle<AbilityChangedEvent>
    {
        private int _rank;
        private bool _trainedOnly;
        private EventAggregator _eventAggregator;

        public Skill(string name, bool isClass, EventAggregator eventAggregator)
        {
            Name = name;
            IsClass = isClass;
            _eventAggregator = eventAggregator;
        }
        public Skill(string name, bool trainedOnly, Ability ability, EventAggregator eventAggregator)
        {
            Ability = ability;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            Rank = 0;
            Name = name;
            IsClass = false;
            TrainedOnly = trainedOnly;
            ArmorCheckPenalty = false;
            //Ability.PropertyChanged += Ability_PropertyChanged;
        }
        public Skill(string name, bool trainedOnly, Ability ability, bool armorCheckPenalty, EventAggregator eventAggregator)
        {
            Ability = ability;
            Rank = 0;
            Name = name;
            IsClass = false;
            ArmorCheckPenalty = armorCheckPenalty;
            _eventAggregator = eventAggregator;
            TrainedOnly = trainedOnly;
            //Ability.PropertyChanged += Ability_PropertyChanged;
        }

        public Ability Ability { get; set; }
        public string Name { get; set; }
        public int Rank
        {
            get => _rank;
            set
            {
                _rank = value;
                OnPropertyChanged(nameof(Rank));
                OnPropertyChanged(nameof(Bonus));



            }
        }
        public int Bonus
        {
            get
            {
                return GetSkillValue();


            }


        }
        public bool IsClass { get; set; }
        public bool TrainedOnly
        {
            get => _trainedOnly;
            set { _trainedOnly = value; OnPropertyChanged(); }
        }
        public bool ArmorCheckPenalty { get; set; }
        public int GetSkillValue()
        {
            int result = Rank + Ability.Modifier;
            if (IsClass == true)
                result += 3;


            return result;

        }
        //private void Ability_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName.Equals(nameof(Ability.Modifier)))
        //    {
        //        OnPropertyChanged(nameof(Bonus));
        //    }
        //}


        public void Handle(AbilityChangedEvent message)
        {
            Ability = message.Ability;
            OnPropertyChanged(nameof(Bonus));
        }
    }
}