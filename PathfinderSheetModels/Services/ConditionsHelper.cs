using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.Models;

namespace UserInterface.Services
{
    public static class ConditionsHelper
    {
        

        public static ObservableCollection<Condition> ConditionList { get; set; }

        static ConditionsHelper()
        {
            

        }
        
    }
}
