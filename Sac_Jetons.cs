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
        public Jeton[] Sac
        {
            get { return sac; }
        }

        public Sac_Jetons(string content)
        {
            if (content!=null && content.Length!=0)
            {
                string[] allJetons = content.Split("\r\n");
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
            if (total != 1)
            {
                int rdm = r.Next(total + 1);
                int nbr = 0;
                for (int i = 0; i < sac.Length; i++)
                {
                    nbr += sac[i].Nombre;
                    if (rdm <= nbr)
                    {
                        jeton = sac[i];
                        jeton.Retire();
                        break;
                    }
                }
                CalculTotal();
            }
            else
            {
                for (int i = 0; i < sac.Length; i++)
                {
                    if (sac[i].Nombre != 0)
                    {
                        jeton = sac[i];
                        jeton.Retire();
                        break;
                    }
                }
            }
            return jeton;
        }

        public void CalculTotal()
        {
            int total=0;
            for (int i = 0; i < sac.Length; i++)
            {
                total += sac[i].Nombre;
            }
            this.total = total;
        }
        public Jeton InfoJeton(char lettre)
        {
            Jeton jeton = new Jeton('$',0,0) ;
            for(int i = 0; i < sac.Length; i++)
            {
                if(sac[i].Lettre == lettre)
                {
                    jeton = sac[i];
                }
            }
            return jeton;
        }

    }
}
