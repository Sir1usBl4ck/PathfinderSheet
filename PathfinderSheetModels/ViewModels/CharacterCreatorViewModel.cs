using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UserInterface.Data;
using UserInterface.EventModels;
using UserInterface.Models;

namespace UserInterface.ViewModels
{
    public class CharacterCreatorViewModel : BaseViewModel, IHandle<RaceChangedEvent>
    {

        private EventAggregator _eventAggregator;

        public CharacterCreatorViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            NewCharacter = new Character(_eventAggregator);


            DataLoader dataLoader = DataLoader.GetDataLoader();
            Races = dataLoader.Races;
            CharacterClasses = dataLoader.CharacterClasses;
            Feats = dataLoader.Feats;


            CreateNewCharacterCommand = new RelayCommand(CreateNewCharacter);
            ChangeAbilityPointsLeftCommand = new RelayCommand<string>(ChangeAbilityPointsLeft);
            RollHpCommand = new RelayCommand(RollHp);

        }

        private void RollHp()
        {
            NewCharacter.MaxHitPoints =  NewCharacter.RollHitPoints(NewCharacter.Level);
        }

        public ObservableCollection<Race> Races { get; set; }
        public ObservableCollection<CharacterClass> CharacterClasses { get; set; }
        public ObservableCollection<GeneralFeat> Feats { get; set; }
        public Character NewCharacter { get; set; }

        public ObservableCollection<int> AvailableLevels { get; set; } = new ObservableCollection<int>
            {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};

        public ICommand ChangeAbilityPointsLeftCommand { get; set; }
        public ICommand CreateNewCharacterCommand { get; set; }
        public ICommand RollHpCommand { get; set; }

        private void ChangeAbilityPointsLeft(string points)
        {
            NewCharacter.PointsLeft = Int32.Parse(points);
        }

        private void CreateNewCharacter()
        {
            _eventAggregator.Publish(new ViewChangedEvent(new GameViewModel(_eventAggregator)));
            _eventAggregator.Publish(new CharacterChangedEvent(NewCharacter));
        }

        public void Handle(RaceChangedEvent message)
        {
            foreach (var ability in NewCharacter.Abilities)
            {
                ability.BonusList.Clear();
                foreach (var bonus in message.Race.ModifiedAbilities.Where(bonus => bonus.Type == ability.Type))
                {
                    
                    ability.BonusList.Add(bonus);
                }
                ability.CalculateBonus();
            }
        }
    }
}
