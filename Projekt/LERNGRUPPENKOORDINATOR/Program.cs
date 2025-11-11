using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Lerngruppe.Anmelden;

namespace Lerngruppe
{


    public class Anmelden
    {
        public string Signedname { get; set; }
        public bool SignedIn { get; set; }

        public void AnmeldenMenuStart(bool SignedIn, string Signedname)
        {

            if (SignedIn == false)
            {
                Console.WriteLine("Zum Anmelden geben Sie Ihren Namen ein!");
                Console.WriteLine("Hier wird der Name in der Datenbank abgeglichen");
                Console.WriteLine("Für Testzwecke bitte nutze 'Max Mustermann'");
                string eingabename = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"WILLKOMMEN");
                Console.WriteLine($"╔══════════════════════╗");
                Console.WriteLine($"║{eingabename}         ║");
                Console.WriteLine($"╩══════════════════════╝");

                this.Signedname = eingabename;
                this.SignedIn = true;

            }
            else { Anmelden.AnmeldungMenuAnzeigen(Signedname); }
        }
        public static string AnmeldungMenuAnzeigen(string name)
        {
            string willkommenBox =
            $@"╔══════════╗
                ║ {name}     ║
                ╩══════════╝";
            return willkommenBox;
        }

        //-------------------------------------------------------------------------------
        public class Menu
        {
            public void MenuAnzeigen(string name)
            {
                Console.WriteLine("Was soll angezeigt werden?");
                Console.WriteLine("1. Meine Termine -> Zeigt dir deine Termine (Kurse in dennen du dich Angemeldet hast)");
                Console.WriteLine("2. Infos über mich -> Deine Nutzerdaten werden nun angezeigt");
                Console.WriteLine("3. Verfügbare Kurse -> Hier Kannst du dich in Kurse einschreiben");

                int x = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                switch (x)
                {
                    case 1:
                        {
                            string dummydatenTermine = "TERMINTABELLE.txt";
                            if (File.Exists(dummydatenTermine))
                            {
                                Console.WriteLine("=== DEINE TERMINE ===");
                                string[] zeilen = File.ReadAllLines(dummydatenTermine);
                                foreach (string p in zeilen)
                                {
                                    Console.WriteLine(p);

                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            string dummydatenNutzer = "NUTZERTABELLE.txt";
                            if (File.Exists(dummydatenNutzer))
                            {
                                Console.WriteLine("=== ÜBER DICH ===");
                                string[] zeilen = File.ReadAllLines(dummydatenNutzer);
                                foreach (string p in zeilen)
                                {
                                    Console.WriteLine(p);
                                }

                            }
                            break;
                        }

;
                    case 3:
                        {
                            string dummydatenKurse = "KURSETABELLE.txt";
                            if (File.Exists(dummydatenKurse))
                            {
                                Console.WriteLine("=== ÜBER DICH ===");
                                string[] zeilen = File.ReadAllLines(dummydatenKurse);
                                foreach (string p in zeilen)
                                {
                                    Console.WriteLine(p);

                                }

                            }
                            break;
                        }
                }

            }
        }
            public class Start
            {
                public static void Init()
                {
                    Console.WriteLine("Verbindung wird aufgebaut...");
                }
            }

        }
        class Program
        {

            static void Main(string[] args)
            {
                Menu menu = new Menu();
                Anmelden anmeldung1 = new Anmelden();
                bool Anmeldung = false;
                Start.Init();
                anmeldung1.AnmeldenMenuStart(false, "");
                Anmeldung = true;

                while (Anmeldung == true)
                {
                    anmeldung1.AnmeldenMenuStart(anmeldung1.SignedIn, $"{anmeldung1.Signedname}");
                    menu.MenuAnzeigen($"{anmeldung1.Signedname}");
                }
            }
        }
    }


