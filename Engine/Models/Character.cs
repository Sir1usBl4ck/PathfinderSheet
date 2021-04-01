using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Annotations;
using SQLite;

namespace Engine.Models
{
    [Serializable]
    public class Character : ObservableObject
    {
        private string _name =  "Bob";


        //--This is all the Player's Stats--
        
        public int Id { get; set; }
  
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }



        public CharacterClass PClass { get; set; }
        public int Experience { get; set; } //Implement a level up event when reaching the specified amount of XP

        public int Level { get; set; }

        //Ability List


        public Ability Strength { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Constitution { get; set; }
        public Ability Intelligence { get; set; }
        public Ability Wisdom { get; set; }
        public Ability Charisma { get; set; }





        //HitPoints

        public int HitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }

        //SkillsList


        public Skill Acrobatics { get; set; }

       
       



        public Character()
        {
           


        }


        
    }
}
