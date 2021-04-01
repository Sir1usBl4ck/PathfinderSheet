using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
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
        public string Name { get; set; }


        private int _score;

        public AbilityType AbilityType;

        public int Score
        {
            get => _score;

            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(Modifier));
            }
        }

        public int Modifier
        {
            get
            {
                return ((_score - 10) % 2) + ((_score - 10) / 2);
            }
        }

        public Ability()
        {
            Score = 12;
        }

        public Ability(string name, AbilityType abilityType)
            : this()
        {
            Name = name;

            Score = 12;
        }


        
    }
}