using LerngruppekoordinatorAufgabe2.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace LerngruppekoordinatorAufgabe2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--------------------------------------------------------------------------BINDING ENGINE
        public class MainViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            protected void OnPropertyChanged(string name) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            private Benutzer _nutzer = new Benutzer();
            public Benutzer Nutzer
            {
                get => _nutzer;
                set { _nutzer = value; OnPropertyChanged(nameof(Nutzer)); }
            }

            public ObservableCollection<Lerngruppe> VerfuegbareGruppen { get; set; }
            public ObservableCollection<Termine> MeineGruppen { get; set; }
            public ObservableCollection<Benutzer> BenutzerGruppe { get; set; }
            public MainViewModel()
            {
                VerfuegbareGruppen = new ObservableCollection<Lerngruppe>();
                MeineGruppen = new ObservableCollection<Termine>();
                BenutzerGruppe = new ObservableCollection<Benutzer>();
                _nutzer = new Benutzer();
            }
        }

        //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------Main
        public LerngruppenKoordinatorDBContext dBContext;
        public MainViewModel Viewmodel;
        public MainWindow()
        {
            InitializeComponent();
            Viewmodel = new MainViewModel();
            DataContext = Viewmodel;
            dBContext = new LerngruppenKoordinatorDBContext();
            DatenbankInitialisieren();
            LernGruppenLaden();
            MeineGruppenLaden();
            
        } 
        //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------LOADING ENGINE
        private void MeineGruppenLaden()
        {
            Viewmodel.MeineGruppen.Clear();

            if (Viewmodel.Nutzer.Id == 0) return; // kein Nutzer angemeldet

            var meinegruppe = dBContext.Termine
                .Include(t => t.Lerngruppen)
                .Where(t => t.BenutzerId == Viewmodel.Nutzer.Id) // NEU
                .ToList();

            foreach (var t in meinegruppe)
                Viewmodel.MeineGruppen.Add(t);
        } 
        private void LernGruppenLaden()
        {
            var gruppen = dBContext.Lerngruppe.ToList();

            foreach (var gruppe in gruppen)
            {
                Viewmodel.VerfuegbareGruppen.Add(gruppe);
            }
        }
        private void DatenbankInitialisieren()
        {
            dBContext.Database.EnsureCreated();

            if (!dBContext.Lerngruppe.Any())
            {
                dBContext.Lerngruppe.AddRange(
                    new Lerngruppe { Name = "Algo-Team", Fach = "Algorithmen und Datenstrukturen", Adresse = "Uni Campus E1", Plz = "66123", Raum = 101, DatumUhrzeit = new DateTime(2026, 3, 1, 10, 0, 0) },
                    new Lerngruppe { Name = "Maschbau-Gruppe", Fach = "Technische Mechanik", Adresse = "HTW Saar B", Plz = "66117", Raum = 202, DatumUhrzeit = new DateTime(2026, 3, 2, 14, 0, 0) },
                    new Lerngruppe { Name = "PsychoLab", Fach = "Kognitive Psychologie", Adresse = "Uni Campus C3", Plz = "66125", Raum = 303, DatumUhrzeit = new DateTime(2026, 3, 3, 9, 0, 0) }
                );
                dBContext.SaveChanges();
            }

            if (!dBContext.Benutzer.Any())
            {
                dBContext.Benutzer.AddRange(
                    new Benutzer { Name = "Anna Schmidt", Adresse = "Hauptstraße 12", Plz = "66111", Studiengang = "Informatik", Fachsemester = 3 },
                    new Benutzer { Name = "Jonas Becker", Adresse = "Bergstraße 5", Plz = "66115", Studiengang = "Maschinenbau", Fachsemester = 2 },
                    new Benutzer { Name = "Laura Klein", Adresse = "Marktplatz 8", Plz = "66119", Studiengang = "Psychologie", Fachsemester = 4 }
                );
                dBContext.SaveChanges();
            }
        }
        //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------PDF ENGINE
        private void PdfOeffnen(byte[] pdfBytes, string? dateiname)
        {
            var tempPfad = System.IO.Path.Combine(System.IO.Path.GetTempPath(), dateiname ?? "dokument.pdf");
            System.IO.File.WriteAllBytes(tempPfad, pdfBytes);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = tempPfad,
                UseShellExecute = true
            });
        }
        private void PdfAnzeigenLerngruppe(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var gruppe = button?.Tag as Lerngruppe;
            if (gruppe == null) return;

            if (gruppe.Unterrichtsmaterial == null)
            {
                MessageBox.Show("Kein PDF vorhanden!");
                return;
            }
            PdfOeffnen(gruppe.Unterrichtsmaterial, gruppe.UnterrichtsmaterialName);
        }
        private void PdfUploadTermin(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var termin = button?.Tag as Termine;
            if (termin == null) return;

            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "PDF Dateien (*.pdf)|*.pdf",
                Title = "PDF auswählen"
            };

            if (dialog.ShowDialog() == true)
            {
                termin.Unterrichtsmaterial = System.IO.File.ReadAllBytes(dialog.FileName);
                termin.UnterrichtsmaterialName = System.IO.Path.GetFileName(dialog.FileName);
                dBContext.Termine.Update(termin);
                dBContext.SaveChanges();
                MessageBox.Show($"PDF '{termin.UnterrichtsmaterialName}' erfolgreich hochgeladen!");
            }
        }
        private void PdfAnzeigenTermin(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var termin = button?.Tag as Termine;
            if (termin == null) return;

            if (termin.Unterrichtsmaterial == null)
            {
                MessageBox.Show("Kein PDF vorhanden!");
                return;
            }
            PdfOeffnen(termin.Unterrichtsmaterial, termin.UnterrichtsmaterialName);
        }
        //--------------------------------------------------------------------------BUTTON ENGINE
        private void GruppeErstellen(object sender, RoutedEventArgs e)
        {
            var dialog = new GruppeErstellenWindow();
            if (dialog.ShowDialog() == true)
            {
                dBContext.Lerngruppe.Add(dialog.NeueLerngruppe);
                dBContext.SaveChanges();
                Viewmodel.VerfuegbareGruppen.Add(dialog.NeueLerngruppe);
            }

        }
        private void BenutzerAnmelden(object sender, RoutedEventArgs e)
        {
            if (Viewmodel.Nutzer == null)
                Viewmodel.Nutzer = new Benutzer { Name = "" };

            // Kopie
            var temp = new Benutzer
            {
                Id = Viewmodel.Nutzer.Id,
                Name = Viewmodel.Nutzer.Name,
                Adresse = Viewmodel.Nutzer.Adresse,
                Plz = Viewmodel.Nutzer.Plz,
                Studiengang = Viewmodel.Nutzer.Studiengang,
                Fachsemester = Viewmodel.Nutzer.Fachsemester
            };

            var dialog = new BenutzerAendernWindow(temp);
            if (dialog.ShowDialog() == true)
            {
                // Kopie Anfügen
                Viewmodel.Nutzer = temp;

                if (temp.Id == 0)
                    dBContext.Benutzer.Add(temp);
                else
                {
                    var existing = dBContext.Benutzer.Find(temp.Id);
                    if (existing != null)
                    {
                        existing.Name = temp.Name;
                        existing.Adresse = temp.Adresse;
                        existing.Plz = temp.Plz;
                        existing.Studiengang = temp.Studiengang;
                        existing.Fachsemester = temp.Fachsemester;
                    }
                }
                dBContext.SaveChanges();
                MessageBox.Show($"Benutzer: {Viewmodel.Nutzer.Name} | ID: {Viewmodel.Nutzer.Id}");
                MeineGruppenLaden();
            }
        }
        private void Einstellungen(object sender, RoutedEventArgs e)
        {
            var dialog = new EinstellungenWindow();
            dialog.DatenZurueckgesetzt += Dialog_DatenZurueckgesetzt;
            dialog.ShowDialog();
        }
        private void GruppeSuchen(object sender, RoutedEventArgs e)
        {
            var dialog = new GruppeSuchenWindow(Viewmodel.VerfuegbareGruppen.ToList());
            dialog.GruppeBeigetreten += SuchfeldGruppeBeigetreten;  
            dialog.ShowDialog();
        }
        //----------------------------------------------------------------Pipeline von GruppesuchenWindow
        private void SuchfeldGruppeBeigetreten(object? sender, Lerngruppe gruppe)
        {
            if (Viewmodel.Nutzer == null || Viewmodel.Nutzer.Id == 0)
            {
                MessageBox.Show("Bitte zuerst Benutzer anlegen oder auswählen!");
                return;
            }

            bool bereitsHinzugefuegt = Viewmodel.MeineGruppen
                .Any(t => t.LerngruppenId == gruppe.Id);

            if (bereitsHinzugefuegt)
            {
                MessageBox.Show("Du bist dieser Gruppe bereits beigetreten!");
                return;
            }

            var neuerTermin = new Termine
            {
                LerngruppenId = gruppe.Id,
                BenutzerId = Viewmodel.Nutzer.Id,
                Fach = gruppe.Fach,
                Adresse = gruppe.Adresse,
                Raum = gruppe.Raum,
                Lerngruppen = gruppe
            };

            dBContext.Entry(gruppe).State = EntityState.Unchanged;
            dBContext.Termine.Add(neuerTermin);
            dBContext.SaveChanges();
            Viewmodel.MeineGruppen.Add(neuerTermin);
        }
        //----------------------------------------------------------------
        //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------TABLE ACTIONS
        private void Beigetreten(object sender, RoutedEventArgs e)
        {
            if (Viewmodel.Nutzer == null || Viewmodel.Nutzer.Id == 0)
            {
                MessageBox.Show("Bitte zuerst Benutzer anlegen oder auswählen!");
                return;
            }

            var button = sender as Button;
            var gruppe = button?.Tag as Lerngruppe;
            
            bool bereitsHinzugefuegt = Viewmodel.MeineGruppen
            .Any(t => t.LerngruppenId == gruppe.Id);

            if (bereitsHinzugefuegt)
            {
                MessageBox.Show("Du bist dieser Gruppe bereits beigetreten!");
                return;
            }
            var neuerTermin = new Termine
            {
                LerngruppenId = gruppe.Id,
                BenutzerId = Viewmodel.Nutzer.Id,
                Fach = gruppe.Fach,
                Adresse = gruppe.Adresse,
                Raum = gruppe.Raum,
                Lerngruppen = gruppe,
                DatumUhrzeit = gruppe.DatumUhrzeit
            };
            dBContext.Lerngruppe.Attach(gruppe);
            dBContext.Termine.Add(neuerTermin);
            dBContext.SaveChanges();
            Viewmodel.MeineGruppen.Add(neuerTermin);
        }
        private void Verlassen(object sender, RoutedEventArgs e)
        {
            if (Viewmodel.Nutzer == null)
            {
                MessageBox.Show("Füge Nutzerdaten ein!");
                return;
            }

            var button = sender as Button;
            var termin = button?.Tag as Termine;

            if (termin == null) return;

            bool vorhanden = Viewmodel.MeineGruppen.Any(t => t.Id == termin.Id);

            if (!vorhanden)
            {
                MessageBox.Show("Du bist dieser Gruppe nicht beigetreten!");
                return;
            }

            dBContext.Termine.Remove(termin);
            dBContext.SaveChanges();
            Viewmodel.MeineGruppen.Remove(termin);
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------DEBUGGING
        private void Dialog_DatenZurueckgesetzt(object? sender, EventArgs e)
        {
            Viewmodel.VerfuegbareGruppen.Clear();
            Viewmodel.MeineGruppen.Clear();
            dBContext = new LerngruppenKoordinatorDBContext();
            LernGruppenLaden();
            MeineGruppenLaden();
        }
        //---------------------------------------------------------------------------
    }
}