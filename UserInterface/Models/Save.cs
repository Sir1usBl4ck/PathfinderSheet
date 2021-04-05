using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Xps;

namespace UserInterface.Models
{

    public enum SaveType
    {
        Fortitude,
        Reflexes,
        Willpower
    }
    [Serializable]
    public class Save : ObservableObject
    {

        public Save(SaveType saveType, Ability ability)
        {
            SaveType = saveType;
            BaseValue = 0;
            Ability = ability;
            IsGood = false;
        }


        private int _bonus;

        public SaveType SaveType { get; set; }

        public int Bonus
        {
            get => _bonus;
            set
            {
                _bonus = value;
                SetBonus();
                OnPropertyChanged();
            }
        }

        public int  BaseValue { get; set; }
        public Ability Ability { get; set; }

        public bool IsGood { get; set; }
        public int Level { get; set; }


        private void SetBonus()
        {
            if (IsGood == true)
            {
                var dLevel = (double) Level;
                var dBaseValue = 2 + (dLevel * 0.5);
                BaseValue = (int) Math.Floor(dBaseValue);
                Bonus = BaseValue + Ability.Modifier;

            }
            else
            {
                var dLevel = (double)Level;
                var dBaseValue = 2 + (dLevel * 0.33);
                BaseValue = (int)Math.Floor(dBaseValue);
                Console.WriteLine("double" + dBaseValue + " " + "int" + BaseValue);
            }
        }

        
    }
}
