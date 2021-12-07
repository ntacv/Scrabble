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


            Jeu JeuTest = new Jeu();
            JeuTest.PlaceWord();
            /*

            bool init = AskSaves();
            int indexLang = AskLanguage();



            Sac_Jetons Sac = new Sac_Jetons();
            Console.WriteLine(Sac.ToString());
            Random r = new Random();
            do
            {
                Jeton newOne = Sac.Retire_Jeton(r);
                Console.WriteLine(newOne.ToString());
            } while (Sac.Total != 0);
            //Console.WriteLine(Sac.ToString() + "\n total : " + Sac.Total);

            Dictionnaire Dicho = new Dictionnaire("Francais");
            string mot = "ex";
            int key = mot.Length;
            int index = Dicho.MotsTrie.IndexOfKey(key);
            //Console.WriteLine(dicho.RechDichoRecursif(mot, 0, Dicho.MotsTrie[key].Length-1));

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

        public static int AskLanguage()
        {
            string[] menu = new string[] { "Francais", "English", "Español" };

            int index = Menu(menu);
            
            return index;
        }

        public static bool AskSaves()
        {
            bool init = false;

            //ask init or save
            string[] menu = new string[2] { "Commencer", "Reprendre" };

            int index = Menu(menu);
            if (index == 0) init = true;

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
