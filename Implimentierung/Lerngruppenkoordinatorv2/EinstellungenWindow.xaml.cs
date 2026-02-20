using LerngruppekoordinatorAufgabe2.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;
using LerngruppekoordinatorAufgabe2;

namespace LerngruppekoordinatorAufgabe2
{
    public partial class EinstellungenWindow : Window
    {
        private LerngruppenKoordinatorDBContext _dBContext;
        public event EventHandler? DatenZurueckgesetzt;
        public EinstellungenWindow()
        {
            InitializeComponent();
            _dBContext = new LerngruppenKoordinatorDBContext();
        }

        private void VerbindungTesten(object sender, RoutedEventArgs e)
        {
            try
            {
                bool canConnect = _dBContext.Database.CanConnect();
                if (canConnect)
                    ZeigeStatus("Verbindung erfolgreich!", erfolgreich: true);
                else
                    ZeigeStatus("Verbindung fehlgeschlagen!", erfolgreich: false);
            }
            catch (Exception ex)
            {
                ZeigeStatus($"Fehler:\n{ex.Message}", erfolgreich: false);
            }
        }
        private void DatenZuruecksetzen(object sender, RoutedEventArgs e) 
        {
            var bestaetigung = MessageBox.Show(
                "Alle Daten werden gelöscht und Beispieldaten eingefügt. Fortfahren?",
                "Achtung",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (bestaetigung != MessageBoxResult.Yes) return;

            try
            {
                _dBContext.Termine.RemoveRange(_dBContext.Termine);
                _dBContext.Lerngruppe.RemoveRange(_dBContext.Lerngruppe);
                _dBContext.Benutzer.RemoveRange(_dBContext.Benutzer);
                _dBContext.SaveChanges();

                var benutzer = new List<Model.Benutzer>
            {
                new() { Name = "Anna Schmidt",  Adresse = "Hauptstraße 12", Plz = "66111", Studiengang = "Informatik",   Fachsemester = 3 },
                new() { Name = "Jonas Becker",  Adresse = "Bergstraße 5",   Plz = "66115", Studiengang = "Maschinenbau", Fachsemester = 2 },
                new() { Name = "Laura Klein",   Adresse = "Marktplatz 8",   Plz = "66119", Studiengang = "Psychologie",  Fachsemester = 4 },
                new() { Name = "Max Müller",    Adresse = "Schulstraße 7",  Plz = "66113", Studiengang = "BWL",          Fachsemester = 1 },
                new() { Name = "Sophie Meier",  Adresse = "Gartenweg 3",    Plz = "66121", Studiengang = "Medizin",      Fachsemester = 2 },
            };
                _dBContext.Benutzer.AddRange(benutzer);
                _dBContext.SaveChanges();

                var gruppen = new List<Model.Lerngruppe>
            {
                new() { Name = "Algo-Team",       Fach = "Algorithmen und Datenstrukturen", Adresse = "Uni Campus E1", Plz = "66123", Raum = 101, DatumUhrzeit = new DateTime(2026, 3, 1, 10, 0, 0) },
                new() { Name = "Maschbau-Gruppe", Fach = "Technische Mechanik",             Adresse = "HTW Saar B",    Plz = "66117", Raum = 202, DatumUhrzeit = new DateTime(2026, 3, 2, 14, 0, 0) },
                new() { Name = "PsychoLab",       Fach = "Kognitive Psychologie",           Adresse = "Uni Campus C3", Plz = "66125", Raum = 303, DatumUhrzeit = new DateTime(2026, 3, 3, 9,  0, 0) },
                new() { Name = "BWL-Workshop",    Fach = "Betriebswirtschaft",              Adresse = "Uni Campus F2", Plz = "66122", Raum = 404, DatumUhrzeit = new DateTime(2026, 3, 4, 11, 0, 0) },
                new() { Name = "Medizin-Gruppe",  Fach = "Anatomie und Physiologie",        Adresse = "Uni Campus G1", Plz = "66124", Raum = 505, DatumUhrzeit = new DateTime(2026, 3, 5, 13, 0, 0) },
            };
                _dBContext.Lerngruppe.AddRange(gruppen);
                _dBContext.SaveChanges();

                ZeigeStatus("Beispieldaten erfolgreich eingefügt!", erfolgreich: true);
                DatenZurueckgesetzt?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ZeigeStatus($"Fehler:\n{ex.Message}", erfolgreich: false);
            }
        }

        private void ZeigeStatus(string nachricht, bool erfolgreich)
        {
            statusBorder.Visibility = Visibility.Visible;
            txtStatus.Text = nachricht;
            if (erfolgreich)
            {
                statusBorder.Background = new SolidColorBrush(Color.FromRgb(230, 255, 230));
                statusBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(150, 220, 150));
                txtStatus.Foreground = new SolidColorBrush(Color.FromRgb(0, 130, 0));
            }
            else
            {
                statusBorder.Background = new SolidColorBrush(Color.FromRgb(255, 238, 238));
                statusBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 200, 200));
                txtStatus.Foreground = Brushes.Red;
            }
        }
    }
}
