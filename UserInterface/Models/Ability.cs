using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public enum AbilityType
    {
        Strength,
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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get => _score;

            set
            {
                _score = value;
                OnPropertyChanged();
                Modifier = ((_score - 10) % 2) + ((_score - 10) / 2);
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

        public Ability(string name)
        {
            Name = name;
            Score = 10;
        }


    }
}