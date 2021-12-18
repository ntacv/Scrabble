using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    public class Plateau
    {
        char[,] plateau;

        #region Paramètres
        public char[,] Board
        {
            get { return plateau; }
        }
        #endregion

        /* code score spéciaux

        0 : case milieu/début
        1 : case vide
        2 : mot double
        3 : mot triple
        4 : lettre double
        5 : lettre triple
        A-Z* : Jetons

        */

        //Constructeur 
        public Plateau(string content)
        {
            if (content != null && content.Length != 0)
            {
                this.plateau = new char[15, 15];
                string[] allLines = content.Split("\r\n");

                for (int i = 0; i < allLines.Length; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        this.plateau[i, j] = Convert.ToChar(allLines[i][j]);
                    }
                }
            }
        }

        #region Méthode toString
        //Méthode ToString qui affiche notre plateau
        public override string ToString()
        {
            //affiche coord ligne coll

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
        #endregion


        public string ToStringSave()
        {
            //affiche coord ligne col

            string txt = "";
            if (plateau != null && plateau.Length != 0)
            {
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {
                        txt += plateau[i, j] + "";
                    }
                    txt += "\r\n";
                }
            }
            else txt += "null";
            return txt;

        }

        //Méthode qui converti une matrice de chiffre en couleur pour former le plateau du srabble
        public void ToStringColor(int[] Position)
        {
            Console.Write("Plateau : \n" +
                "     2   4   6   8   10  12  14  \n" +
                "   1   3   5   7   9   11  13  15\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (plateau != null && plateau.Length != 0)
            {
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    if (i > 8)
                    {
                        Console.Write((i+1)+" ");
                    }else Console.Write(" "+(i+1)+" ");
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {
                       
                        Console.ForegroundColor = ConsoleColor.Gray;
                        switch (plateau[i, j])
                        {
                            case '0':
                                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                                if (Position[0] == i && Position[1] == j){Console.BackgroundColor = ConsoleColor.White;}
                                Console.Write("  ");
                                break;
                            case '1':
                                Console.BackgroundColor = ConsoleColor.Green;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write("  ");
                                break;
                            case '2':
                                Console.BackgroundColor = ConsoleColor.Magenta;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write("DW");
                                break;
                            case '3':
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write("TW");
                                break;
                            case '4':
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write("DL");
                                break;
                            case '5':
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write("TL");
                                break;
                            default:
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                if (Position[0] == i && Position[1] == j) { Console.BackgroundColor = ConsoleColor.White; }
                                Console.Write(plateau[i, j]+" ");
                                break;
                        }

                        

                    }
                    Console.ResetColor();
                    Console.Write("\n");
                }
            }
            else Console.Write("null");
            Console.ResetColor();
        }
        


        /// <summary>
        /// Méthode qui ajoute un mot sur notre plateau 
        /// </summary>
        /// <param name="mot">mot à ajouter</param>
        /// <param name="position">index ligne et colonne ou le mot est placé</param>
        /// <param name="orientation">choix de la direction (sois verticale ou horizontale)</param>
        public void AddWord(string mot, int[] position, int orientation)
        {

            if (orientation == 0)//horizontale
            {
                if (position[1] + mot.Length < 15)
                {
                    for (int i = 0; i < mot.Length; i++)
                    {
                        plateau[position[0], position[1] + i] = mot[i];
                    }
                }
            }
            else //verticale
            {
                if (position[0] + mot.Length < 15)
                {
                    for (int i = 0; i < mot.Length; i++)
                    {
                        plateau[position[0] + i, position[1]] = mot[i];
                    }
                }
            }
        }



    }
}
