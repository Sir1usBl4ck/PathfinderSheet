using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using UserInterface.Models;

namespace UserInterface.Services
{
    public interface IRollable
    {
        public string Name { get;}
        public int BonusToRoll { get;}
    }

    public class RollService
    {
        public void RollExecute(ObservableCollection<DiceRoll> diceRolls, IRollable o )
        {
            IDice dice = new Dice();
            dice.Dice(20).Constant(o.BonusToRoll);
            DiceResult result = dice.Roll(new RandomDieRoller());
            DiceRoll roll = new DiceRoll
            {
                Expression = result.DiceExpression,
                Total = result.Value,
                Sender = o.Name,
                DiceResult = Convert.ToInt32(result.RollsDisplayText)
            };

            diceRolls.Insert(0, roll);


        }
    }
}
