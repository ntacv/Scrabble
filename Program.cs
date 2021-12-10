using System;
using System.IO;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Scrabble!");
            Console.ResetColor();
            //Console.WindowHeight = 480;
            Console.SetWindowSize(60,30);

            
            
            Jeu JeuTest = new Jeu();
            JeuTest.PlaceWord();
            
            /*

            Sac_Jetons Sac = new Sac_Jetons();
            Console.WriteLine(Sac.ToString());
            Random r = new Random();
            do
            {
                Jeton newOne = Sac.Retire_Jeton(r);
                Console.WriteLine(newOne.ToString());
            } while (Sac.Total != 0);
            //Console.WriteLine(Sac.ToString() + "\n total : " + Sac.Total);

            Plateau Plateau = new Plateau(' ');
            Plateau.ToStringColor();
            */

        }


        public static string VerifieString(string phrase)
        {
            string txt=null;

            do
            {
                Console.Write(phrase);
                txt = Convert.ToString(Console.ReadLine());
            } while (txt == null || txt.Length==0);
            
            return txt;
        }

        public static string AskLanguage()
        {
            string[] menu = new string[] { "Francais", "English", "Español" };

            int index = Menu(menu);
            
            return menu[index];
        }

        public static int AskSaves()
        {
            int init = 0;

            //ask init or save
            string[] menu = new string[2] { "Commencer", "Reprendre" };

            int index = Menu(menu);
            if (index == 1) init = 1;

            return init;
        }
        public static int AskJoueur()
        {
            int init = 0;

            //ask init or save
            string[] menu = new string[2] { "Ajouter un joueur", "Commencer la partie" };

            int index = Menu(menu);
            if (index == 1) init = 1;

            return init;
        }


        public static int Menu(string[] menu)
        {
            ConsoleKey inputKey;
            int index = 0;
            do
            {
                Console.Clear();

                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                inputKey = Console.ReadKey().Key;
                if (inputKey == ConsoleKey.DownArrow && index < menu.Length - 1)//40 : Down arrow key
                {
                    index++;
                }
                if (inputKey == ConsoleKey.UpArrow && index > 0)//38 : Up arrow key
                {
                    index--;
                }

            } while (inputKey != ConsoleKey.Enter);
            return index;
        }
        
    }
}
