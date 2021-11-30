using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Sac_Jetons
    {
        Jeton[] sac;
        int total;

        public int Total
        {
            get { return total; }
        }

        public Sac_Jetons()
        {
            string path = "../../../Files/Jetons.txt";
            if (File.Exists(path))
            {
                string[] allJetons = File.ReadAllLines(path);
                this.sac = new Jeton[27];

                for (int i = 0; i < allJetons.Length; i++)
                {
                    string[] line = allJetons[i].Split(';');
                    sac[i] = new Jeton( Convert.ToChar(line[0]), Convert.ToInt32(line[1]), Convert.ToInt32(line[2]));
                        
                }
                for (int i = 0; i < sac.Length; i++)
                {
                    this.total += sac[i].Nombre;
                }
            }
        }

        public override string ToString()
        {
            string txt = "Sac de Jetons : \nLettre, Score, Nombre restant : \n";
            for(int i = 0; i < sac.Length; i++)
            {
                txt += sac[i].ToString()+"\n";
            }
            return txt;
        }

        public Jeton Retire_Jeton(Random r)
        {
            Jeton jeton=null;
            
            int rdm = r.Next(0, this.total);
            int nbr = 0;
            for (int i = 0; i < sac.Length; i++)
            {
                nbr += sac[i].Nombre;
                if (rdm <= nbr)//manque la limite apres une valeur a 0
                {
                    jeton = sac[i];
                    jeton.Retire();
                    break;
                }
            }
            total--;
            return jeton;
        }



    }
}
