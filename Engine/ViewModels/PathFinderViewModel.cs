using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Engine.Annotations;
using Engine.Models;

namespace Engine.ViewModels
{
   [Serializable]
   public class PathFinderViewModel : BaseViewModel
   {
        public Character Character { get; set; }
        

        public ICommand CommandChangePlayerName { get; set; }


        public PathFinderViewModel()
        {
            CommandChangePlayerName = new RelayCommand(ChangePlayerName);


            //SkillList = new List<Skill>();
            Character = new Character
            {
                Strength = new Ability("Strength", AbilityType.Strength),
                Dexterity = new Ability("Dexterity", AbilityType.Dexterity),
                Constitution = new Ability("Constitution", AbilityType.Constitution),
                Intelligence = new Ability("Intelligence", AbilityType.Intelligence),
                Wisdom = new Ability("Wisdom", AbilityType.Wisdom),
                Charisma = new Ability("Charisma", AbilityType.Charisma),
                //Name = "NewCharacter",
            };

            Character.Acrobatics = new Skill("Acrobatics", false, Character.Dexterity);

        }

        private void ChangePlayerName()
        {
            Character.Name = "dfgjikjhdfhj";
        }

        public void Save(PathFinderViewModel character)
        {
           //Utility.SaveCharacter(character);
        }

        //public void Load(Character character)
        //{
        //    Utility.LoadCharacter(character);
        //}
   }   
}
