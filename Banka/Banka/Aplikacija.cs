using System;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class Aplikacija
    {
        String nazivBanke;
        Klijent klijent;
        float iznosKredita;
        int brojMesecnihRata;
        float mesecnaRata = 0;
        String odobreno;

        public Aplikacija(string nazivBanke, Klijent klijent, float iznosKredita, int brojMesecnihRata, String odobreno)
        {
            this.nazivBanke = nazivBanke;
            this.klijent = klijent;
            this.iznosKredita = iznosKredita;
            this.brojMesecnihRata = brojMesecnihRata;
            this.mesecnaRata = iznosKredita / brojMesecnihRata;
            this.odobreno = odobreno;
        }

        public String getNazivBanke()
        {
            return this.nazivBanke;
        }

        public Klijent getKlijent()
        {
            return this.klijent;
        }

        public float getIznosKredita()
        {
            return this.iznosKredita;
        }

        public void setIznosKredita(float iznos)
        {
            this.iznosKredita = iznos;
        }

        public int getBrojMesecnihRata()
        {
            return this.brojMesecnihRata;
        }
        public void setBrojMesecnihRata(int br)
        {
            this.brojMesecnihRata = br;
        }

        public String getOdobreno()
        {
            return this.odobreno;
        }

        public float getMesecnaRata()
        {
            return this.mesecnaRata;
        }
        public void setMesecnaRata(int rata)
        {
            this.mesecnaRata = rata;
        }

        public void setOdobreno(String o)
        {
            this.odobreno = o;
        }

        public override string ToString()
        {
            return "Iznos kredita: " + iznosKredita + ", broj rata: " + brojMesecnihRata;
        }
    }
}
