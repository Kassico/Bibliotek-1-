using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using Microsoft.SqlServer.Server;
using System.Xml.Linq;
using System.CodeDom;

namespace Bibliotek__1_
{
    internal class Program
    {
        static List<Böcker> AllaBöcker = new List<Böcker>();

        

        static void Main(string[] args)
        {


            LägginIListaBok();
        
            bool kör = true;
            while (kör)
            {
                LägginIListaBok();


                bool valt = false;

            while (!valt)
            {
                Console.WriteLine("\n(1) Lista alla böcker.");
                Console.Write("(2) Låna bok.\n");
                Console.Write("(3) Lämna tillbaka bok. \n");
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
                        lånaBok(); valt = true; break;
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
                        skrivfill.WriteLine($"bok1,föfatare1,false");
                    }
                }
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("The Hobbit,J.R.R.Tolkien,false");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("1984,George Orwell,false");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("Dune,Frank Herbert,false");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("The Catcher in the Rye,J.D.Salinger,true");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("Brave New World,Aldous Huxley,true");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("The Name of the Wind,Patrick Rothfuss,false");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("Neuromancer,William Gibson,true");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("Blood Meridian,Cormac McCarthy,true");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("The Lies of Locke Lamora,Scott Lynch,false");
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", true)) skrivfill.WriteLine("The Way of Kings,Brandon Sanderson,false");
            }



        }           //Skapar filen för Böcker om det inte fins en och lägger in några böcker

        static void LägginIListaBok()
        
           
    
        {
            if (AllaBöcker.Count==0)
            {
                if (File.Exists("Böcker.txt"))
            {
                try
                { 
                using (StreamReader läsfill = new StreamReader("Böcker.txt"))
                {
                    string rad;
                    while ((rad = läsfill.ReadLine()) != null)
                    {
                        string[] delar = rad.Split(',');
                        if (delar.Length == 3 && bool.TryParse(delar[2], out bool ärLånad))
                        {
                            
                            AllaBöcker.Add(new Böcker(delar[0], delar[1], ärLånad));
                            
                        }
                        else
                            {
                            Console.WriteLine($"Fel i filen på raden: {rad}. Skippas");
                            }
                    }
                }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett fell uppstod vid läsningen av filen: {ex}");
                }
            }
            else
            { skapafilBöker(); }
            }

        }         //Lägger in alla böcker i listan

        static void skrivInIFillBok()
        {
            

            if (File.Exists("Böcker.txt"))
            {
                using (StreamWriter skrivfill = new StreamWriter("Böcker.txt", false))
                    foreach (var bok in AllaBöcker)
                    {
                        skrivfill.WriteLine($"{bok.BokNamn},{bok.Författare},{bok.ÄrLånad}");
                    }
            }
            else
            {
                skapafilBöker();
            }
        }      //skriver in Böcker i filen

        static void ListaAlla()
        {
            

            Console.WriteLine("\nAlla böcker i biblioteket är:");
            int j = 0;
            for (int i = 0; i < AllaBöcker.Count; i++)
            {
               
            
                    j++;
                    Console.WriteLine($"{j}. {AllaBöcker[i].BokNamn}, {AllaBöcker[i].Författare}");
                


            }
            Console.WriteLine("\nAlla lånade böcker är: ");
                j = 0;
            for (int i = 0; i < AllaBöcker.Count; i++)
            {
                if (AllaBöcker[i].ÄrLånad)
                {
                    j++;
                    Console.WriteLine($"{j}. {AllaBöcker[i].BokNamn}, {AllaBöcker[i].Författare},     (Boken är lånad)");
                }
            }
        }


