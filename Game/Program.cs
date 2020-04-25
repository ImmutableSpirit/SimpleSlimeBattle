using System;

namespace SimpleSlimeBattle
{
    class Program
    {
        public static Hero hero { get; set; }
        public static SlimeMonster slimeMonster { get; set; }
        const string lineThick = "=============================================";
        const string lineThin =  "-  - ----------------------------------- -  -";

        static void Main(string[] args)
        {
            hero = new Hero();
            slimeMonster = new SlimeMonster();
            SkipLines(2);
            Console.WriteLine(lineThick);
            SkipLines(1);
            Console.WriteLine("Welcome to a simple SlimeMonster battle.");
            Console.WriteLine("A slime appears!");
            SkipLines(2);
            PrintSlimes();
            SkipLines(2);
            
            do
            {
                //Hero's turn
                if (hero.status == GameObject.statusType.Normal)
                {
                    GetAction();
                }
                //Slime's turn
                if (slimeMonster.status == GameObject.statusType.Normal)
                {
                    slimeMonster.Do("Slam", hero);
                }
                
                SkipLines(2);

                if (hero.status == GameObject.statusType.Dead)
                {
                    Console.WriteLine("You lost.  But never give up!  Life finds a way...");
                    break;
                }
                if (slimeMonster.status == GameObject.statusType.Dead)
                {
                    Console.WriteLine("You win! New adventures await!");
                    break;
                }

                PrintSlimes(slimeMonster.splinterSlimes.Count + 1);

            } while (true);

            SkipLines(2);
            Console.WriteLine(lineThick);
            Console.WriteLine("~END");
        }

        public static void GetAction(){
            Console.WriteLine("What do you do?");
            Console.WriteLine("Options are: " + hero.GetActionsText());
            var actionText = Console.ReadLine();
            var heroAction = hero.actionParse(actionText);
            bool targetRequired = hero.TargetRequired(heroAction);

            Console.WriteLine(lineThin);
            if (targetRequired)
            {
                hero.Do(actionText, slimeMonster);
            }
            else
            {
                hero.Do(actionText);
            }
            Console.WriteLine(lineThin);
        }

        public static void SkipLines(int x)
        {
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine(string.Empty);
            }
        }

        public static void PrintSlimes(int x = 1){

            string line1 = "    ;;;;;;;    ".Repeat(x);
            string line2 = "  ;;; .;; .;;  ".Repeat(x);
            string line3 = "  ;;;;;;;;;;;  ".Repeat(x);
            string line4 = "  '''''''''''  ".Repeat(x);

            Console.WriteLine(line1);
            Console.WriteLine(line2);
            Console.WriteLine(line3);
            Console.WriteLine(line4);
        }
    }
}
