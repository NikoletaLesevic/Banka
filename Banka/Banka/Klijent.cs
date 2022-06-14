using System;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class Klijent
    {
        String ime; 
        String jmbg;
        float mesecnaPrimanja;
        int godineRadnogStaza;

        public Klijent(string ime, string jmbg, float mesecnaPrimanja, int godineRadnogStaza)
        {
            this.ime = ime;
            this.jmbg = jmbg;
            this.mesecnaPrimanja = mesecnaPrimanja;
            this.godineRadnogStaza = godineRadnogStaza;
        }

        public String getIme()
        {
            return this.ime;
        }

        public String getJmbg()
        {
            return this.jmbg;
        }

        public float getMesecnaPrimanja()
        {
            return this.mesecnaPrimanja;
        }

        public float getGodineRadnogStaza()
        {
            return this.godineRadnogStaza;
        }

    }
}