        static void lämnaTillbacka()
        {
            string boknamn = testastring("Vilken bok vill du lämna tillbacka? (Skriv bokens namn)");

            for (int i = 0; i < AllaBöcker.Count; i++)
            {
                if (AllaBöcker[i].BokNamn.ToLower() == boknamn.ToLower())
                {
                    AllaBöcker[i].ÄrLånad = false;
                    Console.WriteLine($"Boken {boknamn} är nu tillbacka lämnad");
                    skrivInIFillBok();
                    return;
                }
                else if (i == AllaBöcker.Count - 1)
                {
                    Console.WriteLine("Boken finns inte i biblioteket");
                }
            }


        }
        static void lånaBok()
        {
            bool finns = false;
            while (!finns)
            { 
                string boknamn = testastring("Vilken bok vill du låna ? (Skriv bokens namn)").ToLower();
                for (int i = 0; i < AllaBöcker.Count; i++)
                {
                    if (AllaBöcker[i].BokNamn.ToLower() == boknamn)
                    {
                        if (AllaBöcker[i].ÄrLånad)
                        {
                            Console.WriteLine("Boken är redan lånad");
                            while (!finns)
                            {
                                string svar = testastring("Vill du låna en annan bok? (ja/nej)").ToLower();
                                if (svar == "nej")
                                {
                                    Console.WriteLine("Du valde att inte låna någon bok");
                                    finns = true;
                                }
                                else if (svar == "ja")
                                {
                                    finns = false;
                                }
                                else
                                {
                                    Console.WriteLine("Svara med ja eller nej!!!!");
                                }
                            }
                        }
                        else if (!AllaBöcker[i].ÄrLånad)
                        {
                            AllaBöcker[i].ÄrLånad = true;
                            Console.WriteLine($"Du har lånat boken {boknamn}");
                            skrivInIFillBok();
                            finns = true;
                        }
                        else if (i == AllaBöcker.Count - 1)
                        {
                            Console.WriteLine("Boken finns inte i biblioteket");
                            finns = false;

                        }

                    }
                    
                }
            }
        }
        static void listaLånade()
        {


            for (int i = 0; AllaBöcker.Count > i; i ++)
            {
                if (AllaBöcker[i].ÄrLånad)
                {
                    Console.WriteLine($"{AllaBöcker[i].BokNamn}, {AllaBöcker[i].Författare}");
                }

                else if (i == 0)
                {
                    Console.WriteLine("Det finns inga lånade böcker");
                }
            }


        }



