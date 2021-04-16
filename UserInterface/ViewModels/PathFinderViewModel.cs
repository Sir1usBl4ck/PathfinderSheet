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
    public class PathFinderViewModel : BaseViewModel, IHandle<RaceChangedEvent>, IHandle<CharacterClassChangedEvent>
    {
        //EG:  1- Create private field for EventAggregator 

        private EventAggregator _eventAggregator;

        public Character Character { get; set; }
        public ObservableCollection<int> PossibleTotalPoints { get; } = new ObservableCollection<int>();
        public ObservableCollection<Race> Races { get; } = new ObservableCollection<Race>();
        public ObservableCollection<CharacterClass> CharacterClasses { get; } = new ObservableCollection<CharacterClass>();

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
            var barbarian = new CharacterClass("Barbarian", 1, 12);
            barbarian.GoodSave = new Save(SaveType.Fortitude);
            barbarian.ClassSkillNames.Add("Acrobatics");
            barbarian.ClassSkillNames.Add("Climb");
            barbarian.ClassSkillNames.Add("Craft");
            barbarian.ClassSkillNames.Add("Handle Animal");
            barbarian.ClassSkillNames.Add("Intimidate");
            barbarian.ClassSkillNames.Add("Knowledge (nature)");
            barbarian.ClassSkillNames.Add("Perception");
            barbarian.ClassSkillNames.Add("Ride");
            barbarian.ClassSkillNames.Add("Survival");
            barbarian.ClassSkillNames.Add("Swim");

            var wizard = new CharacterClass("Wizard", 0.5, 6);
            wizard.GoodSave = new Save(SaveType.Willpower);

            CharacterClasses.Add(barbarian);
            CharacterClasses.Add(wizard);

            Character = new Character(_eventAggregator);
            Character.CharacterClass = CharacterClasses[0];
            Character.Race = Races[0];
            Character.ExperienceProgression = Character.ExperienceProgressionList[1];


            ApplyRace();
            ApplyClass();

        }
        //----Methods

        private void ApplyRace()
        {
            Character.UpdateRaceAbilities();
        }

        private void ApplyClass()
        {
            Character.UpdateClassSkills(Character.CharacterClass);
            Character.UpdateCharacterClassSaves();
            Character.SetBab();
        }

        public void Handle(RaceChangedEvent message)
        {
            Character.UpdateRaceAbilities();
            Character.UpdateCharacterClassSaves();
        }
        public void Handle(CharacterClassChangedEvent message)
        {
            Character.UpdateCharacterClassSaves();
            Character.SetBab();
        }
    }
}

