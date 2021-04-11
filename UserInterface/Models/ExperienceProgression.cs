using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.EventModels;

namespace UserInterface.Models
{
    public enum Progression
    {
        Slow,
        Medium,
        Fast
    }
    public class ExperienceProgression : ObservableObject
    {
        private EventAggregator _eventAggregator;
        private long[] _experienceTable;
        public string Name { get; set; }
        public Progression Progression { get; set; }

        public long[] ExperienceTable
        {
            get => _experienceTable;
            set
            {
                _experienceTable = value;
                OnPropertyChanged();
                
            }
        }


        public ExperienceProgression(Progression progression)
        {
            _eventAggregator = new EventAggregator();
            Progression = progression;
            GetTab();

        }

        public void GetTab()
        {
            switch (Progression)
            {
                case Progression.Fast:
                    Name = "Fast";
                    ExperienceTable = new long[]
                    {
                        0,0,1300,3300,6000,10000,15000,23000,34000,50000,
                        71000,105000,145000,210000,295000,425000,600000,
                        850000,1200000,1700000,2400000,
                    };
                    break;
                case Progression.Medium:
                    Name = "Medium";
                    ExperienceTable = new long[]
                    {
                        0,0,2000,5000,9000,15000,23000,35000,51000,75000,
                        105000,155000,220000,315000,445000,635000,890000,
                        1300000,1800000,2550000,3600000
                    };
                    break;
                case Progression.Slow:
                    Name = "Slow";
                    ExperienceTable = new long[]
                    {
                        0,3000,7500,14000,23000,35000,53000,77000,115000,
                        160000,235000,330000,475000,665000,955000,1350000,
                        1900000,2700000,3850000,5350000
                    };
                    break;
            }
        }

        
    }


}
