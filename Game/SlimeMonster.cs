using System;
using System.Collections.Generic;

namespace SimpleSlimeBattle
{
    public class SlimeMonster : GameObject
    {
        public Stack<SlimeMonster> splinterSlimes = new Stack<SlimeMonster>();
        public int strength { get; set; }

        public SlimeMonster()
        {
            hitPoints = 20;
            strength = 2;
        }

        public override void Do(string actionText, GameObject gameObject = null)
        {
            Console.WriteLine("Slime does a... {0} ATTACK!", actionText.ToUpper());
            if (gameObject != null)
            {
                var attack = new Attack { displayName = actionText, attackType = Attack.attackTypes.Normal, damage = strength };
                gameObject.Process(attack);
            }
        }

        public override void Process(Attack attack)
        {
            if (attack.attackType == Attack.attackTypes.Normal)
            {
                splinterSlimes.Push(new SlimeMonster());
                Console.WriteLine("It seemed to have no effect!  Wait..");
                Console.WriteLine("The slime has splinterred!  Now there are {0} slimes!", splinterSlimes.Count + 1);
            }
            else if(attack.attackType == Attack.attackTypes.Fire)
            {
                hitPoints -= attack.damage;
                Console.WriteLine("Slime takes {0} damage!", attack.damage);

                if (hitPoints <= 0)
                {
                    Console.WriteLine("Slime is defeated!");
                    if (splinterSlimes.Count > 0)
                    {
                        splinterSlimes.Clear();
                        Console.WriteLine("All of the splinter slimes have melted away.");
                    }
                    status = statusType.Dead;
                }
            }
        }
    }
}