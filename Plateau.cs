using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Plateau
    {
        char[,] plateau;

        /* code score spéciaux

        0 : case milieu/début
        1 : case vide
        2 : mot double
        3 : mot triple
        4 : lettre double
        5 : lettre triple

        */
        public Plateau(char car)
        {
            string path = "../../../Files/PlateauVide.txt";
            if (File.Exists(path))
            {
                this.plateau = new char[15, 15];
                string[] allLines = File.ReadAllLines(path);

                for (int i = 0; i < allLines.Length; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        this.plateau[i, j] = Convert.ToChar(allLines[i][j]);
                    }
                }
            }
        }
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
        public void ToStringColor()
        {
            Console.Write("Plateau : \n");
            if (plateau != null && plateau.Length != 0)
            {
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {

                        switch (plateau[i, j])
                        {
                            case '0':
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;

                                break;
                            case '1':
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case '2':
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                break;
                            case '3':
                                Console.BackgroundColor = ConsoleColor.Red;
                                break;
                            case '4':
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                break;
                            case '5':
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                                break;
                            default:
                                Console.ResetColor();
                                break;
                        }
                        Console.Write("  ");
                    }
                    Console.ResetColor();
                    Console.Write("\n");
                }
            }
            else Console.Write("null");
        }

    }
}
