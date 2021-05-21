﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PathfinderSheetModels
{ 
    public class Save : BaseAttribute, IBonusable, IHandle<AbilityChangedEvent>
    {
        private int _level;

        public Save(AttributeType type, Ability ability)
        {
            Ability = ability;
            AttributeType = type;
        }
        public EventAggregator EventAggregator { get; set; }
        public override int BaseScore { get; set; }
        public override int Score => CalculateScore();
        public AttributeType AttributeType { get; set; }
        public ObservableCollection<Bonus> BonusList { get; set; } = new ObservableCollection<Bonus>();
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

        public bool IsGood { get; set; }


        public int  CalculateScore()
        {
            if (IsGood)
            {
                var dLevel = (double)Level;
                var dBaseValue = 2 + (dLevel * 0.5);
                BaseScore = (int)Math.Floor(dBaseValue);
                return BaseScore + Ability.Modifier + BonusList.Sum(item => item.Value);
                
            }
            else
            {
                var dLevel = (double)Level;
                var dBaseValue = (dLevel * 0.33);
                BaseScore = (int)Math.Floor(dBaseValue);
                return BaseScore + Ability.Modifier + BonusList.Sum(item => item.Value);
               
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
    }

}

