using System;
using System.IO;
using System.Threading.Tasks;

namespace DojencKulator
{
    class Program
    {

        static void Main(string[] args)
        {
            int leto, mesec, dan;
            int letoizp, mesecizp, danizp;
            string ime, datum, datumizp;
            DateTime Rojstvo;
            DateTime Izpust;

            String[] separator = { ".", "," };

            while (true)
            {
                Console.WriteLine("Doborodošli v DojenčKulator V1.1!");
                Console.WriteLine();
                Console.WriteLine("Prosim izpolni spodnje podatke:");
                Console.Write("Priimek in ime: ");
                ime = Console.ReadLine();

                //reads date of birth
                Console.Write("\nFormat: DD.MM.LLLL\n -Datum rojstva: ");
                datum = Console.ReadLine();
                Console.Write(" -Datum izpusta: ");
                datumizp = Console.ReadLine();

                //separates dates into 3 strings in array
                String[] datumSplit = datum.Split(separator, 3, StringSplitOptions.RemoveEmptyEntries);
                String[] datumSplitizp = datumizp.Split(separator, 3, StringSplitOptions.RemoveEmptyEntries);

                //assigns strings from array to variables
                dan = Int32.Parse(datumSplit[0]);
                mesec = Int32.Parse(datumSplit[1]);
                leto = Int32.Parse(datumSplit[2]);
                danizp = Int32.Parse(datumSplitizp[0]);
                mesecizp = Int32.Parse(datumSplitizp[1]);
                letoizp = Int32.Parse(datumSplitizp[2]);


                Rojstvo = new DateTime(leto, mesec, dan);
                Izpust = new DateTime(letoizp, mesecizp, danizp);

                try
                {
                    Rojstvo = new DateTime(leto, mesec, dan);
                    Izpust = new DateTime(letoizp, mesecizp, danizp);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Eden od vnešenih podatkov ni bil v skladu z pravili.");
                    Console.WriteLine("Bodi pozoren pri zapisu datuma");
                    Console.WriteLine("\n\n\n\n\n");

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
                Console.WriteLine("Datum izpusta: " + Izpust.ToString("dd.MM.yyyy"));

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

            String[] results =
            {
                " DojenčKulator rezultati:",
                "--------------------------",
                "",
                " Novorojenček: " + ime,
                " Rojen: " + Rojstvo.ToString("dd.MM.yyyy"),
                "",
                "--------------------------",
                " Obiski novorojenčka: ",
                " 1. Obisk: " + Izpust.AddDays(1).ToString("dd.MM.yyyy") + " V 24 urah po odpustu, tudi če je dela prost dan.",
                " 2. Obisk: " + Izpust.AddDays(7).ToString("dd.MM.yyyy") + Izpust.AddDays(14).ToString("dd.MM.yyyy") + " Prvi do drugi teden po odpustu.",
                " 3. Obisk: " + Izpust.AddDays(14).ToString("dd.MM.yyyy") + " Drugi teden po odpustu.",
                " 4. Obisk: " + Izpust.AddDays(21).ToString("dd.MM.yyyy") + " Tretji teden po odpustu.",
                " 5. Obisk: od " + Rojstvo.AddMonths(4).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddMonths(5).ToString("dd.MM.yyyy") + " V starosti od 4 do 5 mesecev.",
                " 6. Obisk: od " + Rojstvo.AddMonths(10).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddMonths(11).ToString("dd.MM.yyyy") + " V starosti od 10 do 11 mesecev.",
                "",
                "--------------------------",
                "",
                " Življenske stopnje:",
                " Novorojenček: " + Rojstvo.ToString("dd.MM.yyyy") + " do " + Rojstvo.AddDays(27).ToString("dd.MM.yyyy") + " - 0-28 dni",
                " Dojenček:     " + Rojstvo.AddDays(28).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddYears(1).ToString("dd.MM.yyyy") + " - 29 dni - 1 leta",
                " Malček:       " + Rojstvo.AddYears(1).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddYears(3).AddMonths(11).AddDays(29).ToString("dd.MM.yyyy") + " - od 1 leta do 3.9 leta",
                "",
                "--------------------------",
                "",
                " Obiski otročnice",
                " 1. Obisk: " + Izpust.AddDays(3).ToString("dd.MM.yyyy") + " do " + Izpust.AddDays(7).ToString("dd.MM.yyyy") + (" 3-7 dni"),
                " 2. Obisk: " + Rojstvo.AddDays(28).ToString("dd.MM.yyyy") + " do " + Rojstvo.AddDays(42).ToString("dd.MM.yyyy") + (" 4-6 tednov"),
                "",
                "",
                "Powered by DojencKulator"
               
            };

            foreach(string s in results)
            {
                Console.WriteLine(s);
            }
            
            Console.WriteLine("--------------------------");
            Console.WriteLine("\r\n\r\n\r\n\r\n\r\n\r\n");
            Console.WriteLine("Hvala ker uporabljate DojenčKulator!");
            Console.WriteLine("Klikni <enter> za izhod");

            // Specify a name for your top-level folder.
            string folderName = @"c:\DojencKulator";

            // To create a string that specifies the path to a subfolder under your
            // top-level folder, add a name for the subfolder to folderName.
            string pathString = System.IO.Path.Combine(folderName, leto.ToString());

            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create.
            string fileName = ime + ".txt";

            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Pot do shranjene dotateke: {0}\n", pathString);

            if (!System.IO.File.Exists(pathString))
            {
                File.WriteAllLinesAsync(pathString, results);
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            
            }  
        }


    }
}
