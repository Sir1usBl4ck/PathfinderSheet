using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D20Tek.DiceNotation;
using D20Tek.DiceNotation.DieRoller;


namespace UserInterface.Models
{

    public class DiceRoll
    {
        public DiceRoll()
        {
           TimeStamp = DateTime.Now.ToLocalTime().ToShortTimeString();
           
        }
       
        /// <summary>
        /// TODO: add a way to store modifier to the roll 
        /// </summary>
        public string Sender { get; set; }
        public int Total { get; set; }
        public int DiceResult { get; set; }
        public string Expression { get; set; }
        public string TimeStamp { get; set; }
        public string Special { get; set; }

        
        public void RollAttack(Attack attack)
        {
            IDice dice = new Dice();
            dice.Dice(20).Constant(attack.AttackBonus);
            DiceResult diceResult = dice.Roll(new MathNetDieRoller());
            int rollResult = Convert.ToInt32(diceResult.RollsDisplayText);
            
            

            Expression = diceResult.DiceExpression;
            Total = diceResult.Value;
            Sender = $" AttackRoll: {attack.Name} ";
            DiceResult = rollResult;
        }

        public void RollDamage(Attack attack)
        
        {
            IDice dice = new Dice();
            dice.Dice(attack.Dice, attack.DiceNumber).Constant(attack.DamageBonus);
            DiceResult diceResult = dice.Roll(new MathNetDieRoller());
            int rollResult = Convert.ToInt32(diceResult.RollsDisplayText);

            Expression = diceResult.DiceExpression;
            Total = diceResult.Value;
            Sender = " Damage: ";
            DiceResult = rollResult;
        }






        
    }
}
