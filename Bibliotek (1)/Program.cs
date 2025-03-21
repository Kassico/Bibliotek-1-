using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Bibliotek__1_
{
    internal class Program
    {
        static List<Böcker> AllaBöcker = new List<Böcker>();
        static List<LånadeBöcker> LånadeBöcker = new List<LånadeBöcker>();
        

        static void Main(string[] args)
        {

                LägginIListaBok();
                LägginIListaLånebok();


            bool kör = true;
            while (kör)
            {

                
                bool valt = false;

            while (!valt)
            {
                Console.WriteLine("\n(1) Lista alla böcker.");
                Console.Write("(2) Låna bok.\n");
                Console.Write("(3) Lämna tillbacka bok. \n");
                Console.Write("(4) Lägg till ny bok.\n");
                Console.Write("(5) Ta bort bok.\n");
                Console.Write("(6) Lista alla Lånade böcker.\n");
                Console.Write("(7) Sök efter bok.\n");
                Console.Write("(8) Redigera bok\n");
                Console.Write("(9) Avsluta pogramet\n");
                int alternativ = HeltalCheck("\nVälj ett av alternativen ovan\n");

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
                    
                    default:
                        Console.WriteLine("Måste vara ett heltal mellan 1-9!!!"); valt = false; break;
                }
                    //Thread.Sleep(1500);
                    //Console.Clear();
                    
                }
            }
        }

        static void skapafilBöker()
        {
            if (!File.Exists("Böcker.txt"))
            {
                File.Create("Böcker.txt").Close();

                for (int i = 0; i < AllaBöcker.Count; i++)
                {
                    using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true))
                    {
                        skrivfill.WriteLine($"bok1, föfatare1");
                    }
                }
            }

        }           //Skapar filen för Böcker om det inte fins en

        static void skapafilLåneBöker()
        {
            if (!File.Exists("LånadeBöcker.txt"))
            {
                File.Create("LånadeBöcker.txt").Close();
            }
        }       //Skapar filen för lånade böcker om det inte finns en

        static void LägginIListaBok()

        { if (File.Exists("Böcker.txt"))
            {
                using (StreamReader läsfill = new StreamReader("Böcker.txt"))
                {
                    string rad;
                    while ((rad = läsfill.ReadLine()) != null)
                    {
                        string[] delar = rad.Split(',');
                        if (delar.Length == 2)
                        { 
                            AllaBöcker.Add(new Böcker(delar[0], delar[1]));
                            
                        }
                    }
                }
            }
            else
            { skapafilBöker(); }

        }         //Lägger in alla böcker i listan

        static void LägginIListaLånebok()

        {
            if (File.Exists("LånadeBöcker.txt"))
            {
                using (StreamReader läsfill = new StreamReader("LånadeBöcker.txt"))
                {
                    string rad;
                    while ((rad = läsfill.ReadLine()) != null)
                    {
                        string[] delar = rad.Split(',');
                        LånadeBöcker.Add(new LånadeBöcker(delar[0], delar[1]));
                    }
                }
            }
            else
            { skapafilLåneBöker(); }

        }     //Lägger in alla lånade böcker i listan

        static void sktivInIFillBok()
        {


            if (File.Exists("Böcker.txt"))
            {
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", false))
                    for (int i = 0; i < AllaBöcker.Count; i++)
                    {
                        skrivfill.WriteLine($"{AllaBöcker[i].BokNamn}, {AllaBöcker[i].Författare}");
                    }
            }
            else
            {
                skapafilBöker();
            }
        }      //skriver in Böcker i filen


        static void sktivInIFillLånade()
        {
           
                if (File.Exists("LåneBöcker.txt"))
                {
                    for (int i = 0; i < LånadeBöcker.Count; i++)
                    {
                        using (StreamWriter skrivfill = new StreamWriter(("LåneBöcker.txt"), true))
                        {
                            skrivfill.WriteLine($"{LånadeBöcker[i].Författare}, {LånadeBöcker[i].BokNamn}");
                        }
                    }
                }
            else
            {
                skapafilLåneBöker();
            }

        }      //skriver in Lånade Böcker i filen

        static void ListaAlla()
        {
            

            Console.WriteLine("\nAlla böcker i biblioteket är: \n");

            for (int i = 0, j = 1; i < AllaBöcker.Count; i++, j++)
            {
                Console.WriteLine($"{j}. {AllaBöcker[i].BokNamn}, {AllaBöcker[i].Författare}");
            }   

        }
        static void lånabook()
        {

            

        }
        static void lämnaTillbacka() 
        {
        
        
        }
        static void läggtill() 
        {

            string boknamn = testastring("vad är namnet på boken?");


            string författare = testastring("vad är författaren till boken?");
 
            AllaBöcker.Add(new Böcker(boknamn, författare));
            sktivInIFillBok();
            

        }
        static void taBort() 
        {
            bool finns = false;
            int svar = HeltalCheck("vill du ta bort en bok med bokets namn (1) eller med Förfataren (2)");
            if (svar == 1)
            {
                string namnPåTaBortBok = testastring("Vilken bok vill du ta bort? (Skriv bokens namn)");
                for (int i = AllaBöcker.Count-1 ; i >= 0; i--)
                { 
                   if (AllaBöcker[i].BokNamn == namnPåTaBortBok)
                    {
                        AllaBöcker.RemoveAt(i);
                        Console.WriteLine($"Boken med namnet {namnPåTaBortBok} är bort tagen");
                        finns = true;
                        break;
                    }
                }
                if (!finns)
                {
                     Console.WriteLine("Finns inte någon bok med det namnet.");
                }
            }

            else if (svar == 2)
                {
                    string namnPÅFörfattare = testastring("Vilken bok vill du ta bort? (Skriv förfataren)");
                    if (AllaBöcker.Exists(x => x.Författare == namnPÅFörfattare))
                    {
                        AllaBöcker.RemoveAll(x => x.Författare == namnPÅFörfattare);
                        Console.WriteLine($"Boken med förfataren {namnPÅFörfattare} är bort tagen");
                    }
                    else { Console.WriteLine("Finns inge bok med den förfataren."); }
                }
             else { Console.WriteLine("SVARA MED 1 ELLER 2!!!!"); }
            


        }
        static void listaLånade() 
        {
        
        
        }
        static void sökEfter() 
        {

           
        }


        static void redigera() { }
        static void avslutaSpara() { }
        static void avslutaUtanSpara() { }

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
                {
                    Console.WriteLine("Du måste ange en giltig string");
                }
                else
                {
                    return resultat;
                }
            }
        }
    }           
}



