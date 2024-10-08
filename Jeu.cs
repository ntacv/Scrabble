﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime;
using System.Diagnostics;

namespace Scrabble
{
    public class Jeu
    {
        #region Attributs

        Dictionnaire[] _dicho;
        Plateau _plateau;
        Sac_Jetons _sac_jetons;
        List<Joueur> _joueurs;
        Cursor _curseur;
        string lang;
        int _indexTour;
        int indexDicho = 0;
        int indexJoueur = 0;
        Stopwatch _time;
        double _lapLasting;
        int _timeLeft;

        #endregion

        //Constructeur qui lance la partie
        public Jeu()
        {
            
            int init = Program.AskSaves();
            _indexTour=0;

            
            if (init == 0) Initialisation();
            else Sauvegarde();

            StartGame();
        }

        /// <summary>
        /// Méthode qui va chercher le fichier l'initialisation 
        /// </summary>
        public void Initialisation()
        {

            Random rdm = new Random();
            //définition du dictionnaire
            this._dicho = new Dictionnaire[1];
            lang = Program.AskLanguage();
            _dicho[indexDicho] = new Dictionnaire(lang);

            _lapLasting = Program.AskTime("Combien de temps dure un tour (s) : ");

            //recherche du texte d'initialisation
            string path = "../../../Files/Initialisation.txt";
            string content = ReadFile(path);
            string[] parts = content.Split("\r\n\r\n");
            _plateau = new Plateau(parts[0]);
            _curseur = new Cursor(_plateau);
            _sac_jetons = new Sac_Jetons(parts[1]);

            //définition des joueurs
            int add; int nb=0;
            _joueurs = new List<Joueur> {};
            do
            {
                Joueur player = new Joueur(false);//not AI
                for(int i = 0; i < 8; i++)
                {
                    player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
                }
                // add player as AI
                _joueurs.Add(player);
                nb++;
                add = Program.AskJoueur();
            } while (add == 0 && nb<4);

            _indexTour = 0;
        }
        /// <summary>
        /// Méthode qui va chercher le fichier de sauvegarde
        /// </summary>
        public void Sauvegarde()
        {
            
            string path = "../../../Files/Sauvegarde.txt";
            string content = ReadFile(path);
            string[] parts = content.Split("\r\n\r\n\r\n\r\n");

            _plateau = new Plateau(parts[2]);
            _curseur = new Cursor(_plateau);
            _sac_jetons = new Sac_Jetons(parts[3]);

            //définition du dictionnaire
            this._dicho = new Dictionnaire[1];
            _dicho[indexDicho] = new Dictionnaire(parts[0]);
            lang = parts[0];
            //définition du temps
            _lapLasting = Convert.ToDouble(parts[1]);
            //définition des joeurs
            _joueurs = new List<Joueur> { };
            
            for(int i=4;i<parts.Length;i++)
            {
                string[] lines = parts[i].Split("\r\n");
                string[] line1 = lines[0].Split(';');
                string[] tabMot = new string[0] { };
                List<string> listMot = new List<string>();
                if (lines[1] != null && lines[1].Length != 0)
                {
                    tabMot = lines[1].Split(';');
                    listMot = new List<string> { };
                    for (int j = 0; j < tabMot.Length; j++)
                    {
                        listMot.Add(tabMot[j]);
                    }
                }
                

                string[] mainChar = lines[2].Split(';');
                List<Jeton> mainCourante = new List<Jeton> { };
                for(int k = 0; k < mainChar.Length; k++)
                {
                    mainCourante.Add(_sac_jetons.InfoJeton(Convert.ToChar(mainChar[k])));
                }
                Joueur player = new Joueur(line1[0],Convert.ToInt32(line1[1]), listMot, mainCourante);
                _joueurs.Add(player);
            } 

            Console.WriteLine(JoueurToString());

            for(int i = 0; i < _joueurs.Count; i++)
            {
                for(int j=0;j<_joueurs[i].Mots.Count; j++)
                {
                    _indexTour++;
                }
            }

        }

        /// <summary>
        /// Permet de placer le texte dans le fichier de sauvegarde
        /// </summary>
        /// <returns>vrai si la sauvegarde a pu se faire</returns>
        public bool SaveGame()
        {

            string txt = lang + "\r\n\r\n\r\n\r\n";
            txt += _lapLasting + "\r\n\r\n\r\n\r\n";
            txt += _plateau.ToStringSave() + "\r\n\r\n\r\n";
            txt += _sac_jetons.ToStringSave() + "\r\n\r\n\r\n\r\n";
            for(int i=0;i<_joueurs.Count-1; i++)
            {
                txt += _joueurs[i].ToStringSave() + "\r\n\r\n\r\n\r\n";
            }
            txt += _joueurs[_joueurs.Count-1].ToStringSave();

            WriteFile("../../../Files/Sauvegarde.txt", txt);
            return true;
        }
        
