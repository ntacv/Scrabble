using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Joueur
    {
        //Attributs
        string nom;
        int score;
        List<string> mots;

        //Constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            score = 0;
            mots = null;
        }
        public Joueur(string nom, int score, List<string> mots)
        {
            this.nom = nom;
            this.score = score;
            this.mots = mots;
        }
        /// <summary>
        /// définie les joueurs a partir d'un tableau de joueurs;
        /// </summary>
        /// <param name="joueurs">tableau de lignes pour chaque joueurs avec ';' entre les attributs</param>
        /*public Joueur(string[] joueurs)
        {
            if (joueurs != null && joueurs.Length != 0)
            {
                foreach (string joueur in joueurs)
                {
                    string[] joueurParam = joueur.Split(';');
                    string nom = joueurParam[0];
                    int score = Convert.ToInt32(joueurParam[1]);
                    string[] mots = joueurParam[2].Split(',');
                    Joueur moi = new Joueur(nom, score, mots);
                }
            }
        }*/

        public override string ToString()
        {
            string txt = nom + " ; " + score + " ; ";
            if(mots!=null && mots.Count != 0)
            {
                for(int i = 0; i < mots.Count; i++)
                {
                    txt += mots[i];
                }
            }

            return txt;
        }


        public void Add_Mot(string mot)
        {
            mots.Add(mot);
        }
        public void Add_Score(int val)
        {
            if (val >= 0 && val < 100)
            {
                score += val;
            }
        }
        /*
        public void Add_Main_Courante(Jeton monjeton)
        {

        }
        public void Remove_Main_Courante(Jeton monjeton)
        {

        }
        */
    }
}
