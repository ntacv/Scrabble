using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Scrabble
{
    public class Cursor
    {
        //Cette à été crée pour permettre de se déplacer sur le plateau à l'aide des flèches
        Plateau lab;
        int[] position;

        #region Paramètes
        public int[] Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion


        //Constructeur qui prend un plateau en paramètre et qui initialise la position du curseur au milieu de se plateau 
        public Cursor(Plateau lab)
        {
            this.lab = lab;
            this.position = new int[] {7,7};
        }
        
        
        public override string ToString()
        {
            return position[0].ToString() + ' ' + position[1].ToString();
        }

        
        //Méthode qui renvoie la position de la méthode mouvement
        public int AskMovm()
        {
            int init = 0;
            int[] index = Movement(lab);
            Console.WriteLine(index[0]+" , "+index[1]);
            position[0] = index[0];
            position[1] = index[1];
            return init;
        }

        /// <summary>
        /// Méthode qui déplace une case blanche sur le plateau
        /// </summary>
        /// <param name="plateau">plateau du jeu</param>
        /// <returns>return la dernière position du curseur</returns>
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
