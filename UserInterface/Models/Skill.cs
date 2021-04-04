using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace UserInterface.Models
{
    [Serializable]
    public class Skill : ObservableObject
    {

        public Ability Ability { get; }

        public string Name { get; set; }

        private int _rank;
        
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

        public bool TrainedOnly { get; set; }

        public bool ArmorCheckPenalty { get; set; }

        public int GetSkillValue()
        {
            int result = Rank + Ability.Modifier;
            if (IsClass == true)
                result += 3;
            

            return result;

        }

        public Skill(string name, bool trainedOnly, Ability ability)
        {
            Ability = ability;
            Rank = 0;
            Name = name;
            IsClass = false;
            TrainedOnly = false;
            ArmorCheckPenalty = false;
            Ability.PropertyChanged += Ability_PropertyChanged;
        }

        public Skill(string name, bool trainedOnly, Ability ability,bool armorCheckPenalty)
        {
            Ability = ability;
            Rank = 0;
            Name = name;
            IsClass = false;
            TrainedOnly = false;
            ArmorCheckPenalty = armorCheckPenalty;
            Ability.PropertyChanged += Ability_PropertyChanged;
        }



        private void Ability_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Ability.Modifier)))
            {
                OnPropertyChanged(nameof(Bonus));
            }
        }

    }
}