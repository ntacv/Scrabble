using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    class Joueur
    {
        //Attributs
        string nom;
        int score;
        List<string> mots;
        List<Jeton> main_Courante;
        List<Jeton> main_Courante_En_Cours;

        public List<Jeton> Main_Courante
        {
            get { return main_Courante; }
        }
        public List<Jeton> Main_Courante_En_Cour
        {
            get { return main_Courante_En_Cours; }
            set { main_Courante_En_Cours = value; }
        }

        //Constructeur
        public Joueur()
        {
            string nom = Program.VerifieString("Veuillez saisir un nom : ");
            this.nom = nom;
            score = 0;
            mots = null;
            main_Courante = new List<Jeton> { };
            main_Courante_En_Cours = new List<Jeton> { };
        }
        public Joueur(string nom, int score, List<string> mots, List<Jeton> Main)
        {
            this.nom = nom;
            this.score = score;
            this.mots = mots;
            this.main_Courante = Main;
            this.main_Courante_En_Cours = new List<Jeton> { };
        }
        /// <summary>
        /// définie les joueurs a partir d'un tableau de joueurs;
        /// </summary>
        /// <param name="joueurs">tableau de lignes pour chaque joueurs avec ';' entre les attributs</param>
        /*public Joueur(string[] joueurs)
        {
            if (joueurs != null && joueurs.Length != 0)
            {
                foreach (string joueur in joueurs)
                {
                    string[] joueurParam = joueur.Split(';');
                    string nom = joueurParam[0];
                    int score = Convert.ToInt32(joueurParam[1]);
                    string[] mots = joueurParam[2].Split(',');
                    Joueur moi = new Joueur(nom, score, mots);
                }
            }
        }*/

        public override string ToString()
        {
            string txt = nom + " ; " + score + " ;\n";
            if(mots!=null && mots.Count != 0)
            {
                for(int i = 0; i < mots.Count; i++)
                {
                    txt += mots[i]+" ; ";
                }
            }
            if (main_Courante != null && main_Courante.Count != 0)
            {
                for (int i = 0; i < main_Courante.Count; i++)
                {
                    txt += main_Courante[i].Lettre + " ; ";
                }
            }

            return txt;
        }
        public string ToStringShort()
        {
            string txt = nom + " " + score;
            return txt;
        }
        public string ToStringGame()
        {
            string txt = nom + " ; " + score + " ;\n";
            
            if (main_Courante != null && main_Courante.Count != 0)
            {
                for (int i = 0; i < main_Courante.Count; i++)
                {
                    txt += main_Courante[i].Lettre + " | ";
                }
            }

            return txt;
        }

        public void Add_Mot(string mot)
        {
            if (mot != null && mot.Length != 0)
            {
                mots.Add(mot);
            }
        }
        public void Add_Score(int val)
        {
            if (val >= 0 && val < 100)
            {
                score += val;
            }
        }
        
        public void Add_Main_Courante(Jeton monjeton)
        {
            if(main_Courante.Count<7)
            {
                main_Courante.Add(monjeton);
            }
        }
        public void Remove_Main_Courante(Jeton monjeton)
        {
            main_Courante.Remove(monjeton);
        }
        //public bool In_Main_Courante(Plateau plateau, string mot)
        
        
    }
}
