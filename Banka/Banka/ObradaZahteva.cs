using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Banka
{
    class ObradaZahteva
    {
        public void zahtevanjeKredita()
        {
            Console.WriteLine("KREIRANJE ZAHTEVA ZA KREDIT\n");

            Console.WriteLine("Unesite naziv banke:");

            String nazivBanke = Console.ReadLine();
            while (nazivBanke.ToLower() != "rbc" && nazivBanke.ToLower() != "santander" && nazivBanke.ToLower() != "wells fargo")
            {
                Console.WriteLine("Pogrešan naziv banke, unesite naziv banke ponovo.");
                nazivBanke = Console.ReadLine();
            }
            nazivBanke = nazivBanke.ToLower();

            Console.WriteLine("Unesite Vaše ime:");

            String ime = Console.ReadLine();

            Console.WriteLine("Unesite Vaš jmbg:");

            String jmbg = Console.ReadLine();

            Console.WriteLine("Unesite Vaš mesečni prihod:");

            String p = Console.ReadLine();
            float prihod = float.Parse(p);

            Console.WriteLine("Unesite Vaše godine radnog staža:");

            String g = Console.ReadLine();
            int godine = int.Parse(g);

            Console.WriteLine("Unesite iznos kredita koji želite da dobijete:");

            String i = Console.ReadLine();
            int iznos = int.Parse(i);

            Console.WriteLine("Unesite na koliko mesečnih rata želite da dobijete kredit:");

            String b = Console.ReadLine();
            int brojRata = int.Parse(b);

            Klijent klijent = new Klijent(ime, jmbg, prihod, godine);
            Aplikacija aplikacija = new Aplikacija(nazivBanke, klijent, iznos, brojRata, "ne");

            int odgovor = 0;

            if (nazivBanke.ToLower() == "rbc")
            {
                RBC rbc = new RBC();
                odgovor = rbc.obradaZahteva(aplikacija);
                if (odgovor == 1)
                {
                    aplikacija.setOdobreno("da");
                    Console.WriteLine("Vaš zahtev za kredit u banci RBC je odobren.");
                }
                else
                    Console.WriteLine("Vaš zahtev za kredit u banci RBC nije odobren.");
            }
            else if (nazivBanke.ToLower() == "santander")
            {
                Santander santander = new Santander();
                odgovor = santander.obradaZahteva(aplikacija);
                if (odgovor == 1)
                {
                    aplikacija.setOdobreno("da");
                    Console.WriteLine("Vaš zahtev za kredit u banci Santander je odobren.");
                }
                else
                    Console.WriteLine("Vaš zahtev za kredit u banci Santander nije odobren.");
            }
            else if (nazivBanke.ToLower() == "wells fargo")
            {
                Wells_Fargo wells = new Wells_Fargo();
                odgovor = wells.obradaZahteva(aplikacija);
                if (odgovor == 1)
                {
                    aplikacija.setOdobreno("da");
                    Console.WriteLine("Vaš zahtev za kredit u banci Wells Fargo je odobren.");
                }
                else
                    Console.WriteLine("Vaš zahtev za kredit u banci Wells Fargo nije odobren.");
            }

            dodajAplikaciju(aplikacija,"aplikacije.csv");

        }



        public static void dodajAplikaciju(Aplikacija a, String nazivFajla)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(nazivFajla, true))
                {
                    file.WriteLine(a.getNazivBanke() + "," +
                                   a.getKlijent().getIme() + "," +
                                   a.getKlijent().getJmbg() + "," +
                                   a.getKlijent().getMesecnaPrimanja() + "," +
                                   a.getKlijent().getGodineRadnogStaza() + "," +
                                   a.getIznosKredita() + "," +
                                   a.getBrojMesecnihRata() + "," +
                                   a.getOdobreno());
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Desila se greška pri dodavanju aplikacije.", ex);
            }
        }

        public List<Aplikacija> ucitajAplikacije()
        {
            List<Aplikacija> aplikacije = new List<Aplikacija>();

            try 
            {
                using (var reader = new StreamReader("aplikacije.csv"))
                {
                    //var line = reader.ReadLine();
                    aplikacije = new List<Aplikacija>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(",");

                        Klijent klijent = new Klijent(values[1], values[2], float.Parse(values[3]), int.Parse(values[4]));

                        Aplikacija aplikacija = new Aplikacija(values[0], klijent, float.Parse(values[5]), int.Parse(values[6]), values[7]);

                        aplikacije.Add(aplikacija);
                    }
                }
            }
            catch
            {
                
            }
            

            return aplikacije;
        }

        public void prikazInformacija()
        {
            Console.WriteLine("PRIKAZ INFORMACIJA O STANJU KREDITA U BANCI\n");
            Console.WriteLine("Unesite Vaš JMBG:");
            String jmbg = Console.ReadLine();
            Console.WriteLine("Unesite naziv banke:");
            String nazivBanke = Console.ReadLine();

            while (nazivBanke.ToLower() != "rbc" && nazivBanke.ToLower() != "santander" && nazivBanke.ToLower() != "wells fargo")
            {
                Console.WriteLine("Pogrešan naziv banke, unesite naziv banke ponovo.");
                nazivBanke = Console.ReadLine();
            }

            prikazInformacija2(jmbg, nazivBanke);

            
        }

        public void prikazInformacija2(String jmbg, String nazivBanke)
        {
            List<Aplikacija> aplikacije = ucitajAplikacije();

            if (aplikacije.Count == 0)
            {
                Console.WriteLine("Trenutno nemate kredit ni u jednoj banci.\n");
            }

            int flg = 0;

            for (int i = 0; i < aplikacije.Count; i++)
            {
                if (aplikacije[i].getKlijent().getJmbg() == jmbg && aplikacije[i].getNazivBanke() == nazivBanke && aplikacije[i].getOdobreno()=="da")
                {
                    Console.WriteLine("U banci " + nazivBanke + " Vam je preostalo " + aplikacije[i].getBrojMesecnihRata() +
                                        " rata za plaćanje, u iznosu od " + aplikacije[i].getIznosKredita() / aplikacije[i].getBrojMesecnihRata());
                    flg = 1;
                }
            }

            if (flg == 0)
            {
                Console.WriteLine("Trenutno nemate kredit u banci " + nazivBanke + ".");
            }
        }

        public void placanjeRate()
        {
            Console.WriteLine("PLAĆANJE RATE KREDITA U BANCI\n");
            Console.WriteLine("Unesite Vaš JMBG:");
            String jmbg = Console.ReadLine();
            Console.WriteLine("Unesite naziv banke:");
            String nazivBanke = Console.ReadLine();

            while (nazivBanke.ToLower() != "rbc" && nazivBanke.ToLower() != "santander" && nazivBanke.ToLower() != "wells fargo")
            {
                Console.WriteLine("Pogrešan naziv banke, unesite naziv banke ponovo.");
                nazivBanke = Console.ReadLine();
            }

            List<Aplikacija> aplikacije = ucitajAplikacije();

            if (aplikacije.Count == 0)
            {
                Console.WriteLine("Trenutno nemate kredit ni u jednoj banci.\n");
            }

            int flg = 0;
            int flgRBC = 0;
            int flgS = 0;
            int flgW = 0;

            for (int i = 0; i < aplikacije.Count; i++)
            {
                if (aplikacije[i].getKlijent().getJmbg() == jmbg && aplikacije[i].getNazivBanke() == nazivBanke && aplikacije[i].getOdobreno()=="da")
                {
                    flg++;
                    if (nazivBanke == "rbc")
                        flgRBC++;
                    if (nazivBanke == "santander")
                        flgS++;
                    if (nazivBanke == "wells fargo")
                        flgW++;
                }
            }

            if (flg == 0)
            {
                Console.WriteLine("Trenutno nemate kredit u banci " + nazivBanke + ".");
            }

            if((flgRBC>0 && flgS>0) || (flgRBC>0 && flgW>0) || (flgS>0 && flgW>0) || (flgRBC>0 && flgS>0 && flgW>0))
            {
                String odgovor;

                Console.WriteLine("Izaberite u kojoj banci želite da platite ratu:");

                if(flgRBC>0)
                    Console.WriteLine("RBC, unesite broj 1");
                if(flgS>0)
                    Console.WriteLine("Santander, unesite broj 2");
                if(flgW>0)
                    Console.WriteLine("Wells Fargo, unesite broj 3");

                while (true)
                {
                    odgovor = Console.ReadLine();

                    if (flgRBC > 0 && flgS > 0 && flgW == 0 && odgovor != "1" && odgovor != "2")
                    {
                        Console.WriteLine("Neispravno unet odgovor, pokušajte ponovo!");
                    }
                    if (flgRBC > 0 && flgS > 0 && flgW == 0 && odgovor != "1")
                    {
                        platiRatu(jmbg, "rbc", flgRBC);
                        break;
                    }
                    if (flgRBC > 0 && flgS > 0 && flgW == 0 && odgovor != "2")
                    {
                        platiRatu(jmbg, "santander", flgS);
                        break;
                    }

                    if (flgRBC > 0 && flgS == 0 && flgW > 0 && odgovor != "1" && odgovor != "3")
                    {
                        Console.WriteLine("Neispravno unet odgovor, pokušajte ponovo!");
                    }
                    if (flgRBC > 0 && flgS == 0 && flgW > 0 && odgovor != "1")
                    {
                        platiRatu(jmbg, "rbc", flgRBC);
                        break;
                    }
                    if (flgRBC > 0 && flgS == 0 && flgW > 0 && odgovor != "3")
                    {
                        platiRatu(jmbg, "wells fargo", flgW);
                        break;
                    }

                    if (flgRBC == 0 && flgS > 0 && flgW > 0 && odgovor != "2" && odgovor != "3")
                    {
                        Console.WriteLine("Neispravno unet odgovor, pokušajte ponovo!");
                    }
                    if (flgRBC == 0 && flgS > 0 && flgW > 0 && odgovor != "2")
                    {
                        platiRatu(jmbg, "santander", flgS);
                        break;
                    }
                    if (flgRBC == 0 && flgS > 0 && flgW > 0 && odgovor != "3")
                    {
                        platiRatu(jmbg, "wells fargo", flgW);
                        break;
                    }

                    if (flgRBC > 0 && flgS > 0 && flgW > 0 && odgovor != "1" && odgovor != "2" && odgovor != "3")
                    {
                        Console.WriteLine("Neispravno unet odgovor, pokušajte ponovo!");
                    }
                    if (flgRBC > 0 && flgS > 0 && flgW > 0 && odgovor != "1")
                    {
                        platiRatu(jmbg, "rbc", flgRBC);
                        break;
                    }
                    if (flgRBC > 0 && flgS > 0 && flgW > 0 && odgovor != "2")
                    {
                        platiRatu(jmbg, "santander", flgS);
                        break;
                    }
                    if (flgRBC > 0 && flgS > 0 && flgW > 0 && odgovor != "3")
                    {
                        platiRatu(jmbg, "wells fargo", flgW);
                        break;
                    }
                }
            }
            else if(flg>1)
            {
                if (flgRBC > 1 && flgS == 0 && flgW == 0)
                {
                    platiRatu(jmbg, "rbc", flgRBC);
                }
                if (flgRBC == 0 && flgS > 1 && flgW == 0)
                {
                    platiRatu(jmbg, "santander", flgS);
                }
                if (flgRBC == 0 && flgS == 0 && flgW > 1)
                {
                    platiRatu(jmbg, "wells fargo", flgW);
                }
            }
            else if(flg==1)
            {
                if (flgRBC == 1)
                    platiRatu(jmbg, "rbc", 1);
                if (flgS == 1)
                    platiRatu(jmbg, "santander", 1);
                if (flgW == 1)
                    platiRatu(jmbg, "wells fargo", 1);
            }

        }

        public Aplikacija odabirKredita(String jmbg, String nazivBanke)
        {
            List<Aplikacija> aplikacije = ucitajAplikacije();
            List<Aplikacija> kreditiZaPlacanje = new List<Aplikacija>();
            Aplikacija kredit;

            int flg = 0;

            for (int i = 0; i < aplikacije.Count; i++)
            {
                if (aplikacije[i].getKlijent().getJmbg() == jmbg && aplikacije[i].getNazivBanke() == nazivBanke && aplikacije[i].getOdobreno()=="da")
                {
                    flg ++;
                    Console.WriteLine("Kredit " + flg + ": Preostalo Vam je " + aplikacije[i].getBrojMesecnihRata() +
                                        " rata za plaćanje, u iznosu od " + aplikacije[i].getIznosKredita() / aplikacije[i].getBrojMesecnihRata() + 
                                        ", unesi broj " + flg + " za plaćanje rate ovog kredita");
                    kreditiZaPlacanje.Add(aplikacije[i]);
                }
            }

            while(true)
            {
                String izbor = Console.ReadLine();
                int izbor2 = int.Parse(izbor) -1;

                for(int i=0;i<kreditiZaPlacanje.Count;i++)
                {
                    if(i==izbor2)
                    {
                        return kreditiZaPlacanje[i];
                    }
                }
                Console.WriteLine("Neispravno unet izbor, pokušajte ponovo!");
            }


        }

        public void platiRatu(string jmbg, string nazivBanke, int brojKreditaUBanci)
        {
            if (brojKreditaUBanci > 1)
            {
                Console.WriteLine("Izaberite za koji kredit želite da platite ratu:");
                Aplikacija kreditZaPlacanje = odabirKredita(jmbg, nazivBanke);

                String tempFile = "temp.csv";
                try
                {
                    using (var reader = new StreamReader("aplikacije.csv"))
                    {
                        //var line = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(",");

                            Klijent klijent = new Klijent(values[1], values[2], float.Parse(values[3]), int.Parse(values[4]));

                            Aplikacija aplikacija = new Aplikacija(values[0], klijent, float.Parse(values[5]), int.Parse(values[6]), values[7]);


                            if (aplikacija.getNazivBanke() == kreditZaPlacanje.getNazivBanke() && aplikacija.getKlijent().getJmbg() == kreditZaPlacanje.getKlijent().getJmbg() &&
                                aplikacija.getIznosKredita()==kreditZaPlacanje.getIznosKredita() && aplikacija.getBrojMesecnihRata()==kreditZaPlacanje.getBrojMesecnihRata() && aplikacija.getOdobreno() == "da")
                            {
                                aplikacija.setIznosKredita(aplikacija.getIznosKredita() - aplikacija.getMesecnaRata());
                                aplikacija.setBrojMesecnihRata(aplikacija.getBrojMesecnihRata() - 1);

                                if (aplikacija.getBrojMesecnihRata() == 0)
                                {
                                    aplikacija.setOdobreno("ne");
                                }
                            }
                            dodajAplikaciju(aplikacija, tempFile);

                        }
                    }

                    File.Delete("aplikacije.csv");

                    System.IO.File.Move(tempFile, "aplikacije.csv");

                    Console.WriteLine("Uspešno ste platili ratu!");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Desila se greška pri plaćanju rate.", ex);
                }
            }

            else if(brojKreditaUBanci==1)
            {
                String tempFile = "temp.csv";
                try
                {
                    using (var reader = new StreamReader("aplikacije.csv"))
                    {
                        //var line = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(",");

                            Klijent klijent = new Klijent(values[1], values[2], float.Parse(values[3]), int.Parse(values[4]));

                            Aplikacija aplikacija = new Aplikacija(values[0], klijent, float.Parse(values[5]), int.Parse(values[6]), values[7]);

                            if(aplikacija.getNazivBanke()==nazivBanke && aplikacija.getKlijent().getJmbg()==jmbg && aplikacija.getOdobreno()=="da")
                            {
                                aplikacija.setIznosKredita(aplikacija.getIznosKredita()-aplikacija.getMesecnaRata());
                                aplikacija.setBrojMesecnihRata(aplikacija.getBrojMesecnihRata() - 1);
                                
                                if(aplikacija.getBrojMesecnihRata()==0)
                                {
                                    aplikacija.setOdobreno("ne");
                                }

                            }
                            
                            dodajAplikaciju(aplikacija, tempFile);
                            
                        }
                    }

                    File.Delete("aplikacije.csv");

                    System.IO.File.Move(tempFile, "aplikacije.csv");

                    Console.WriteLine("Uspešno ste platili ratu!");
                }
                catch(Exception ex)
                {
                    throw new ApplicationException("Desila se greška pri plaćanju rate.", ex);
                }
            }
            else
            {
                Console.WriteLine("Poštovani nemate kredit u banci {0}.", nazivBanke);
            }

        }

        public void unosPodataka()
        {
            Console.WriteLine("Unesi putanju do fajla sa podacima:");

            String path = Console.ReadLine();

            String tempFile = "temp.csv";


            try
            {
                using (var reader = new StreamReader("aplikacije.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(",");

                        Klijent klijent = new Klijent(values[1], values[2], float.Parse(values[3]), int.Parse(values[4]));

                        Aplikacija aplikacija = new Aplikacija(values[0], klijent, float.Parse(values[5]), int.Parse(values[6]), values[7]);

                        dodajAplikaciju(aplikacija, tempFile);

                    }
                }

                using (var reader = new StreamReader(@path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(",");

                        Klijent klijent = new Klijent(values[1], values[2], float.Parse(values[3]), int.Parse(values[4]));

                        Aplikacija aplikacija = new Aplikacija(values[0], klijent, float.Parse(values[5]), int.Parse(values[6]), values[7]);

                        dodajAplikaciju(aplikacija, tempFile);
                    }
                }

                Console.WriteLine("Uspešno uneti podaci iz fajla {0}.", path);
                File.Delete("aplikacije.csv");
                File.Delete(path);
                System.IO.File.Move(tempFile, "aplikacije.csv");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Desila se greška pri plaćanju rate.", ex);
            }

        }
    }
}
