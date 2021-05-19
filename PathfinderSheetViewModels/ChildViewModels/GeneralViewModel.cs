using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PathfinderSheetModels;
using PathfinderSheetServices;

namespace PathfinderSheetViewModels.ChildViewModels
{
    public class GeneralViewModel : ChildViewModel
    {
        private long[] _xpTable;
        private ChildViewModel _currentFeatView;

        public GeneralViewModel()
        {
            XpTable = ExperienceService.Table(Progression.Medium);
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
            CurrentClassView = new CharacterClassViewModel(Character, EventAggregator);
            AddFeatCommand = new RelayCommand(ChangeViewToAddFeats);
            BackCommand = new RelayCommand(ChangeViewToFeats);
        }

        private void ChangeViewToFeats()
        {
            CurrentFeatView = new FeatsViewModel(Character, EventAggregator);
        }

        private void ChangeViewToAddFeats()
        {
            CurrentFeatView = new AddFeatsViewModel(Character, EventAggregator);
        }

        public long[] XpTable
        {
            get => _xpTable;
            set
            {
                _xpTable = value;
                OnPropertyChanged(nameof(OldXpToLevel));
                OnPropertyChanged(nameof(XpToLevel));
            }
        }

        public ChildViewModel CurrentFeatView
        {
            get => _currentFeatView;
            set { _currentFeatView = value; OnPropertyChanged(); }
        }

        private ChildViewModel _currentClassView;

        public ChildViewModel CurrentClassView
        {
            get => _currentClassView;
            set { _currentClassView = value; OnPropertyChanged(); }
        }


        public long OldXpToLevel => Character.Level != 1 ? 0 : XpTable[Character.Level];
        public long XpToLevel => XpTable[Character.Level + 1];
        public long CurrentXp { get; set; }

        public ICommand BackCommand { get; set; }
        public ICommand AddFeatCommand { get; set; }
    }
}
