using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;
using PathfinderSheetDataAccess;
using PathWalkerViewModels.EventModels;


namespace PathWalkerViewModels
{
    public class CharacterCreatorViewModel : BaseViewModel, IHandle<CharacterClassChangedEvent>, IHandle<RaceChangedEvent>
    {

        private EventAggregator _eventAggregator;
        private int _abilityPointsLeft = 20;

        public CharacterCreatorViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            NewCharacter = new Character(_eventAggregator);
            CharacterInitializer.InitializeCharacterAbilities(NewCharacter, eventAggregator);

            var dataLoader = DataLoader.GetDataLoader();
            Races = dataLoader.Races;
            CharacterClasses = dataLoader.CharacterClasses;
            Feats = dataLoader.Feats;
            Spells = dataLoader.Spells;

            CreateNewCharacterCommand = new RelayCommand(CreateNewCharacter);
            ChangeAbilityPointsLeftCommand = new RelayCommand<string>(ChangeAbilityPointsLeft);
        }

        public ObservableCollection<Race> Races { get; set; }
        public ObservableCollection<CharacterClass> CharacterClasses { get; set; }
        public ObservableCollection<Spell> Spells { get; set; }
        public ObservableCollection<GeneralFeat> Feats { get; set; }
        public Character NewCharacter { get; set; }
        public List<int> AvailableLevels { get; set; } = new List<int>
            {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};

        public int AbilityPointsLeft
        {
            get => _abilityPointsLeft;
            set
            {
                _abilityPointsLeft = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeAbilityPointsLeftCommand { get; set; }
        public ICommand CreateNewCharacterCommand { get; set; }
        

        private void ChangeAbilityPointsLeft(string points)
        {
            AbilityPointsLeft = Int32.Parse(points);
        }

        private void CreateNewCharacter()
        {
            _eventAggregator.Publish(new ViewChangedEvent(new CharacterViewModel(NewCharacter, _eventAggregator)));
        }



        public void Handle(CharacterClassChangedEvent message)
        {
            CharacterService.ApplyClass(NewCharacter);
        }

        public void Handle(RaceChangedEvent message)
        {
            foreach (var ability in NewCharacter.Abilities)
            {
                ability.BonusList.Remove(ability.BonusList.SingleOrDefault(a => a.BonusSource == "Race"));

                foreach (var bonus in message.Race.ModifiedAbilities.Where(a => a.Target == ability.AttributeType)
                    .ToList())
                {
                    ability.BonusList.Add(bonus);

                }
            }

        }
    }
}
