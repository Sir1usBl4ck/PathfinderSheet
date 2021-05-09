using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models.Modifiers
{
    public class Buff :ObservableObject
    {
        private string _name;
        private Bonus _newBonus;

        public Buff()
        {
            BonusList = new ObservableCollection<Bonus>();
            NewBonus = new Bonus();
            NewBonus.BonusSource = ($"Buff: {Name}");
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Bonus> BonusList { get; set; }

        public Bonus NewBonus
        {
            get => _newBonus;
            set
            {
                _newBonus = value;
                OnPropertyChanged();
            }
        }
    }
}
