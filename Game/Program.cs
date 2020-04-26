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
                    bool validAction = false;
                    do
                    {
                        validAction = GetAction();
                    } while (validAction == false);                    
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
                    Console.WriteLine("Press any key + Enter to exit.");
                    Console.ReadLine();
                    break;
                }
                if (slimeMonster.status == GameObject.statusType.Dead)
                {
                    Console.WriteLine("You win! New adventures await!");
                    Console.WriteLine("Press any key + Enter to exit.");
                    Console.ReadLine();
                    break;
                }

                PrintSlimes(slimeMonster.splinterSlimes.Count + 1);

            } while (true);

            SkipLines(2);
            Console.WriteLine(lineThick);
            Console.WriteLine("~END");
        }

        public static bool GetAction(){
            Console.WriteLine("What do you do?");
            Console.WriteLine("Options are: " + hero.GetActionsText());
            var actionText = Console.ReadLine();

            try
            {
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
            catch (ArgumentException)
            {
                Console.WriteLine("I'm sorry, what did you say?");
                return false;
            }
            catch(System.Exception)
            {
                Console.WriteLine("Oh no.. something terrible happened.  The world is melting!");
                //TODO: Log exception to file
                Environment.Exit(0);
            }

            return true;
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
