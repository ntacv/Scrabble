using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Joueur
    {
        //Attributs
        string nom;
        int score;
        string[] mots;

        //Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            score = 0;
            mots = null;
        }
        public Joueur()
        {

        }


    }
}
