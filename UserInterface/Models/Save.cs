using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace UserInterface.Models
{

    public enum SaveType
    {
        Fortitude,
        Reflexes,
        Willpower
    }
    [Serializable]
    public class Save
    {
        public SaveType Type { get; set; }
        private int _base;
        private int _bonus;


        public int Bonus
        {
            get
            {
                return GetValue();
            }

        }

        public double Base
        {
            get => _base;
            set => _base =  (int) value;
        }

        public Ability Ability { get; }

        private int GetValue()
        {
            int intBase = (int) Base;
            int result =  intBase + Ability.Modifier;
            return result;
        }


        public Save(Ability ability, SaveType type)
        {
            Ability = ability;
            Type = type;
        }
    }
}
