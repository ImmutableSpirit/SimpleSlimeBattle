using System;
using System.Collections.Generic;

namespace SimpleSlimeBattle
{
    public class Hero : GameObject
    {
        public enum actionType { Sword, Fireball, Wait, Run };
        public enum targetType { Self, Single, Multi };  
        public Dictionary<actionType, targetType> actionList;
        public actionType currentAction = actionType.Wait;
        public int strength { get; set; }
        public int magicPwr { get; set; }

        public Hero()
        {
            hitPoints = 100;
            strength = 2;
            magicPwr = 5;
            buildActionList();
        }

        private void buildActionList()
        {
            actionList = new Dictionary<actionType, targetType>();
            actionList.Add(actionType.Sword, targetType.Single);
            actionList.Add(actionType.Fireball, targetType.Single);
            actionList.Add(actionType.Wait, targetType.Self);
            actionList.Add(actionType.Run, targetType.Self);
        }

        public string GetActionsText()
        {
            var arr = Enum.GetNames(typeof(actionType));
            var text = string.Join(", ", arr);
            return text;
        }

        public actionType actionParse(string actionText){
            
            actionType actionVal;
            if (Enum.TryParse(actionText, true, out actionVal))
            {
                return actionVal;
            }
            else
            {
                throw new ArgumentException("Could not parse actionText as a valid actionType", "actionText");
            }
        }

        public bool TargetRequired(actionType action)
        {
            bool targetReq = actionList[action] == targetType.Single 
                                || actionList[action] == targetType.Multi;
            return targetReq;
        }

        public override void Do(string actionText, GameObject gameObject = null)
        {
            Console.WriteLine("Hero chooses " + actionText);
            var action = actionParse(actionText);

            if (action == actionType.Sword && gameObject != null)
            {
                var attack = new Attack { damage = strength, attackType = Attack.attackTypes.Normal };
                gameObject.Process(attack);
            }
            else if (action == actionType.Fireball && gameObject != null)
            {
                var attack = new Attack { damage = magicPwr, attackType = Attack.attackTypes.Fire };
                gameObject.Process(attack);
            }
            else if (action == actionType.Run)
            {
                Console.WriteLine("There is no escape!  Except for CTRL-C...");
            }
            else if (action == actionType.Wait)
            {
                Console.WriteLine("You're a very patient fellow.");
            }
        }

        public override void Process(Attack attack)
        {
            hitPoints -= attack.damage;
            Console.WriteLine("Hero takes {0} damage.  Hitpoints remaining: {1}", attack.damage, hitPoints);
            if (hitPoints <= 0)
            {
                Console.WriteLine("Our hero has perished!");
                status = statusType.Dead;
            }
            else
            {
                status = statusType.Normal;
            }
        }
    }
}