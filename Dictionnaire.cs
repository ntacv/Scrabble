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
            string txt = "Langue = " + langue+"\n";
            if(dicho!=null && dicho.Count != 0)
            {
                for (int i = 0; i < dicho.Count; i++)
                {
                    txt += dicho.Keys[i]+" : "+dicho.Values[i].Length+", \n";
                }
            }
            return txt;
        }

        public bool RechDichoRecursif(string mot)
        {
            bool exist = false;

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
                    }
                }
            }


            return exist;
        }

    }
}
