using System;
using System.Linq;
using System.Collections.Generic;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace MamicnKalkulator
{
    class Program
    {
        static readonly string[] Scopes = { CalendarService.Scope.Calendar };
        static readonly string ApplicationName = "DojencKulator";
        

        static void Main()
        {
            string ime = null;
            DateTime Rojstvo = new(), Izpust = new();

            while (true) //pridobi podatke o dojenčku
            {
                Console.WriteLine("\x1b[1;0m Doborodošli v DojenčKulator!\n\n");

                try
                {
                    Console.WriteLine("Prosim izpolni spodnje podatke:");
                    Console.Write("Ime in Priimek: ");
                    ime = Console.ReadLine();
                    Console.Write("Datum rojstva <dd.mm.yyyy>: ");
                    Rojstvo = DateTime.Parse(Console.ReadLine()); 
                    Console.Write("Datum izpusta <dd.mm.yyyy>: ");
                    Izpust = DateTime.Parse(Console.ReadLine());
                    
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Eden od vnešenih podatkov ni bil v skladu z pravili.");
                    Console.WriteLine("Bodi pozoren pri zapisu datuma");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ReadKey();

                    Console.Clear();

                    //Start process, friendly name is something like MyApp.exe (from current bin directory)
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.FriendlyName);

                    //Close the current process
                    Environment.Exit(0);

                }

                Console.Clear();

                Console.WriteLine("Vnešeni podatki: ");
                Console.WriteLine($"Ime: \x1b[1;31m {ime}\x1b[1;0m");
                Console.WriteLine($"Datum rojstva: \x1b[1;31m{Rojstvo:d}\x1b[1;0m");
                Console.WriteLine($"Datum izpusta: \x1b[1;31m{Izpust:d}\x1b[1;0m");

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

            List<Obisk> Obiski = new();

            for (int i = 1; i <= 6; i++)
            {  
                var obisk = new Obisk()
                {
                    N = i,
                };

                switch (i)
                {
                    case 1:
                        obisk.Opis = "V 24 urah po odpustu, tudi če je dela prost dan.";
                        obisk.Datum = Izpust.AddDays(1);
                        break;
                    case 2:
                        obisk.Opis = "Prvi do drugi teden po odpustu.";
                        obisk.Datum = UnWeekend(Izpust.AddDays(7)); 
                        break;
                    case 3:
                        obisk.Opis = "Drugi teden po odpustu.";
                        obisk.Datum = UnWeekend(Izpust.AddDays(14));
                        break; 
                    case 4:
                        obisk.Opis = "Tretji teden po odpustu.";
                        obisk.Datum = UnWeekend(Izpust.AddDays(21));
                        break;
                    case 5:
                        obisk.Opis = $"V starosti od 4 do 5 mesecev. Zadnji čas za ta obisk: {UnWeekend(Izpust.AddMonths(5)):d}";
                        obisk.Datum = UnWeekend(Izpust.AddMonths(4));
                        break;
                    case 6:
                        obisk.Opis = $"V starosti od 10 do 11 mesecev. Zadnji čas za ta obisk: {UnWeekend(Izpust.AddMonths(11)):d}";
                        obisk.Datum = UnWeekend(Izpust.AddMonths(10));
                        break;
                    
                } //assign values

                Obiski.Add(obisk);
            }

            string[] results =
            {
                " DojenčKulator rezultati:",
                "--------------------------",
                "",
                " Novorojenček: " + ime,
                " Rojen: " + Rojstvo.ToString("dd.MM.yyyy"),
                "",
                "--------------------------",
                " Obiski novorojenčka: ",
                $"{Obiski[0].N}. obisk: \x1b[1;35m{Obiski[0].Datum:d}\x1b[1;0m {Obiski[0].Opis}",
                $"{Obiski[1].N}. obisk: \x1b[1;35m{Obiski[1].Datum:d}\x1b[1;0m {Obiski[1].Opis}",
                $"{Obiski[2].N}. obisk: \x1b[1;35m{Obiski[2].Datum:d}\x1b[1;0m {Obiski[2].Opis}",
                $"{Obiski[3].N}. obisk: \x1b[1;35m{Obiski[3].Datum:d}\x1b[1;0m {Obiski[3].Opis}",
                $"{Obiski[4].N}. obisk: \x1b[1;35m{Obiski[4].Datum:d}\x1b[1;0m {Obiski[4].Opis}",
                $"{Obiski[5].N}. obisk: \x1b[1;35m{Obiski[5].Datum:d}\x1b[1;0m {Obiski[5].Opis}",                "",
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

            string[] fancyResults =
            {
                " DojenčKulator rezultati:",
                "--------------------------",
                "",
                " Novorojenček: " + ime,
                " Rojen: " + Rojstvo.ToString("dd.MM.yyyy"),
                "",
                "--------------------------",
                " Obiski novorojenčka: ",
                $"{Obiski[0].N}. obisk: {Obiski[0].Datum:d} {Obiski[0].Opis}",
                $"{Obiski[1].N}. obisk: {Obiski[1].Datum:d} {Obiski[1].Opis}",
                $"{Obiski[2].N}. obisk: {Obiski[2].Datum:d} {Obiski[2].Opis}",
                $"{Obiski[3].N}. obisk: {Obiski[3].Datum:d} {Obiski[3].Opis}",
                $"{Obiski[4].N}. obisk: {Obiski[4].Datum:d} {Obiski[4].Opis}",
                $"{Obiski[5].N}. obisk: {Obiski[5].Datum:d} {Obiski[5].Opis}",                "",
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

            foreach (string o in results)
            {
                Console.WriteLine(o);
            }

            Console.WriteLine("Ali želiš da shranim zapiske v računalnik? [Y/N]");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {

                string pathString = Path.Combine(@"c:\DojencKulator", Rojstvo.Year.ToString());

                Directory.CreateDirectory(pathString);

                pathString = Path.Combine(pathString, ime + +".txt");

                // Verify the path that you have constructed.
                Console.WriteLine("Pot do shranjene dotateke: {0}\n", pathString);

                if (File.Exists(pathString))
                {
                    int i = 1;
                    while (File.Exists(pathString))
                    {
                        pathString = Path.Combine(pathString, ime, " " + i.ToString() + ".txt");
                        i++;
                    }           
                }

                File.WriteAllLinesAsync(pathString, results);
            }

            Console.WriteLine("Ali želiš da shranim datume obiskov v tvoj koledar? [Y/N]");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Write("Prosim vpiši ime v tožilniku (Ana => Ane, Janez => Janeza): ");
                string ime2 = Console.ReadLine();
                WriteDates(Obiski, ime, ime2, Rojstvo);
            }
            else
            {
                Console.WriteLine("Okej, lep dan še naprej!");
            }

            DateTime UnWeekend(DateTime obisk)
            {
                if (obisk.DayOfWeek == DayOfWeek.Saturday)
                    return obisk.AddDays(2);
                else if (obisk.DayOfWeek == DayOfWeek.Sunday)
                    return obisk.AddDays(1);
                else
                    return obisk;
            }
        }

        static public void WriteDates(List<Obisk> obiski, string ime, string ime2, DateTime Rojstvo)
        {
            UserCredential credential;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nPoteka prijava z Google računom...");

            //get credentials
            using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            Console.WriteLine("Iščem koledar...");

            List<CalendarListEntry> lst = service.CalendarList.List().Execute().Items.ToList();

            CalendarListEntry kol = lst.FirstOrDefault(o => o.Summary == "Dojenčki");
            if (kol == null)
            {
                Console.WriteLine("Koledar ni bil najden. Ustvarjam novega...");
                Calendar cal = new()
                {
                    Summary = "Dojenčki",
                    Description = "Datumi obiskov novorojenčkov"
                };
                service.Calendars.Insert(cal).Execute();
                Console.WriteLine("Koledar ustvarjen!");
                lst = service.CalendarList.List().Execute().Items.ToList();
                kol = lst.FirstOrDefault(o => o.Summary == "Dojenčki");
            }
            string calendarId = kol.Id;


            Console.WriteLine("Ustvarjam dogodke...\n");

            foreach (Obisk o in obiski)
            {
                var ev = new Event();
                EventDateTime start = new();
                start.DateTime = o.Datum;
                EventDateTime end = new();
                end.DateTime = o.Datum.AddHours(24);

                ev.Start = start;
                ev.End = end;
                ev.Summary = $"{o.N}. Obisk {ime2}";
                ev.Description = $"{ime} Rojen {Rojstvo:d}\nTa obisk je načetovan {o.Opis}";

                service.Events.Insert(ev, calendarId).Execute();
                Console.WriteLine($"{o.N}. obisk ustvarjen!");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nUstvarjanje dogodkov je zaključeno. Lep dan še naprej!");
        }
    }

    public struct Obisk
    {
        /// <summary>
        /// Številka obiska
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Datum obiska
        /// </summary>
        public DateTime Datum { get; set; }
        /// <summary>
        /// Opis obiska
        /// </summary>
        public string Opis { get; set; }

    }
}