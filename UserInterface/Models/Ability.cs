using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UserInterface.ViewModels;

namespace UserInterface.Models
{
    public enum AbilityType
    {
        Strenght,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }
   [Serializable]
    public class Ability : ObservableObject
    {
        
        private string _name;
        private int _score;
        private int _modifier;
        private int _raceBonus;
        private int _baseScore;
        private ObservableCollection<int> _baseValues;

        public Ability(string name, AbilityType type)
        {
            Name = name;
            Type = type;
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

        public int RaceBonus
        {
            get => _raceBonus;
            set
            {
                _raceBonus = value;
                
                OnPropertyChanged();
                GetScore();
                
                
               
            }
        }

        public int BaseScore
        {
            get => _baseScore;
            set
            {
                _baseScore = value;
                OnPropertyChanged();
                GetScore();



            }
        }

        public int Score
        {
            get => _score;

            set
            {
                _score = value;
                OnPropertyChanged();
                Modifier = (_score-_score%2)/2-5;
                OnPropertyChanged("Modifier");
                
                
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


        private void GetScore()
        {
            Score = _baseScore + _raceBonus;
            
        }

    }
}