        static void läggtill() 
        {

            string boknamn = testastring("vad är namnet på boken?");


            string författare = testastring("vad är författaren till boken?");
 
            AllaBöcker.Add(new Böcker(boknamn, författare, false));
            skrivInIFillBok();
            

        }
        static void taBort() 
        {
            bool finns = false;
            int svar = HeltalCheck("vill du ta bort en med hjälp av titielen (1) eller ta bort alla böcker hos en författare (2) ");
            if (svar == 1)
            {
                string namnPåTaBortBok = testastring("Vilken bok vill du ta bort? (Skriv bokens namn)").ToLower();
                for (int i = AllaBöcker.Count-1 ; i >= 0; i--)
                { 
                   if (AllaBöcker[i].BokNamn.ToLower() == namnPåTaBortBok)
                    {
                        AllaBöcker.RemoveAt(i);
                        Console.WriteLine($"Boken med namnet {namnPåTaBortBok} är bort tagen");
                        skrivInIFillBok();
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
                
                string namnPÅFörfattare = testastring("Vilken bok vill du ta bort? (Skriv förfataren)").ToLower();
                    if (namnPÅFörfattare.ToLower() == "nej")
                {
                    Console.WriteLine("Du valde att inte ta bort någon bok");
                    return;
                }

                bool tarBortFörfatare = true;
                while (tarBortFörfatare)
                {
                    string svarString = testastring($"Vill du ta bort alla böcker med förfataren {namnPÅFörfattare}? (ja/nej)");

                    if (svarString.ToLower() == "ja")
                    {
                    for (int i = AllaBöcker.Count - 1; i >= 0; i--)
                    {
                        if (AllaBöcker[i].Författare.ToLower() == namnPÅFörfattare.ToLower())
                        {
                            AllaBöcker.RemoveAt(i);
                            
                                skrivInIFillBok();

                                if (i == 0)
                                {
                                    Console.WriteLine($"Alla böcker med förfatare {namnPÅFörfattare} är borttagna");
                                    tarBortFörfatare = false;
                                }

                            }
                    }
                    }
                    else if (svarString == "nej")
                    { 
                        Console.WriteLine("Du valde att inte ta bort någon bok");
                        tarBortFörfatare = false;
                    }
                    else
                    {
                        Console.WriteLine("Svar med ja eller nej!!!!");
                    }
                }


            }
            else { Console.WriteLine("SVARA MED 1 ELLER 2!!!!"); }

        }
        static void sökEfter() 
        {
            string boksvar = testastring("Vilken bok vill du söka efter? (Skriv bokens namn eller författare).").ToLower();
            Console.WriteLine($"\nAlla böcker med namnet {boksvar}: \n");
            int FinnsinteBok = 0;
            foreach (var bok in AllaBöcker)
            {
                
                if (bok.BokNamn.ToLower().Contains(boksvar))
                {
                    Console.WriteLine($"{bok.BokNamn}, {bok.Författare}");
                }
                else if (!bok.BokNamn.ToLower().Contains(boksvar))
                    {
                    FinnsinteBok++;
                    }           
            }
            if (FinnsinteBok == AllaBöcker.Count)
            {
                Console.WriteLine("Det finns ingen bok med det namnet");
            }
            Console.WriteLine("\nAlla böcker med författaren är: \n");
            int FinnsinteFörfattare = 0;
            foreach (var bok in AllaBöcker)
            {
                if (bok.Författare.ToLower().Contains(boksvar))
                {
                    Console.WriteLine($"{bok.BokNamn}, {bok.Författare}");
                }
                else if(!bok.Författare.ToLower().Contains(boksvar))
                {
                    FinnsinteFörfattare++;
                }
            }
            if (FinnsinteFörfattare == AllaBöcker.Count)
            {
                Console.WriteLine("Det finns ingen bok med den författaren");
            }




        }

        static void redigera()
        {
            bool finns = false;
            while (!finns)
            { 
            
            string svar = testastring("Vill du redigera boknamn eller författare?").ToLower();
            if (svar == "författare")
            {
                    string boknamn = testastring("Vilken bok vill du redigera? (Skriv bokens namn)");
                    foreach (var bok in AllaBöcker)
                {
                    if (bok.BokNamn.ToLower() == boknamn.ToLower())
                    {
                        string nyFörfattare = testastring("Vad är det nya författaren på boken?");
                        bok.Författare = nyFörfattare;
                        Console.WriteLine($"Boken {boknamn} är nu redigerad och författaren är nu {nyFörfattare}");
                        skrivInIFillBok();
                            finns = true;
                    }

                    }
                    if (finns == false)
                    {
                        Console.WriteLine("Boken finns inte i biblioteket");
                        while (!finns)
                        {

                            svar = testastring("Vill du redigera en annan bok? (ja/nej)").ToLower();

                            if (svar == "nej")
                            {
                                Console.WriteLine("Du valde att inte redigera någon bok");
                                finns = true;
                            }
                            else if (svar == "ja")
                            {
                                finns = false;
                                break;
                            }
                            else
                            {
                            }
                        }
                    }
                }
            else if (svar == "boknamn")
            {
                    string boknamn = testastring("Vilken bok vill du redigera? (Skriv bokens namn)");


                    foreach (var bok in AllaBöcker)
                {
                        if (bok.BokNamn.ToLower() == boknamn.ToLower())
                        {
                            boknamn = testastring("Vad är det nya titelen på boken?");
                            bok.BokNamn = boknamn;
                            Console.WriteLine($"Den nya titlen är nu {boknamn}");
                            skrivInIFillBok();
                            finns = true; break;
                        }
                    }
                        if (finns == false)
                    {
                        Console.WriteLine("Boken finns inte i biblioteket");
                        while (!finns)
                        {

                            svar = testastring("Vill du redigera en annan bok? (ja/nej)").ToLower();

                            if (svar == "nej")
                            {
                                Console.WriteLine("Du valde att inte redigera någon bok");
                                finns = true;
                            }
                            else if (svar == "ja")
                            {
                                finns = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Svara med ja eller nej!!!!");
                            }
                        }
                    }
                }

                else { Console.WriteLine("Svara med författare eller boknamn!!!!"); }
        }
        }
        static void avslutaSpara() 
        {
            Environment.Exit(0);
        }

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