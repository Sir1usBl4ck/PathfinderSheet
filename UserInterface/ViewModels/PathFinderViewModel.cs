using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UserInterface.Models;

namespace UserInterface.ViewModels
{
   [Serializable]
   public class PathFinderViewModel : BaseViewModel
   {
        public Character Character { get; set; }
        

        public ICommand CommandChangePlayerName { get; set; }


        public PathFinderViewModel()
        {
            CommandChangePlayerName = new RelayCommand(ChangePlayerName);

            
            Character = new Character
            {
                
            };

           
        }

        private void ChangePlayerName()
        {
            Character.Name = "dfgjikjhdfhj";
        }

       
   }   
}
