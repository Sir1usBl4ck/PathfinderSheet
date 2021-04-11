using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UserInterface.EventModels;
using UserInterface.Models;
using UserInterface.Views;
using Type = UserInterface.Models.Type;

namespace UserInterface.ViewModels
{
    [Serializable]
    public class PathFinderViewModel : BaseViewModel, IHandle<RaceChangedEvent>, IHandle<AbilityChangedEvent>, IHandle<CClassChangedEvent>
    {
        private CClass _selectedClass;
        private int _selectedPointBuy;
        //EG:  1- Create private field for EventAggregator 

        private EventAggregator _eventAggregator;

        public Character Character { get; set; }
        public ObservableCollection<int> PossibleTotalPoints { get; } = new ObservableCollection<int>();

        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        public ObservableCollection<CClass> CClasses { get; } = new ObservableCollection<CClass>();


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

        //----Constructor
        public PathFinderViewModel()
        {
            //EG:  2- Instantiate the EventAggregator
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);



            PossibleTotalPoints.Add(15);
            PossibleTotalPoints.Add(20);
            PossibleTotalPoints.Add(25);
            

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

            barbarian.GoodSave = new Save(SaveType.Fortitude);
            CClasses.Add(barbarian);
            UpdateCClassSaves();
            




            //Character
            Character = new Character(_eventAggregator);
            Character.CClass = CClasses[0];
            Character.Race = Races[0];
            Character.ExperienceProgression = Character.ExperienceProgressionList[1];
            UpdateRaceAbilities();
            ApplyClass();

        }
        //----Methods
        private void UpdateRaceAbilities()
        {

            if (Character != null)
            {
                foreach (var ability in Character.Abilities)
                {
                    var raceBonus = Character.Race.ModifiedAbilities.FirstOrDefault(a => a.Type == ability.Type);

                    if (raceBonus != null)
                        ability.Score = ability.BaseScore + raceBonus.Bonus;
                    else
                        ability.Score = ability.BaseScore;
                }
            }
            
        }
        private void UpdateCClassSaves()
        {

            if (Character != null)
            {
                foreach (var save in Character.Saves)
                {
                    if (Character.CClass.GoodSave.SaveType == save.SaveType)
                    {
                        save.IsGood = true;
                    }
                }
            }
        }

        private void ApplyClass()
        {
            Character.BabProgress = Character.CClass.BaBProgression;
            UpdateCClassSaves();
            Character.SetBab();
        }




        public void Handle(RaceChangedEvent message)
        {
            UpdateRaceAbilities();
            UpdateCClassSaves();
        }

        public void Handle(AbilityChangedEvent message)
        {
            UpdateRaceAbilities();
            UpdateCClassSaves();
        }

        public void Handle(CClassChangedEvent message)
        {
            UpdateCClassSaves();
        }
    }
}

