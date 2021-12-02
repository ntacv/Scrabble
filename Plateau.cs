using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Plateau
    {
        char[,] plateau;

        public Plateau()
        {
            string path = "../../../Files/InstancePlateau.txt";
            if (File.Exists(path))
            {
                this.plateau = new char[15, 15];
                string[] allLines = File.ReadAllLines(path);

                for (int i = 0; i<allLines.Length; i++)
                {
                    string[] line = allLines[i].Split(';');
                    
                    for(int j = 0; j < 15; j++)
                    {
                        this.plateau[i, j] = Convert.ToChar(line[j]);
                    }
                }
            }
        }


        public override string ToString()
        {
            //affiche coord ligne col

            string txt = "Plateau : \n";
            if (plateau != null && plateau.Length != 0)
            {
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {
                        txt += plateau[i, j] + " ";
                    }
                    txt += "\n";
                }
            }
            else txt += "null";
            return txt;
        }

    }
}
