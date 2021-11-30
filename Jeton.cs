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


        public Jeton(char lettre, int score, int nombre)
        {
            this.lettre = lettre;
            this.score = score;
            this.nombre = nombre;
        }
        
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

        public override string ToString()
        {
            string txt = lettre + ", " + score + ", " + nombre;
            return txt;
        }

        public void Retire()
        {
            if (nombre > 0)
            {
                nombre--;
            }
        }

        
    }
}
