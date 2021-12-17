using System;
using System.IO;
using System.Threading;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(60, 30);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Scrabble!\n\n");
            Thread.Sleep(1000);
            Console.ResetColor();

            
            Jeu JeuTest = new Jeu();
        }


        /// <summary>
        /// Vérification si une phrase est valide
        /// </summary>
        /// <param name="phrase">phrase à vérifier</param>
        /// <returns></returns>
        public static string VerifieString(string phrase)
        {
            string txt=null;

            do
            {
                Console.Write(phrase);
                txt = Convert.ToString(Console.ReadLine());
            } while (txt == null || txt.Length==0);
            
            return txt;
        }
        //Méthode qui vérifie si un mot est valide 
        public static string VerifieStringWord(string phrase, Dictionnaire _dicho)
        {
            string txt = null;
            //index pour passer le tour apres 5 tentative
            int index_tentative = 0;
            do
            {
                Console.Write(phrase);
                txt = Convert.ToString(Console.ReadLine());
                index_tentative++;
            } while (txt == null || txt.Length == 0);
            //index_tentative < 5// && txt == null || txt.Length == 0);

            return txt;
            /*
            string mot = null;
            //index pour passer le tour apres 5 tentative
            int index_tentative = 0;
            do
            {
                Console.Write(phrase);
                mot = Convert.ToString(Console.ReadLine());
                if(mot == "*")
                {
                    mot = "";
                }
                Console.WriteLine(mot);
                index_tentative++;
                //Console.WriteLine(index_tentative);
            }
            while(mot == null || mot.Length == 0);
            //while (mot == null || mot.Length < 2 || mot.Length > 15 ||_dicho.RechDichoRecursif(mot, 0, _dicho.MotsTrie[mot.Length].Length));
            //index_tentative < 5// && txt == null || txt.Length == 0);

            return mot;
            */
        }
        //isletter
        public static bool VerifieChar(char lettre)
        {
            bool possible = false;
            if(lettre>=65 && lettre <= 90)
            {
                possible = true;
            }
            return possible;
        }

        /// <summary>
        /// Séries de méthodes qui demandent des modes de jeux différents
        /// </summary>
        /// <returns></returns>
        #region Asks

        
        public static string AskLanguage()
        {
            string[] menu = new string[] { "Francais", "English", "Español" };

            int index = Menu(menu);
            
            return menu[index];
        }
        public static int AskSaves()
        {
            int init = 0;

            string[] menu = new string[2] { "Commencer", "Reprendre" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : Commencer 
            // 1 : Reprendre

            return init;
        }
        public static int AskJoueur()
        {
            int init = 0;

            
            string[] menu = new string[2] { "Ajouter un joueur", "Commencer la partie" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            //0 : Ajouter
            //1 : Commencer
            return init;
        }
        public static int AskTour(Joueur player)
        {
            int init = 0;

            Console.SetCursorPosition(0, Console.CursorTop +1);

            string[] menu = new string[3] { "Placer un mot", "Repiocher", "Passer son tour" };

            int index = MenuPlayer(menu, player);
            if (index == 1) init = 1;
            if (index == 2) init = 2;
            //0 : placer
            //1 : piocher
            //2 : passer

            return init;
        }
        public static int AskConfirm()
        {
            int init = 0;

            
            string[] menu = new string[2] { "Confirme", "Changer" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : Confirme 
            // 1 : changer

            return init;
        }
        public static int AskDirection()
        {
            int init = 0;

            string[] menu = new string[2] { "Horizontale", "Verticale" };

            int index = Menu(menu);
            if (index == 1) init = 1;
            // O : horizontale 
            // 1 : verticale
            return init;
        }
        #endregion
        
        /// <summary>
        /// Méthode menu qui permet de faire des choix avec les flèches du clavier
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>return un nombre qui indique quelle option on a choisi</returns>
        public static int Menu(string[] menu)
        {
            ConsoleKey inputKey;
            int index = 0;
            do
            {
                Console.Clear();
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (i == menu.Length - 1) Console.Write(menu[i]);
                    else Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                inputKey = Console.ReadKey().Key;
                if (inputKey == ConsoleKey.DownArrow && index < menu.Length - 1)//40 : Down arrow key
                {
                    index++;
                }
                if (inputKey == ConsoleKey.UpArrow && index > 0)//38 : Up arrow key
                {
                    index--;
                }

            } while (inputKey != ConsoleKey.Enter);
            return index;
        }
        /// <summary>
        /// Même chose que pour la fonction Menu mais avec un affichage différent qui rajoute la main du joueur
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="player">Nom du joueur en cour</param>
        /// <returns></returns>
        public static int MenuPlayer(string[] menu, Joueur player)
        {
            ConsoleKey inputKey;
            int index = 0;

            player.ToStringGame();
            Console.SetCursorPosition(0, Console.CursorTop +2);
            /*
            do
            {
                player.ToStringGame();
                Console.WriteLine("Voici votre main, appuier sur entrer pour continuer...");
            } while (ConsoleKey.Enter != Console.ReadKey().Key);
            */
            do
            {
                int currentLineCursor = Console.CursorTop;

                for (int j = 0; j < menu.Length; j++)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                //Console.SetCursorPosition(0, currentLineCursor - menu.Length+1);


                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == index)
                    {

                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (i == menu.Length - 1) Console.Write(menu[i]);
                    else Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                inputKey = Console.ReadKey().Key;
                if (inputKey == ConsoleKey.DownArrow && index < menu.Length - 1)//40 : Down arrow key
                {
                    index++;
                }
                if (inputKey == ConsoleKey.UpArrow && index > 0)//38 : Up arrow key
                {
                    index--;
                }


                player.ToStringGame();

            } while (inputKey != ConsoleKey.Enter);
            return index;
        }

        /// <summary>
        /// Méthode qui remplace la foncion Console.Clear() mais en moin radicale
        /// </summary>
        public static void ClearConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static void ClearConsoleLine2()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor-1);
        }
        
    }
    
}
