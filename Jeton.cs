using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Jeton
    {
        char lettre; 
        int score;
        int nombre;


        //Constructeur qui affecte au jeton sa lettre le score de celle-ci et le nombre qu'il en existe dans le sac
        public Jeton(char lettre, int score, int nombre)
        {
            this.lettre = lettre;
            this.score = score;
            this.nombre = nombre;
        }
        //Constructeur identique mais qui prend en paramettre que la lettre seule
        public Jeton(char lettre)
        {
            this.lettre = lettre;

            

        }
        
        #region Paramètres

        //
        public char Lettre
        {
            get { return lettre; }
        }
        public int Score
        {
            get { return score; }
        }
        public int Nombre
        {
            get { return nombre; }
        }
        #endregion

        //Affichage du jeton
        public override string ToString()
        {
            string txt = lettre + ", " + score + ", " + nombre;
            return txt;
        }

        
        public string ToStringSave()
        {
            string txt = lettre + ";" + score + ";" + nombre;
            return txt;
        }

        //Méthode qui retire un jeton du sac
        public void Retire()
        {
            if (nombre > 0)
            {
                nombre--;
            }
        }

        
    }
}
