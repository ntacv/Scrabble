using System;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Cursor
    {

        Plateau lab;
        int[] position;

        public int[] Position
        {
            get { return position; }
        }

        public Cursor(Plateau lab)
        {
            this.lab = lab;
            this.position = new int[] {7,7};
            /*
            if (ligne >= 0) this.ligne = ligne;
            else this.ligne = 0;
            if (colonne >= 0) this.colonne = colonne;
            else this.colonne = colonne;
            */
        }
        
        /*
        public int Ligne
        {
            get { return ligne; }
            set
            {
                if (ligne >= 0) ligne = value;
                else ligne = 0;
            }
        }
        public int Colonne
        {
            get { return colonne; }
            set
            {
                if (colonne >= 0) colonne = value;
                else colonne = 0;
            }
        }
        */

        public override string ToString()
        {
            return position[0].ToString() + ' ' + position[1].ToString();
        }
        public bool EstEgale(int[] position)
        {
            bool same = false;

            if (position[0] == this.position[0] && position[1] == this.position[1])
            {
                same = true;
            }

            return same;
        }

       /* public void DeplacementSuivant()
        {
            Console.Clear();

            int x = position[0];
            int y = position[1];


            Console.Write("Entree case possible (z,q,s,d): ");
            char inputKey;
            bool possiblePosition = false;
            do
            {
                inputKey = Console.ReadKey().KeyChar;

                for (int i = 0; i < directionPossible(lab, position).Length; i++)
                {
                    if (inputKey == directionPossible(lab, position)[i] && inputKey != ' ')
                    {
                        possiblePosition = true;
                    }
                    //Console.Write(directionPossible(lab, position)[i]);
                }
            } while (!possiblePosition);

            switch (inputKey)
            {
                case 'z':
                    Console.Write("up");
                    position = new Position(position.Ligne - 1, position.Colonne);
                    break;
                case 's':
                    Console.Write("down");
                    position = new Position(position.Ligne + 1, position.Colonne);
                    break;
                case 'q':
                    Console.Write("left");
                    position = new Position(position.Ligne, position.Colonne - 1);
                    break;
                case 'd':
                    Console.Write("right");
                    position = new Position(position.Ligne, position.Colonne + 1);
                    break;
                default:
                    break;
            }

            lab.MarquerPassage(position);

        }*/
        public static char[] directionPossible(Plateau lab, int[] position)
        {
            char[] key = new char[] { ' ', ' ', ' ', ' ' };
            int x = position[0];
            int y = position[1];

            if (lab.Board[x, y - 1] == 0 || lab.Board[x, y - 1] == 3)
            {//Si la case du haut n'est pas un mur
                key[0] = 'q';
            }
            if (lab.Board[x - 1, y] == 0 || lab.Board[x - 1, y] == 3)
            {
                key[1] = 'z';
            }
            if (lab.Board[x, y + 1] == 0 || lab.Board[x, y + 1] == 3)
            {
                key[2] = 'd';
            }
            if (lab.Board[x + 1, y] == 0 || lab.Board[x + 1, y] == 3)
            {
                key[3] = 's';
            }
            return key;
        }
        public int AskMovm()
        {
            int init = 0;

            //Plateau plateau = new Plateau;
            int[] index = Movement(lab);
            Console.WriteLine(index[0]+" , "+index[1]);
            return init;
        }
        public static int[] Movement(Plateau plateau)
        {
            ConsoleKey inputKey;
            int[] index = new int[] { 0, 0 };
            do
            {
                Console.Clear();
                plateau.ToStringColor(index);

                /*for (int i = 0; i < plateau.Board.GetLength(0); i++)
                {
                    if (i == index[0])
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    //Console.WriteLine(plateau) ;
                    Console.ResetColor();
                }*/
                inputKey = Console.ReadKey().Key;
                if (inputKey == ConsoleKey.DownArrow && index[1] < 15)//40 : Down arrow key
                {
                    index[0]++;
                }
                if (inputKey == ConsoleKey.UpArrow && index[1] > 0)//38 : Up arrow key
                {
                    index[0]--;
                }
                if (inputKey == ConsoleKey.LeftArrow && index[0] > 0)//40 : Down arrow key
                {
                    index[1]--;
                }
                if (inputKey == ConsoleKey.RightArrow && index[0] < 15)//40 : Down arrow key
                {
                    index[1]++;
                }

                
                
            } while (inputKey != ConsoleKey.Enter);
            return index;
        }
    }
}
