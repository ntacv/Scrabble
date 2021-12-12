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
        SortedList<int, string[]> motsTrie;

        public SortedList<int, string[]> MotsTrie
        {
            get { return motsTrie; }
        }

        public Dictionnaire(string langue)
        {
            if (langue != null && langue.Length != 0)
            {
                string path = "../../../Files/" + langue + ".txt";
                if (File.Exists(path))
                {
                    this.langue = langue;
                    this.motsTrie = new SortedList<int, string[]> { };
                    string[] allDicho = File.ReadAllLines(path);

                    for(int i = 0; i < allDicho.Length/2; i++)
                    {
                        motsTrie.Add(Convert.ToInt32(allDicho[2*i]), allDicho[2*i + 1].Split(' '));
                    }
                }
            }
        }


        public override string ToString()
        {
            string txt = "Langue : " + langue+"\n";
            if(motsTrie!=null && motsTrie.Count != 0)
            {
                for (int i = 0; i < motsTrie.Count; i++)
                {
                    txt += motsTrie.Keys[i]+" : "+motsTrie.Values[i].Length+", \n";
                }
            }
            return txt;
        }

        public bool RechDichoRecursif(string mot, int debut, int fin)
        {
            bool exist = false;
            if (mot != null && mot.Length != 0)
            {
                mot = mot.ToUpper();
                int key = mot.Length;
                int index = motsTrie.IndexOfKey(key);

                int milieu = (debut + fin) / 2;
                //Console.Write(motsTrie[key][milieu] + " ; "+milieu+"   ");

                if (fin < debut)
                {
                    return exist;
                }
                if (motsTrie[key][milieu] == mot)
                {
                    return true;
                }
                else
                {

                    if (mot.CompareTo(motsTrie[key][milieu]) == -1)
                    {
                        return RechDichoRecursif(mot, debut, milieu - 1);
                    }
                    else
                    {
                        return RechDichoRecursif(mot, milieu + 1, fin);
                    }

                    /*
                    bool motSupMid = true;
                    bool motInfMid = true;

                    for (int i = 0; i < key; i++)
                    { 
                        if(mot[i] > motsTrie[key][milieu][i])
                        {
                            motSupMid = false;
                        }
                        if(mot[i] < motsTrie[key][milieu][i])
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
                    */
                }
            }
            return exist;
        }

        public bool RechFor(string mot)
        {
            bool exist = false;
            if (motsTrie != null && motsTrie.Count != 0 && mot != null && mot.Length >= 2 && mot.Length <= 5)
            {
                mot = mot.ToUpper();
                int key = mot.Length;
                int index = motsTrie.IndexOfKey(key);

                for (int i = 0; i < motsTrie.Values[index].Length; i++)
                {
                    if (mot == motsTrie.Values[index][i])
                    {
                        exist = true;
                        break;
                    }
                }
            }
            return exist;
        }


    }
}
