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
            Console.WriteLine(dicho.ToString());

            Joueur nathan = new Joueur("nathan");

            Console.WriteLine(nathan.ToString());

            Console.WriteLine(dicho.RechDichoRecursif("bat"));

            /*
            string path = "../../../Files/joueurs.txt";
            if (File.Exists(path))
            {
                string[] joueurs = File.ReadAllLines(path);

                if (joueurs != null && joueurs.Length != 0)
                {
                    foreach (string joueur in joueurs)
                    {
                        string[] joueurParam = joueur.Split(';');
                        string nom = joueurParam[0];
                        int score = Convert.ToInt32(joueurParam[1]);
                        string[] mots = joueurParam[2].Split(',');
                        Joueur  = new Joueur(nom, score, mots);
                    }
                }
            }

            */

        }
    }
}
