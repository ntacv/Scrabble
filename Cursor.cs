using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scrabble
{
    class Cursor
    {

        Plateau lab;
        int[] position;
        public int[] Position
        {
            get { return position; }
            set { position = value; }
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

        public void PlaceLettre(List<Jeton> main_courante, Plateau lab)
        {
            if (position != null && position[0] < lab.Board.GetLength(0) && position[1] < lab.Board.GetLength(1))
            {
                ConsoleKey inputKey;
                do { 
                    inputKey = Console.ReadKey().Key;
                    Console.WriteLine(inputKey);
                    //if (inputKey >=65 && inputKey <=90 )//65 90 A to Z
                    {
                        
                    }
                } while (inputKey != ConsoleKey.Enter);

            }
        }
        public bool DirectionPossible(Plateau lab)
        {
            bool possible = false;

            if(position!=null && position[0]<lab.Board.GetLength(0) && position[1] < lab.Board.GetLength(1))
            {
                for(int i = 0; i < lab.Board.GetLength(0); i++)
                {
                    for (int j = 0; j < lab.Board.GetLength(1); j++)
                    {
                        if(lab.Board[position[0],position[1]] == 0)
                        {

                        }
                    }
                }
            }
            return possible;
        }
        //ne marche pas? return position choisi?
        public int AskMovm()
        {
            int init = 0;

            //Plateau plateau = new Plateau;
            int[] index = Movement(lab);
            Console.WriteLine(index[0]+" , "+index[1]);
            position[0] = index[0];
            position[1] = index[1];
            return init;
        }
        public static int[] Movement(Plateau plateau)
        {
            ConsoleKey inputKey;
            int[] index = new int[] { 7, 7 };
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
