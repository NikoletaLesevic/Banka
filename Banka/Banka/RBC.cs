using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class RBC : Banka
    {
        public override int obradaZahteva(Aplikacija a)
        {
            //RBC odbija kredit ako klijent ima
            //neki aktivan(neisplaćen) kredit u RBC - ju, ili ukoliko plata nije bar duplo veća od rate
            ObradaZahteva obrada = new ObradaZahteva();

            List<Aplikacija> aplikacije = obrada.ucitajAplikacije();

            //ako nema uslov za kredit vrati 0
            for (int i=0;i<aplikacije.Count;i++)
            {
                if (a.getKlijent().getJmbg() == aplikacije[i].getKlijent().getJmbg() && aplikacije[i].getNazivBanke() == "rbc" && aplikacije[i].getOdobreno() == "da")
                    return 0;
            }

            
            if (a.getKlijent().getMesecnaPrimanja() < a.getMesecnaRata() * 2)
                return 0;
            else return 1;
        }
    }
}
