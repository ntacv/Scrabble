using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


namespace Scrabble
{
    class Jeu
    {
        
        // methode calcul score

        //score spéciaux (TL DW)


        Dictionnaire[] _dicho;
        Plateau _plateau;
        Sac_Jetons _sac_jetons;
        List<Joueur> _joueurs;
        Cursor _curseur;


        public Jeu()
        {


            int init = Program.AskSaves();

            if (init == 0) Initialisation();
            else Sauvegarde();
            //Cursor curseur = new Cursor(_plateau);
            //curseur.AskMovm();
            InGame();
            //Console.WriteLine(_sac_jetons.ToString());
            

        }


        public void Initialisation()
        {

            Random rdm = new Random();
            //définition du dictionnaire
            this._dicho = new Dictionnaire[1];
            string lang = Program.AskLanguage();
            _dicho[0] = new Dictionnaire(lang);


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

            


            Console.WriteLine(JoueurToString());


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
            _dicho[0] = new Dictionnaire(parts[0]);

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
            

        }

        public bool SaveGame()
        {
            return true;
        }

        public void StartGame()
        {

            InGame();
        }
        public void InGame()
        {
            int index = 0;
            do
            {
                Joueur player = _joueurs[index];
                Console.Clear();
                _plateau.ToStringColor(_curseur.Position);
                Console.WriteLine("C'est au tour de " + player.ToStringGame());
                PlaceWord(player);
                SaveGame();
                
                index++;
                if (index == _joueurs.Count)
                {
                    index = 0;
                }

                int sleepTime = 1000; // in mills
                Thread.Sleep(sleepTime);

            } while (EndGame());
        }
        public bool EndGame()
        {
            return true;
        }
        
        public void PlaceWord(Joueur player)
        {

            //déplacement curseur
            //_curseur.AskMovm();
            _curseur.PlaceLettre(player.Main_Courante, _plateau);

            //1er mot sur la case central

            string mot = null;
            int index = 0;
            do {
                //if(index>0) Program.ClearConsoleLine2();
                do
                {
                    mot = Program.VerifieString("Taper un mot : ");

                } while (mot.Length < 2 || mot.Length > 15 || !_dicho[0].RechDichoRecursif(mot, 0, _dicho[0].MotsTrie[mot.Length].Length));

                Console.WriteLine("votre mot \"" + mot + "\" est correct");
                index++;
            }
            while(Program.AskConfirm() != 0);


            //place mot : mot//coordonnees//Hori/Vertical


            player.Add_Score(CalculScore(mot));

            //return possible;
        }
        
    
        
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
        public void WriteFile(string filename)
        {

            StreamWriter SWrite = null;
            try
            {
                SWrite = new StreamWriter(filename);
                /*for (int i = 0; i < CompteBancaire.Count; i++)
                {
                    SWrite.WriteLine(CompteBancaire[i].ToString());
                }*/
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

        public int CalculScore(string mot)
        {
            int score = 0;

            if(mot!=null && mot.Length != 0)
            {

                for(int i=0; i < mot.Length; i++)
                {
                    Jeton lettre = _sac_jetons.InfoJeton(mot[i]);
                    score += lettre.Score;
                }

            }

            return score;
        }
    }
}
