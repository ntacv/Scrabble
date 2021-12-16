using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime;
using System.Diagnostics;

namespace Scrabble
{
    class Jeu
    {
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
        double _timeLeft;


        public Jeu()
        {
            int init = Program.AskSaves();
            _indexTour=0;

            
            _lapLasting = 10;
            

            if (init == 0) Initialisation();
            else Sauvegarde();

            StartGame();
        }


        public void Initialisation()
        {

            Random rdm = new Random();
            //définition du dictionnaire
            this._dicho = new Dictionnaire[1];
            lang = Program.AskLanguage();
            _dicho[indexDicho] = new Dictionnaire(lang);

            //recherche du texte d'initialisation
            string path = "../../../Files/Initialisation.txt";
            string content = ReadFile(path);
            string[] parts = content.Split("\r\n\r\n");
            _plateau = new Plateau(parts[0]);
            _curseur = new Cursor(_plateau);
            _sac_jetons = new Sac_Jetons(parts[1]);

            //définition des joeurs
            int add; int nb=0;
            _joueurs = new List<Joueur> { };
            do
            {
                Joueur player = new Joueur();
                for(int i = 0; i < 8; i++)
                {
                    player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
                }
                _joueurs.Add(player);
                nb++;
                add = Program.AskJoueur();
            } while (add == 0 && nb<4);


            _indexTour = 0;

            //Console.WriteLine(JoueurToString());


        }
        public void Sauvegarde()
        {
            
            string path = "../../../Files/Sauvegarde.txt";
            string content = ReadFile(path);
            string[] parts = content.Split("\r\n\r\n");

            _plateau = new Plateau(parts[1]);
            _curseur = new Cursor(_plateau);
            _sac_jetons = new Sac_Jetons(parts[2]);

            //définition du dictionnaire
            this._dicho = new Dictionnaire[1];
            _dicho[indexDicho] = new Dictionnaire(parts[0]);
            lang = parts[0];
            //définition des joeurs
            _joueurs = new List<Joueur> { };
            
            for(int i=3;i<parts.Length;i++)
            {
                string[] lines = parts[i].Split("\r\n");
                string[] line1 = lines[0].Split(';');
                string[] tabMot = lines[1].Split(';');
                List<string> listMot = new List<string> { };
                for(int j=0; j < tabMot.Length; j++)
                {
                    listMot.Add(tabMot[j]);
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

        public bool SaveGame()
        {

            string txt = lang + "\r\n\r\n";

            txt += _plateau.ToStringSave() + "\r\n";
            txt += _sac_jetons.ToStringSave() + "\r\n\r\n";
            for(int i=0;i<_joueurs.Count-1; i++)
            {
                txt += _joueurs[i].ToStringSave() + "\r\n\r\n";
            }
            txt += _joueurs[_joueurs.Count-1].ToStringSave();

            Console.Write(txt);
            WriteFile("../../../Files/Sauvegarde.txt", txt);


            return true;
        }

        public void StartGame()
        {
            InGame();
        }
        public void InGame()
        {
            
            do
            {
                Joueur player = _joueurs[indexJoueur];
                Console.Clear();
                //_plateau.ToStringColor(_curseur.Position);

                _time = new Stopwatch();
                _time.Start();

                while (_time.ElapsedMilliseconds < _lapLasting * 1000)
                {

                    Console.WriteLine("C'est au tour de " + player.ToStringGame());
                    PlaceWord(player, _indexTour);
                    SaveGame();

                }

                indexJoueur++;
                _indexTour++;
                if (indexJoueur == _joueurs.Count)
                {
                    indexJoueur = 0;
                }

                int sleepTime = 1000; // in mills
                Thread.Sleep(sleepTime);

            } while (EndGame());
        }
        public bool EndGame()
        {
            return true;
        }
        

        public void PlaceWord(Joueur player, int indexTour)//joueur en cours 
        {

            string mot = null;
            int index = 0;
            int position = 0;
            int orientation = 0;
            int indexConfirm = 0;
            Random rdmRepioche;

            int choixTour = Program.AskTour(player);
            if(choixTour == 1)
            {
                player.Remove_AllMainCourante();
                for(int l=0;l<player.Main_Courante.Count; l++)
                {
                    rdmRepioche = new Random();
                    player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdmRepioche));
                }
            }
            else
            {
                if(choixTour == 2)
                {
                    return; 
                }
            }


            do
            {
                if (indexConfirm != 0)
                {
                    player.ToStringGame();
                }

                do
                {
                    //plateau ne s'affiche pas après la premiere
                    _plateau.ToStringColor(_curseur.Position);
                    player.ToStringGame();


                    _timeLeft = (_lapLasting - _time.Elapsed.TotalSeconds / 1000);
                    Console.WriteLine("Temps restant : "+_timeLeft+"s");

                    //mot = Program.VerifieStringWord("Taper un mot à placer : ", _dicho[indexDicho]);
                    do
                    {
                        mot = Program.VerifieStringWord("Taper un mot : ", _dicho[indexDicho]);
                    }
                    while (mot.Length < 2 || mot.Length > 15 || !_dicho[indexDicho].RechDichoRecursif(mot, 0, _dicho[indexDicho].MotsTrie[mot.Length].Length-1));

                    mot = mot.ToUpper();
                    Console.WriteLine("votre mot \"" + mot + "\" est correct");
                    index++;

                    //1er mot sur la case centrale
                    if (indexTour > 0)
                    {
                        _curseur.AskMovm();

                        #region Demande position
                        /*
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
                        */
                        #endregion
                    }
                    else { _curseur.Position = new int[] { 7, 7 }; }

                    orientation = Program.AskDirection();

                } while (!ConfirmPlaceWord(mot, orientation, _curseur.Position, player));
                indexConfirm = Program.AskConfirm();
            }
            while (indexConfirm != 0);
            

            player.Replace_Main_Courante();

            //place mot : mot//coordonnees//Hori/Vertical
            _plateau.AddWord(mot,_curseur.Position,orientation);
            player.Add_Mot(mot);
            player.Add_Score(CalculScore(mot));

            //Repioche les lettres en moins
            Random rdm = new Random();
            while (player.Main_Courante.Count < 7)
            {
                player.Add_Main_Courante(_sac_jetons.Retire_Jeton(rdm));
            }
        }
        /*
        public void PlaceWordFirst(Joueur player)//joueur en cours 
        {

            string mot = null;
            int index = 0;
            int position = 0;
            int orientation = 0;



            do
            {
                do
                {
                    //plateau ne s'affiche pas après la premiere

                    mot = Program.VerifieStringWord("Taper un mot à placer : ", _dicho[indexDicho]);
                    
                    mot = mot.ToUpper();
                    Console.WriteLine("votre mot \"" + mot + "\" est correct");
                    index++;

                    orientation = Program.AskDirection();

                } while (!ConfirmPlaceWord(mot, orientation, new int[] { 7, 7 }, player));
            }
            while (Program.AskConfirm() != 0);


            //place mot : mot//coordonnees//Hori/Vertical
            _plateau.AddWord(mot, _curseur.Position, orientation);

            player.Add_Score(CalculScore(mot));

        }
        */
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
                #region verticale
                /*
                        else //verticale
                        {// A CHANGER d'apres horiz
                            if (position[0] + mot.Length < 15)
                            {
                                for (int i = 0; i < mot.Length; i++)
                                {
                                    char lettrePlateau = _plateau.Board[position[0] + i, position[1]];
                                    if (Char.IsLetter(lettrePlateau))
                                    {//A to Z
                                        if (lettrePlateau == mot[i])
                                        {
                                            possible = true;
                                        }
                                        else
                                        {
                                            possible = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    */ 
                #endregion
            }
            possible = true;
            for(int i = 0; i < mot.Length; i++)
            {
                
                if (!lettrePossible[i])
                {
                    possible = false;
                }
            }
            if (!possible)
            {
                player.Replace_Main_Courante();
            }       
            return possible;
        }

        public bool ConfirmOtherWord(string mot, int orientation, int[] position, Joueur player)
        {
            bool possible = false;
            int ligne = 0;
            int colonne = 0;
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

            return possible;
        }

        /// <summary>
        /// Calcul du temps par tour
        /// </summary>
        public void ElapsedTime()
        {
            
        }
        public string JoueurToString()
        {
            string txt = "Joueurs : \n";
            for(int i=0;i<_joueurs.Count; i++)
            {
                txt += _joueurs[i].ToStringShort()+"\n";
            }
            return txt;
        }
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

        /* code score spéciaux

        0 : case milieu/début
        1 : case vide
        2 : mot double
        3 : mot triple
        4 : lettre double
        5 : lettre triple
        A-Z* : Jetons

        */
        public int CalculScore(string mot)
        {
            int score = 0;
            if(mot!=null && mot.Length != 0)
            {
                int DW = 0;
                int TW = 0;

                for(int i=0; i < mot.Length; i++)
                {
                    char lettreJeton = _plateau.Board[_curseur.Position[0],_curseur.Position[0]];

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

                    Jeton lettre = _sac_jetons.InfoJeton(mot[i]);
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
                }
            }
            return score;
        }
        
    }
}
