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
        private Dictionary<int, int> pointBuyCost;

        public Character Character { get; set; }
        public ObservableCollection<int> Points { get; } = new ObservableCollection<int>();

        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        public ObservableCollection<CClass> Classes { get; } = new ObservableCollection<CClass>();

        private Race _selectedRace;
        private CClass _selectedClass;
        private int _selectedPointBuy;


        public int SelectedPointBuy
        {
            get => _selectedPointBuy;
            set
            {
                _selectedPointBuy = value;
                OnPropertyChanged();
            }
        }

        public CClass SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                Character.CClass = _selectedClass;
                ApplyClass();
            }
        }

        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
                Character.Race = _selectedRace;
                ResetAbilities();
                UpdateAbilities();

            }
        }



        public PathFinderViewModel()
        {
            
            pointBuyCost = new Dictionary<int, int>()
            {
                {7,-4},{8,-2},{9,-1},{10,0},{11,1},{12,2},{13,3},{14,5},{15,7},{16,10},{17,13},{18,17}
            };

            Points.Add(15);
            Points.Add(20);
            Points.Add(25);
            SelectedPointBuy = 20;

            //Races
            var elf = new Race("Elf", Type.Humanoid, SubType.Elf);
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Dexterity, 2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Intelligence, 2));
            elf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution, -2));

            var dwarf = new Race("Dwarf", Type.Humanoid, SubType.Dwarf);
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Constitution, 2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Wisdom, 2));
            dwarf.ModifiedAbilities.Add(new AbilityModifier(AbilityType.Charisma, -2));



            Races.Add(elf);
            Races.Add(dwarf);


            //Classes

            var barbarian = new CClass("Barbarian", 1);

            Classes.Add(barbarian);




            //Character
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
                ability.RaceBonus = bonus;
            }

        }

        private void ResetAbilities()
        {
            foreach (var characterAbility in Character.Abilities)
            {
                Ability? ability = null;
                foreach (var a in Character.Abilities)
                {
                    if (a.RaceBonus != 0)
                    {
                        ability = a;
                        ability.RaceBonus = 0;
                    }
                }


            }
        }


        private void ApplyClass()
        {
            Character.BabProgress = SelectedClass.BaBProgression;
            Character.GetBab();
        }

        private void CalculatePointBuy()
        {
            
            foreach (var ability in Character.Abilities)
            {
                ability.PointCost = pointBuyCost[ability.BaseScore];
                SelectedPointBuy -= ability.PointCost;

            }


        }

        
    }
}

