using System;

namespace Banka
{
    class Program
    {
        static void Main(string[] args)
        {
            ObradaZahteva obrada = new ObradaZahteva();

            while (true)
            {
                Console.WriteLine("Izaberite jednu od 4 opcije:\n");
                Console.WriteLine("1. Apliciranje za kredit, unesite broj 1");
                Console.WriteLine("2. Prikaz informacija o kreditima, unesite broj 2");
                Console.WriteLine("3. Plaćanje kredita, unesite broj 3");
                Console.WriteLine("4. Unos novih aplikacija iz fajla, unesite broj 4");
                Console.WriteLine("5. Izlaz iz aplikacije, unesite broj 5");

                string odgovor = Console.ReadLine();

                while(odgovor!="1" && odgovor!="2" && odgovor!="3" && odgovor!="4" && odgovor!="5")
                {
                    Console.WriteLine("Neispravno unet odgovor, pokušajte ponovo!");
                    odgovor = Console.ReadLine();
                }

                if(odgovor=="1")
                {
                    obrada.zahtevanjeKredita();
                }
                else if(odgovor=="2")
                {
                    obrada.prikazInformacija();
                }
                else if(odgovor=="3")
                {
                    obrada.placanjeRate();
                }
                else if(odgovor=="4")
                {
                    obrada.unosPodataka();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

    }
}
