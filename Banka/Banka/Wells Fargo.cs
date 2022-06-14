using System;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class Wells_Fargo : Banka
    {
        public override int obradaZahteva(Aplikacija a)
        {
            //Wells Fargo odobrava kredit ukoliko su mesečna primanja klijenta veća od rate kredita.

            //ako ispunjava uslov za kredit vrati 1
            if (a.getKlijent().getMesecnaPrimanja() > a.getMesecnaRata())
                return 1;
            else return 0;
        }
    }
}
