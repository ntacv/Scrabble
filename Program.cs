using System;
using System.IO;
using System.Threading;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 30);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Scrabble!\n\n");
            Thread.Sleep(1000);
            Console.ResetColor();
            



            Jeu JeuTest = new Jeu();
            //JeuTest.PlaceWord();
            
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
        public static bool VerifieChar(char lettre)
        {
            bool possible = false;
            if(lettre>=65 && lettre <= 90)
            {
                possible = true;
            }
            return possible;
        }//isletter


        public static string AskLanguage()
        {
            string[] menu = new string[] { "Francais", "English", "Español" };

            int index = Menu(menu);
            
            return menu[index];
        }
        public static int AskSaves()
        {
            int init = 0;

            string[] menu = new string[2] { "Commencer", "Reprendre" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : Commencer 
            // 1 : Reprendre

            return init;
        }
        public static int AskJoueur()
        {
            int init = 0;

            
            string[] menu = new string[2] { "Ajouter un joueur", "Commencer la partie" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            //0 : Ajouter
            //1 : Commencer
            return init;
        }
        public static int AskConfirm()
        {
            int init = 0;

            
            string[] menu = new string[2] { "Confirme", "Changer" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : Confirme 
            // 1 : changer

            return init;
        }
        public static int AskDirection()
        {
            int init = 0;

            string[] menu = new string[2] { "Horizontale", "Verticale" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : horizontale 
            // 1 : verticale
            return init;
        }

        public static int Menu(string[] menu)
        {
            ConsoleKey inputKey;
            int index = 0;
            do
            {
                Console.Clear();
                /*
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop - menu.Length+1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor - menu.Length+1);
                */
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (i == menu.Length - 1) Console.Write(menu[i]);
                    else Console.WriteLine(menu[i]);
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

        public static void ClearConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void ClearConsoleLine2()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor-1);
        }
    }
}
