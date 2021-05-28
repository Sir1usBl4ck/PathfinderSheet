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
    public class CharacterCreatorViewModel : BaseViewModel, IHandle<CharacterClassChangedEvent>, IHandle<RaceChangedEvent>, IHandle<AbilityChangedEvent>
    {

        private EventAggregator _eventAggregator;

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

            PointBuy = 20;
            CalculatePointsLeft();
            CreateNewCharacterCommand = new RelayCommand(CreateNewCharacter);
            SetPointsBuyCommand = new RelayCommand<int>(SetPointBuy);

        }

        private void SetPointBuy(int obj)
        {
            PointBuy = obj;
            PointsLeft = CalculatePointsLeft();
           
        }

        public ObservableCollection<Race> Races { get; set; }
        public ObservableCollection<CharacterClass> CharacterClasses { get; set; }
        public ObservableCollection<Spell> Spells { get; set; }
        public ObservableCollection<GeneralFeat> Feats { get; set; }
        public Character NewCharacter { get; set; }
        public List<int> AvailableLevels { get; set; } = new List<int>
            {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};

        

        public int CalculatePointsLeft()
        {
            var pointsLeft = PointBuy;
            foreach (var ability in NewCharacter.Abilities)
            {
                var scoreCost = new Dictionary<int, int>()
                {
                    {7, -4}, {8, -2}, {9, -1}, {10, 0}, {11, 1}, {12, 2}, {13, 3}, {14, 5}, {15, 7}, {16, 10}, {17, 13},
                    {18, 17}
                };
                var pointCost = scoreCost[ability.BaseScore];
                pointsLeft -= pointCost;
                
            }
            return pointsLeft;
        }

        private int _pointsLeft;

        public int PointsLeft
        {
            get { return _pointsLeft; }
            set
            {
                _pointsLeft = value; 
                OnPropertyChanged();
            }
        }


        private int _pointBuy;

        public int PointBuy
        {
            get { return _pointBuy; }
            set
            {
                _pointBuy = value;
                OnPropertyChanged();
            }
        }


        public ICommand CreateNewCharacterCommand { get; set; }
        public ICommand SetPointsBuyCommand { get; set; }
       
        

        
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
                ability.ActiveBonusList.Remove(ability.ActiveBonusList.SingleOrDefault(a => a.BonusSource == "Race"));

                foreach (var bonus in message.Race.ModifiedAbilities.Where(a => a.Target == ability.AttributeType)
                    .ToList())
                {
                    ability.ActiveBonusList.Add(bonus);

                }
            }

        }

        public void Handle(AbilityChangedEvent message)
        {
            PointsLeft = CalculatePointsLeft();
        }
    }
}
