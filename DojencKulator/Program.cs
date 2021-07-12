using System;

namespace DojencKulator
{
    class Program
    {

        static void Main(string[] args)
        {
            int leto, mesec, dan;
            string ime;
            DateTime Rojstvo;

            while (true)
            {
                Console.WriteLine("Doborodošli v DojenčKulator V1.0!");
                Console.WriteLine();
                Console.WriteLine("Prosim izpolni spodnje podatke:");
                Console.Write("Ime in Priimek: ");
                ime = Console.ReadLine();
                Console.Write("Dan rojstva: ");
                dan = Int32.Parse(Console.ReadLine());
                Console.Write("Mesec rojstva: ");
                mesec = Int32.Parse(Console.ReadLine());
                Console.Write("Leto rojstva: ");
                leto = Int32.Parse(Console.ReadLine());

                Rojstvo = new DateTime(leto, mesec, dan);
                try
                {
                    Rojstvo = new DateTime(leto, mesec, dan);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Eden od vnešenih podatkov ni bil v skladu z pravili.");
                    Console.WriteLine("Bodi pozoren pri zapisu datuma");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        //Start process, friendly name is something like MyApp.exe (from current bin directory)
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);

                        //Close the current process
                        Environment.Exit(0);
                    }
                }

                Console.Clear();

                Console.WriteLine("Vnešeni podatki:");
                Console.WriteLine("Ime: " + ime);
                Console.WriteLine("Datum rojstva: " + Rojstvo.ToString("dd.MM.yyyy"));

                Console.WriteLine("Ali so vnešeni podatki pravilni? [Y/N]");

                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }

            Console.WriteLine(" DojenčKulator rezultati:");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine(" Novorojenček: " + ime);
            Console.WriteLine(" Rojen: " + Rojstvo.ToString("dd.MM.yyyy"));
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine(" Obiski novorojenčka: ");
            Console.WriteLine(" 1. Obisk: " + Rojstvo.AddDays(1).ToString("dd.MM.yyyy") + " V 24 urah po odpustu, tudi če je dela prost dan.");
            Console.WriteLine(" 2. Obisk: " + Rojstvo.AddDays(7).ToString("dd.MM.yyyy") + " Prvi do drugi teden po odpustu.");
            Console.WriteLine(" 3. Obisk: " + Rojstvo.AddDays(14).ToString("dd.MM.yyyy") + " Drugi teden po odpustu.");
            Console.WriteLine(" 4. Obisk: " + Rojstvo.AddDays(21).ToString("dd.MM.yyyy") + " Tretji teden po odpustu.");
            Console.WriteLine(" 5. Obisk: " + Rojstvo.AddMonths(4).AddDays(14).ToString("dd.MM.yyyy") + " V starosti od 4 do 5 mesecev.");
            Console.WriteLine(" 6. Obisk: " + Rojstvo.AddMonths(6).AddDays(14).ToString("dd.MM.yyyy") + " V starosti od 10 do 11 mesecev.");
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine(" Življenske stopnje:");
            Console.WriteLine(" Novorojenček: " + Rojstvo.ToString("dd.MM.yyyy") + " - 0-28dni");
            Console.WriteLine(" Dojenček:     " + Rojstvo.AddDays(29).ToString("dd.MM.yyyy" + " - 29dni - 1 leta"));
            Console.WriteLine(" Malček:       " + Rojstvo.AddYears(3).AddMonths(11).AddDays(29).ToString("dd.MM.yyyy") + " - od 1 leta do 3.9 leta");
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine(" Obiski otročnice");
            Console.WriteLine(" 1. Obisk: " + Rojstvo.AddDays(3).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddDays(6).ToString("dd.MM.yyyy") + (" 3-6 dni"));
            Console.WriteLine(" 2. Obisk: " + Rojstvo.AddDays(28).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddDays(42).ToString("dd.MM.yyyy") + (" 4-6 tednov"));
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine("\r\n\r\n\r\n\r\n\r\n\r\n");
            Console.WriteLine("Hvala ker uporabljate DojenčKulator!");
            Console.WriteLine("Klikni <enter> za izhod");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }

}
      