using System;
using System.Collections.ObjectModel;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;
using PathfinderSheetModels;

namespace PathfinderSheetServices
{
    public class RollService
    {
        public void RollExecute(ObservableCollection<DiceRoll> diceRolls, IRollable o )
        {
            IDice dice = new Dice();
            dice.Dice(20).Constant(o.Score);
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
