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
            Dictionnaire dicho = new Dictionnaire("Francais");
            Console.WriteLine(dicho.ToString());

            Sac_Jetons Sac = new Sac_Jetons();
            Console.WriteLine(Sac.ToString());
            Random r = new Random();
            for (int i = 0; i < 102; i++)
            {
                Jeton newOne = Sac.Retire_Jeton(r);
                //Console.WriteLine(newOne.ToString());
            }
            /*
            do
            {
                Jeton newOne = Sac.Retire_Jeton(r);
                Console.WriteLine(newOne.ToString());
            } while (Sac.Total != 0);
            */

            Console.WriteLine(Sac.ToString() + "\n total : " + Sac.Total); ;

            Joueur nathan = new Joueur();
            Console.WriteLine(nathan.ToString());

            string mot = "ex";
            int key = mot.Length;
            int index = dicho.Dicho.IndexOfKey(key);
            //Console.WriteLine(dicho.RechDichoRecursif(mot, 0, dicho.Dicho[key].Length-1));

            Plateau Plateau = new Plateau(' ');
            Console.WriteLine(Plateau);
            Plateau.ToStringColor();


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


    }
}
