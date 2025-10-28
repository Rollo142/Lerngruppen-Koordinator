using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static Lerngruppe.Anmelden;

namespace Lerngruppe
{


    public class Anmelden
    {
        public string Signedname { get; set; }
        public bool SignedIn { get; set; }
        
        public void AnmeldenMenuStart(bool SignedIn, string Signname)
        {
            
            if (SignedIn == false)
            {   
                Console.WriteLine("Zum Anmelden geben Sie Ihren Namen ein!");
                string eingabename = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"WILLKOMMEN");
                Console.WriteLine($"╔══════════╗");
                Console.WriteLine($"║{eingabename}     ║");
                Console.WriteLine($"╩══════════╝");
                
                this.Signedname = eingabename;
                this.SignedIn = true;

            }
            else { Anmelden.AnmeldungMenuAnzeigen(Signname); }
        }
        public static string AnmeldungMenuAnzeigen(string name)
        {
            string willkommenBox =
            $@"╔══════════╗
║ {name}     ║
╩══════════╝";
            return willkommenBox;
        }
        public class Menu
        {
            public void MenuAnzeigen(string name)
            {
                Console.WriteLine("Was soll angezeigt werden?");
                Console.WriteLine("1. Meine Termine");
                Console.WriteLine("2. Infos über mich");
                Console.WriteLine("3. Verfügbare Kurse");

                int x = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                switch (x)
                {
                    case 1:
                        {
                            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=LerngruppenKoordinatorDB;Trusted_Connection=True;";
                            string query = $"SELECT BenutzerID, LERNGRUPPENID, TERMIN FROM Terminkalender WHERE BenutzerID = {name}";
                            try
                            {
                                using (SqlConnection connection1 = new SqlConnection(connectionString))
                                {
                                    connection1.Open();
                                    using (SqlCommand command = new SqlCommand(query,connection1))
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        Console.WriteLine("╔════════════════╦════════════════╦══════╦════════════════╦");
                                        Console.WriteLine("║ BenutzerID     ║ LerngruppenID  ║Termin║ Studiengang    ║");
                                        Console.WriteLine("╠════════════════╬════════════════╬══════╬════════════════╬");

                                        while (reader.Read())
                                        {
                                            string BenutzerID = reader["BenutzerID"].ToString().PadRight(12);
                                            string GruppenID = reader["LERNGRUPPENID"].ToString().PadRight(12);
                                            string termin = Convert.ToDateTime(reader["Termin"]).ToString("yyyy-MM-dd HH:mm").PadRight(20);
                                            Console.WriteLine($"║ {BenutzerID} ║ {GruppenID} ║ {termin}");
                                        }
                                    }
                                    Console.WriteLine("Verbindung erfolgreich");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Fehler bei der Verbindung: " + ex.Message);
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
                Logo logo = new Logo();
                Console.WriteLine(logo.LogoAnzeigen());
                Console.WriteLine("Verbindung wird aufgebaut...");
                Verbindungaufbauen.ConnectionOn();
            }
        }
        public class Logo
        {
            public string LogoAnzeigen()
            {
                string Logo1 = ("  _                                                             __  __                                   \r\n | |                                                           |  \\/  |                                  \r\n | |     ___ _ __ _ __   __ _ _ __ _   _ _ __  _ __   ___ _ __ | \\  / | __ _ _ __   __ _  __ _  ___ _ __ \r\n | |    / _ \\ '__| '_ \\ / _` | '__| | | | '_ \\| '_ \\ / _ \\ '_ \\| |\\/| |/ _` | '_ \\ / _` |/ _` |/ _ \\ '__|\r\n | |___|  __/ |  | | | | (_| | |  | |_| | |_) | |_) |  __/ | | | |  | | (_| | | | | (_| | (_| |  __/ |   \r\n |______\\___|_|  |_| |_|\\__, |_|   \\__,_| .__/| .__/ \\___|_| |_|_|  |_|\\__,_|_| |_|\\__,_|\\__, |\\___|_|   \r\n                         __/ |          | |   | |                                         __/ |          \r\n                        |___/           |_|   |_|                                        |___/           ");
                return Logo1;
            }

        }
        public class Verbindungaufbauen
        {
            public static void ConnectionOn()
            {
                string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=LerngruppenKoordinatorDB;Trusted_Connection=True;";

                try
                {
                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        connection2.Open();
                        Console.WriteLine("Verbindung erfolgreich");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler bei der Verbindung: " + ex.Message);
                }
            }

        }
        class Program
        {

            static void Main(string[] args)
            {
                Menu menu = new Menu();
                Logo logo = new Logo();
                Anmelden anmeldung1 = new Anmelden();
                bool Anmeldung = false;
                Start.Init();
                anmeldung1.AnmeldenMenuStart(false, "");
                Anmeldung = true;

                while (Anmeldung == true)
                {
                    
                    anmeldung1.AnmeldenMenuStart(anmeldung1.SignedIn, $"{anmeldung1.Signedname}");
                    menu.MenuAnzeigen(anmeldung1.Signedname);
                }
                //Menu.MenuAnzeigen();
            }
        }
    }
}
