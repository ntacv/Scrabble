using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Scrabble
{
    public class Joueur
    {
        //Attributs
        string nom;
        int score;
        List<string> mots;
        List<Jeton> main_Courante;
        List<Jeton> main_Courante_Save;

        #region Paramètres

        public List<string> Mots
        {
            get { return mots; }
        }
        public List<Jeton> Main_Courante
        {
            get { return main_Courante; }
        }
        public List<Jeton> Main_Courante_Save
        {
            get { return main_Courante_Save; }
            set { main_Courante_Save = value; }
        }
        #endregion

        //Constructeur
        public Joueur()
        {
            string nom = Program.VerifieString("Veuillez saisir un nom : ");
            this.nom = nom;
            score = 0;
            mots = new List<string> { };
            main_Courante = new List<Jeton> { };
            main_Courante_Save = new List<Jeton> { };
        }

        /// <summary>
        /// définie les joueurs a partir d'un tableau de joueurs;
        /// </summary>
        /// <param name="joueurs">tableau de lignes pour chaque joueurs</param>
        public Joueur(string nom, int score, List<string> mots, List<Jeton> Main)
        {
            this.nom = nom;
            this.score = score;
            if (mots != null)
            {
                this.mots = mots;
            }
            else this.mots = new List<string> { };
            this.main_Courante = Main;
            this.main_Courante_Save = new List<Jeton> { };
        }
        
        

        #region toString
        
        public override string ToString()
        {
            string txt = nom + " ; " + score + " ;\n";
            if (mots != null && mots.Count != 0)
            {
                for (int i = 0; i < mots.Count; i++)
                {
                    txt += mots[i] + " ; ";
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
        public string ToStringSave()
        {
            string txt = nom + ";" + score + ";\r\n";
            if (mots != null && mots.Count != 0)
            {
                for (int i = 0; i < mots.Count - 1; i++)
                {
                    txt += mots[i] + ";";
                }
                txt += mots[mots.Count - 1];
            }
            txt += "\r\n";
            if (main_Courante != null && main_Courante.Count != 0)
            {
                for (int i = 0; i < main_Courante.Count - 1; i++)
                {
                    txt += main_Courante[i].Lettre + ";";
                }
                txt += main_Courante[main_Courante.Count - 1].Lettre;
            }
            else txt += "\r\n";

            return txt;
        }

        //Affiche juste le nom et le score du joueur
        public string ToStringShort()
        {
            string txt = nom + " " + score;
            return txt;
        }

        //Affiche le nom, le score et la main courante
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
        #endregion


        #region add et remove des lettres du sac de jeton dans les mains courantes

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
            if (main_Courante.Count < 7)
            {
                main_Courante.Add(monjeton);
            }
        }
        public void Add_Main_Courante_Save(Jeton monjeton)
        {
            if (main_Courante_Save.Count < 7)
            {
                main_Courante_Save.Add(monjeton);
            }
        }

        public void Remove_Main_Courante(Jeton monjeton)
        {
            main_Courante.Remove(monjeton);
        }

        public void Remove_Main_Courante_Save(Jeton monjeton)
        {
            main_Courante_Save.Remove(monjeton);
        }

        public void Replace_Main_Courante()
        {
            /*if (main_Courante_Save.Count != 0)
            {
                main_Courante = main_Courante_Save;
            }
            */
            main_Courante = main_Courante_Save;
        }

        public void Remove_AllMainCourante()
        {
            for (int i = 0; i < main_Courante.Count; i++)
            {
                main_Courante.Remove(main_Courante[i]);

            }
        }
        #endregion
    }
}
