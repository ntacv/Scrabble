using System;
using System.IO;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scrabble!");

            Dictionnaire dicho = new Dictionnaire("Francais");

            Sac_Jetons Sac = new Sac_Jetons();
            Console.WriteLine(Sac.ToString());
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                Jeton newOne = Sac.Retire_Jeton(r);
                //Console.WriteLine(newOne.ToString());
            }
            Console.WriteLine(Sac.ToString());

            Joueur nathan = new Joueur("nathan");

            Console.WriteLine(nathan.ToString());

            Console.WriteLine(dicho.RechDichoRecursif("bat"));

            Plateau Plateau = new Plateau();
            


        }
    }
}
