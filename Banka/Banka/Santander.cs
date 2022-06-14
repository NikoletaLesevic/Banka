using System;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class Santander : Banka
    {
        public override int obradaZahteva(Aplikacija a)
        {
            //Santander odobrava kredit ukoliko klijent ima preko 5 godina radnog staža i
            //ukoliko je iznos mesečne rate manji od 70 % mesečnog pirmanja klijenta

            //ako ima uslov za kredit vrati 1
            if (a.getKlijent().getGodineRadnogStaza() > 5 || a.getMesecnaRata() < a.getKlijent().getMesecnaPrimanja() * 0.7)
                return 1;
            else return 0;
        }
    }
}
