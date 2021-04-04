using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using UserInterface.Models;
using UserInterface.Views;
using Type = UserInterface.Models.Type;

namespace UserInterface.ViewModels
{
    [Serializable]
    public class PathFinderViewModel : BaseViewModel
    {
        public Character Character { get; set; }
        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        private Race _selectedRace;
        

       

        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
                Character.Race = _selectedRace;
                UpdateAbilities();

            }
        }
        
        public ICommand CommandChangeRace { get; set; }


        public PathFinderViewModel()
        {
            //Races
            var elf = new Race("Elf", Type.Humanoid, SubType.Elf);
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Dexterity,2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Intelligence,2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution,-2));

            var dwarf = new Race("Dwarf", Type.Humanoid, SubType.Dwarf);
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution,2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Wisdom,2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Charisma,2));

            

            Races.Add(elf);
            Races.Add(dwarf);


            //Classes
            


            Character = new Character
            {

            };

        }


        private void UpdateAbilities()
        {
            foreach (var abilityToUpdate in _selectedRace.ModifiedAbilities)
            {
                
                var ability = Character.Abilities.FirstOrDefault(a => a.Type == abilityToUpdate.AbilityType);
                var bonus = abilityToUpdate.Bonus;
                ability.RaceBonus = 0;
                ability.RaceBonus += bonus;
            }
            
        }

       





    }
}