        /// <summary>
        /// Méthode qui définit chaque tour avant la fin de la partie
        /// </summary>
        public void StartGame()
        {
            
            do
            {
                Joueur player = _joueurs[indexJoueur];
                Console.Clear();

                Console.Write("C'est au tour de ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(player.ToStringShort());
                Console.ResetColor();


                PlaceWord(player, _indexTour);
                SaveGame();

                
                indexJoueur++;
                _indexTour++;
                if (indexJoueur == _joueurs.Count)
                {
                    indexJoueur = 0;
                }

                int sleepTime = 1000; // in mills
                Thread.Sleep(sleepTime);

            } while (EndGame());

            //Affichage finale du gagnant
            Console.WriteLine("Bravo le jeu est fini ! ");
            int scoreMax = 0;
            int indexGagnant = 0;
            for(int j=0; j<_joueurs.Count; j++)
            {
                if (_joueurs[j].Score > scoreMax)
                {
                    scoreMax = _joueurs[j].Score;
                    indexGagnant = j;
                }
            }
            Console.WriteLine(_joueurs[indexGagnant].Nom + " est le gagnant !");
        }

        /// <summary>
        /// Méthode qui stop le jeu quand il n'y a plus de jeton dans le sac
        /// </summary>
        /// <returns>vrai si la partie est finie</returns>
        public bool EndGame()
        {
            _sac_jetons.CalculTotal();
            return _sac_jetons.Total != 0;
        }
        
        /// <summary>
        /// Permet de demander les informations pour placer un mot
        /// </summary>
        /// <param name="player">Joueur en cours</param>
        /// <param name="indexTour">numéro du tour</param>
        public void PlaceWord(Joueur player, int indexTour)//joueur en cours 
        {

            string mot = null;
            int index = 0;
            int orientation = 0;
            int indexConfirm = 0;
            Random rdm = new Random();

            int choixTour = 0;

            //start ai round
            //if(player.AI) player.AIPlaceWord

            do
            {
                choixTour = Program.AskTour(player, _plateau, _curseur);

                switch (choixTour)
                {
                    case 0://continuer
                        break;
                    case 1://Repiocher la main
                        player.Remove_AllMainCourante();
                        for (int l = 0; l < player.Main_Courante.Count; l++)
                        {
                            player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
                        }
                        break;
                    case 2://Passer son tour
                        return;
                    default:
                        break;
                }

            } while (choixTour == 1);

            do
            {
                do
                {

                    while (player.Main_Courante.Count < 7)
                    {
                        player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
                    }
                    Console.Clear();
                    _plateau.ToStringColor(_curseur.Position);
                    Console.WriteLine(player.ToStringGame());

                    _time = new Stopwatch();
                    _time.Start();

                    do
                    {
                        _timeLeft = Convert.ToInt32(_lapLasting - _time.Elapsed.TotalSeconds);
                        Console.WriteLine("Temps restant : " + _timeLeft + "s");
                        mot = Program.VerifieStringWord("Taper un mot : ", _dicho[indexDicho]);
                        if(_time.ElapsedMilliseconds > _lapLasting * 1000)
                        {
                            Console.WriteLine("Time finished");
                            Thread.Sleep(1000);
                            return;
                        }
                    }
                    while ( (mot.Length < 2 || mot.Length > 15 || 
                    !_dicho[indexDicho].RechDichoRecursif(mot, 0, _dicho[indexDicho].MotsTrie[mot.Length].Length-1))
                    && _time.ElapsedMilliseconds < _lapLasting * 1000);

                    _time.Stop();


                    mot = mot.ToUpper();
                    Console.WriteLine("votre mot \"" + mot + "\" est correct");
                    index++;

                    _curseur.AskMovm();
                    #region Premier cas différent et alternative
                    /*
                                //1er mot sur la case centrale
                                if (indexTour > 0)
                                {
                                    _curseur.AskMovm();

                                    #region Demande position

                                    int ligne;
                                    do
                                    {
                                        Console.WriteLine("Choisissez ligne");
                                        ligne = Convert.ToInt32(Console.ReadLine());
                                    } while (ligne < 0 || ligne > 14);
                                    int colonne;
                                    do
                                    {
                                        Console.WriteLine("Choisissez colonne");
                                        colonne = Convert.ToInt32(Console.ReadLine());
                                    } while (colonne < 0 || colonne > 14);

                                    int[] pos = new int[] { ligne,colonne};
                                    _curseur.Position = pos;

                                    #endregion
                                }
                                else { _curseur.Position = new int[] { 7, 7 }; }
                                */ 
                    #endregion

                    orientation = Program.AskDirection();

                } while (!ConfirmWord(mot, orientation, _curseur.Position, player));
                
                indexConfirm = Program.AskConfirm();
            }
            while (indexConfirm != 0);
            

            player.Replace_Main_Courante();

            //place mot : mot//coordonnees//Hori/Vertical
            _plateau.AddWord(mot,_curseur.Position,orientation);
            player.Add_Mot(mot);
            player.Add_Score(CalculScore(mot, orientation));

            //Repioche les lettres en moins
            while (player.Main_Courante.Count < 7)
            {
                player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
            }
        }
        
        /// <summary>
        /// Vérifie que toute les conditions de mots croisés sont respéctées
        /// </summary>
        /// <param name="mot">mot à vérifier</param>
        /// <param name="orientation">direction choisi (horizontale ou verticale)</param>
        /// <param name="position">position du mot sur le plateau</param>
        /// <param name="player">Joueur en cours</param>
        /// <returns></returns>
        #region Confirm
        public bool ConfirmWord(string mot, int orientation, int[] position, Joueur player)
        {
            //bool possible = false;

            bool a = ConfirmPlaceWord(mot, orientation, position, player);

            
             bool b = ConfirmeInline(mot, orientation, position, player);

            b = true;


            return a && b ;
        }


        public bool ConfirmPlaceWord(string mot, int orientation, int[] position, Joueur player)
        {//ne verifie pas les lettres autours de notre mot en cours
            bool possible = false;
            int ligne = 0;
            int colonne = 0;
            bool[] lettrePossible = null;

            #region Jocker
            int jocker = 0;

            for(int i = 0; i < player.Main_Courante.Count; i++)
            {
                if(player.Main_Courante[i].Lettre == '*')
                {
                    jocker++;
                }
            }
            #endregion

            if (mot != null && mot.Length != 0)
            {
                lettrePossible = new bool[mot.Length];
                if (orientation == 0)//horizontale
                {
                    ligne = position[0];
                    colonne = position[1];
                }
                else
                {
                    ligne = position[0];
                    colonne = position[1];
                }
                if (colonne + mot.Length < 15)
                {
                    for (int i = 0; i < mot.Length; i++)
                    {
                        
                        if (orientation == 0)//horizontale
                        {
                            ligne = position[0];
                            colonne = position[1] + i;
                        }
                        else //Verticale
                        {
                            ligne = position[0] + i;
                            colonne = position[1];
                        }
                        

                        char lettrePlateau = _plateau.Board[ligne, colonne];
                        if (Char.IsLetter(lettrePlateau))//A to Z
                        {
                            if (mot[i] == _plateau.Board[ligne, colonne])
                            {
                                possible = true;
                            }
                            else
                            {
                                possible = false;
                                lettrePossible[i] = false;
                                break;
                            }
                        }
                        else
                        {
                            bool InHand = false;
                            for (int k = 0; k < player.Main_Courante.Count; k++)
                            {
                                if(player.Main_Courante[k].Lettre==mot[i])
                                {
                                    InHand=true;
                                }
                            }
                            if (!InHand && jocker > 0)
                            {                           
                                jocker--;
                                player.Remove_Main_Courante(_sac_jetons.InfoJeton('*'));
                                possible = true;
                                break;
                            }
                            if ( !InHand && jocker==0)
                                //!player.Main_Courante.Contains(_sac_jetons.InfoJeton(mot[i])))
                            {
                                
                                Console.WriteLine("Lettre non dans la main");
                                Console.WriteLine(player.ToStringGame());
                                Thread.Sleep(1000);
                                possible = false;
                                break;
                            }
                            else
                            {
                                //remove main courante
                                possible = true;
                                Console.WriteLine(player.ToStringGame());
                                //player.Remove_Main_Courante(_sac_jetons.InfoJeton(mot[i]));
                                player.Remove_Main_Courante_Save(_sac_jetons.InfoJeton(mot[i]));
                                
                                //ou
                                //return une list de char de la main et remove en dehors
                                //player.Main_Courante_En_Cour.Add(_sac_jetons.InfoJeton(mot[i]));
                            }
                        }
                        lettrePossible[i] = possible;
                    }
                }
            }
            possible = true;
            for(int i = 0; i < mot.Length; i++)
            {
                if (!lettrePossible[i])
                {
                    possible = false;
                }
            }
            if (!possible && player.Main_Courante_Save.Count!=0)
            {
                player.Replace_Main_Courante();
            }       
            return possible;
        }

        public bool ConfirmOtherWord(string mot, int orientation, int[] position, Joueur player)
        {
            bool possible = false;
            //int ligne = 0;
            //int colonne = 0;
            bool[] lettrePossible = null;

            possible = ConfirmeInline(mot, orientation, position, player);
            
            possible = true;
            for (int i = 0; i < mot.Length; i++)
            {

                if (!lettrePossible[i])
                {
                    possible = false;
                }
            }

            return possible;
        }
        public bool ConfirmeInline(string mot, int orientation, int[] position, Joueur player)
        {
            bool possible = false;
            int ligne = position[0];
            int colonne = position[1];
            int i=0;
            string motAgrandie = "";
            while(Char.IsLetter(_plateau.Board[ligne,colonne]) )
            {
                if (orientation == 0)//horizontale
                {
                    ligne = position[0];
                    colonne = position[1] + i;
                }
                else//verticale
                {
                    ligne = position[0] + i;
                    colonne = position[1];
                }
                i++;
                motAgrandie += _plateau.Board[ligne, colonne];
                
            }
            

            return possible;
        }
        #endregion
        
        
        /// <summary>
        /// Affiche tous les joueurs de la partie
        /// </summary>
        /// <returns></returns>
        public string JoueurToString()
        {
            string txt = "Joueurs : \n";
            for(int i=0;i<_joueurs.Count; i++)
            {
                txt += _joueurs[i].ToStringShort()+"\n";
            }
            return txt;
        }
        /// <summary>
        /// Permet d'aller chercher du texte dans un fichier
        /// </summary>
        /// <param name="filename">Chemin du fichier</param>
        /// <returns>Tout le texte du fichier</returns>
        public string ReadFile(string filename)
        {
            StreamReader SReader = null;
            string content = "";
            try
            {
                SReader = new StreamReader(filename);
                content = SReader.ReadToEnd();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (SReader != null) SReader.Close();
            }
            return content;
        }
        /// <summary>
        /// Permet d'aller écrire du texte dans un fichier
        /// </summary>
        /// <param name="filename">Chemin du fichier</param>
        /// <param name="txt">Texte à mettre dans le fichier</param>
        public void WriteFile(string filename, string txt)
        {

            StreamWriter SWrite = null;
            try
            {
                SWrite = new StreamWriter(filename);
                SWrite.Write(txt);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (SWrite != null) SWrite.Close();
            }

        }

        /// <summary>
        /// Permet de calculer le score d'un mot placé
        /// </summary>
        /// <param name="mot">mot donc on veut calculer le score</param>
        /// <returns>return le score calculé</returns>
        public int CalculScore(string mot, int orientation)
        {
            int score = 0;
            if(mot!=null && mot.Length != 0)
            {
                //int DW = 0;
                //int TW = 0;
                int ligne = 0;
                int colonne = 0;

                for(int i=0; i < mot.Length; i++)
                {

                    if (orientation == 0)//horizontale
                    {
                        ligne = _curseur.Position[0];
                        colonne = _curseur.Position[1] + i;
                    }
                    else //Verticale
                    {
                        ligne = _curseur.Position[0] + i;
                        colonne = _curseur.Position[1];
                    }

                    char lettreJeton = _plateau.Board[ligne,colonne];
                    /*
                    switch (lettreJeton)
                    {
                        case '2':
                            DW++;
                            break;
                        case '3':
                            TW++;
                            break;
                        default:
                            break;
                    }
                    */
                    Jeton lettre = _sac_jetons.InfoJeton(mot[i]);
                    score += lettre.Score;
                    /*
                    if (lettreJeton == 4)
                    {
                        score += lettre.Score*2;
                    }
                    else
                    {
                        if(lettreJeton == 5)
                        {
                            score += lettre.Score*3;
                        }
                        else
                        {
                            score += lettre.Score;
                        }
                    }
                    */
                }
                /*
                for (int k = 0; k <= DW; k++)
                {
                    score *= 2;
                }
                for (int k = 0; k <= TW; k++)
                {
                    score *= 3;
                }
                */
            }
            return score;
        }
        
    }
}
