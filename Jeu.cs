using System;
using System.Collections.Generic;
using System.Text;


namespace Scrabble
{
    class Jeu
    {
        
        // methode calcul score

        //score spéciaux (TL DW)


        Dictionnaire[] _dicho;
        Plateau _plateau;
        Sac_Jetons _sac_jetons;
        


        public Jeu()
        {

            this._dicho = new Dictionnaire[1];
            _dicho[0] = new Dictionnaire("Francais");




        }

        

        public void StartGame(bool init, int lang)
        {

        }
        
        
        public void PlaceWord()
        {
            //bool possible = false;

            //1er mot sur la case central

            string mot = null;
            do
            {
                mot = Program.VerifieString("Taper un mot : ");
            } while (!_dicho[0].RechFor(mot));
            Console.WriteLine("votre mot \"" + mot + "\" est correct");
            //place mot : mot//coordonnees//Hori/Vertical

            //return possible;
        }

    }
}
