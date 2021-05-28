using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderSheetModels;
using PathWalkerViewModels.EventModels;

namespace PathWalkerViewModels.ChildViewModels
{
    public class SpellDetailViewModel : ChildViewModel, IHandle<SelectedSpellChangedEvent>
    {
        public SpellDetailViewModel(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe(this);
        }

        private Spell _selectedSpell;

        public Spell SelectedSpell
        {
            get { return _selectedSpell; }
            set
            {
                _selectedSpell = value;
                OnPropertyChanged();
            }
        }

        public void Handle(SelectedSpellChangedEvent message)
        {
            SelectedSpell=message.Spell;
        }
    }
}
