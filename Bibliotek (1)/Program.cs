using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bibliotek__1_
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<Böcker> AllaBöcker = new List<Böcker>();

            List<LånadeBöcker> LånadeBöcker = new List<LånadeBöcker>();

            bool valt = false;

            while (!valt)
            {
                 Console.WriteLine("(1) Lista alla böcker.");
                Console.Write("(2) Låna bok.\n");
                Console.Write("(3) Lämna tillbacka bok. \n");
                Console.Write("(4) Lägg till ny bok.\n");
                Console.Write("(5) Ta bort bok.\n");
                Console.Write("(6) Lista alla Lånade böcker.\n");
                Console.Write("(7) Sök efter bok.\n");
                Console.Write("(8) Redigera bok\n");
                Console.Write("(9) Avsluta pogramet (spara)\n");
                Console.Write("(10) Avsluta pogramet (utan att spara)\n");

                int alternativ = HeltalCheck("Välj ett av alternativen ovan");

                switch (alternativ)
                {
                    case 1:
                        ListaAlla(); valt = true; break;
                    case 2:
                        lånabook(); valt = true; break;
                    case 3:
                        lämnaTillbacka(); valt = true; break;
                    case 4:
                        läggtill(); valt = true; break;
                    case 5:
                        taBort(); valt = true; break;
                    case 6:
                        listaLånade(); valt = true; break;
                    case 7:
                        sökEfter(); valt = true; break;
                    case 8:
                        redigera(); valt = true; break;
                    case 9:
                        avslutaSpara(); valt = true; break;
                    case 10:
                        avslutaUtanSpara(); valt = true; break;
                    default:
                        Console.WriteLine("Måste vara ett heltal mellan 1-10!!!"); valt = false; break;


                }
            }

        }


        //static void sktivInIFillBok()
        //{
        //    if (File.Exists("Böcker.txt"))
        //    {
        //        for (int i = 0; i < Böcker.count; i++)
        //        {
        //            using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true))
        //            {
        //                skrivfill.WriteLine($"{B.Författare}, {Böcker[i].BokNamn}");
        //            }
        //        }
        //    }
        //}



        //static void sktivInIFillBok()
        //{
        //    // Om filen inte finns, skapa den och skriv data
        //    using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", false))
        //    {
        //        foreach (var bok in AllaBöcker)
        //        {
        //            skrivfill.WriteLine($"{bok.Författare}, {bok.BokNamn}");
        //        }
        //    }
        //}

        static void sktivInIFillLånade()
        {
            if (File.Exists("LånadeBöcker.txt"))
            {

            }
        }

        static void ListaAlla()
        { }

        static void lånabook()
        {
        
        }

        static void lämnaTillbacka()
        { }

        static void läggtill()
        { }

        static void taBort()
        { }


        static void listaLånade()
        { }

        static void sökEfter()
        { }

        static void redigera()
        { }

        static void avslutaSpara()
        { }

        static void avslutaUtanSpara()
        { }



        static int HeltalCheck(string fråga)
        {
            int resultat;
            while (true)
            {
                Console.WriteLine(fråga);
                var input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out resultat) && resultat > 0)
                {

                    return resultat;
                }
                Console.WriteLine("Du måste ange en giltig siffra! (Ett heltal över 0)");
            }
        }


        static string testastring(string fråga)
        {
            string resultat;

            while (true)
            {
                Console.WriteLine(fråga);
                resultat = Console.ReadLine();

                if (string.IsNullOrEmpty(resultat))
                { Console.WriteLine("Du måste ange en giltig string"); }
                if (!string.IsNullOrEmpty(resultat))
                { return resultat; }

            }
        }




    }
}



