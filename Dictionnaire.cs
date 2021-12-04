using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scrabble
{
    class Dictionnaire
    {

        string langue;
        SortedList<int, string[]> dicho;

        public SortedList<int, string[]> Dicho
        {
            get { return dicho; }
        }

        public Dictionnaire(string langue)
        {
            if (langue != null && langue.Length != 0)
            {
                string path = "../../../Files/" + langue + ".txt";
                if (File.Exists(path))
                {
                    this.langue = langue;
                    this.dicho = new SortedList<int, string[]> { };
                    string[] allDicho = File.ReadAllLines(path);

                    for(int i = 0; i < allDicho.Length/2; i++)
                    {
                        dicho.Add(Convert.ToInt32(allDicho[2*i]), allDicho[2*i + 1].Split(' '));
                    }
                }
            }
        }


        public override string ToString()
        {
            string txt = "Langue : " + langue+"\n";
            if(dicho!=null && dicho.Count != 0)
            {
                for (int i = 0; i < dicho.Count; i++)
                {
                    txt += dicho.Keys[i]+" : "+dicho.Values[i].Length+", \n";
                }
            }
            return txt;
        }

        public bool RechDichoRecursif(string mot, int debut, int fin)
        {
            bool exist = false;
            mot = mot.ToUpper();
            int key = mot.Length;
            int index = dicho.IndexOfKey(key);

            int milieu = (debut + fin) / 2;
            Console.Write(dicho[key][milieu] + " ; "+milieu+"   ");

            if(fin < debut)
            {
                return exist;
            }
            if(dicho[key][milieu] == mot)
            {
                return true;
            }
            else
            {
                bool motSupMid = true;
                bool motInfMid = true;
                for (int i = 0; i < key; i++)
                { 
                    if(mot[i] > dicho[key][milieu][i])
                    {
                        motSupMid = false;
                    }
                    if(mot[i] < dicho[key][milieu][i])
                    {
                        motInfMid = false;
                    }
                }
                if (!motSupMid && motInfMid)
                {
                    //mot inférieur au mot milieu
                    return RechDichoRecursif(mot, debut, milieu - 1);
                }
                else return RechDichoRecursif(mot, milieu + 1, fin);
                
            }

            /*
            if(dicho!=null && dicho.Count != 0 && mot!=null && mot.Length>=2 && mot.Length<=5)
            {
                mot = mot.ToUpper();
                int key = mot.Length;
                int index = dicho.IndexOfKey(key);
                
                for(int i=0;i< dicho.Values[index].Length ; i++)
                {
                    if(mot == dicho.Values[index][i])
                    {
                        exist = true;
                        break;
                    }
                }
            }*/


            return exist;
        }

    }
}
