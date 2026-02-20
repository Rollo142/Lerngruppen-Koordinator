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
            LernGruppenLaden();
            MeineGruppenLaden();
            
        }
        //--------------------------------------------------------------------------

        //--------------------------------------------------------------------------LOADING ENGINE
        private void MeineGruppenLaden()
        {

            var meinegruppe = dBContext.Termine.Include(t => t.Lerngruppen).ToList();
            foreach (var t in meinegruppe)
            {
                Viewmodel.MeineGruppen.Add(t);
            }
        }
        private void LernGruppenLaden()
        {
            var gruppen = dBContext.Lerngruppe.ToList();

            foreach (var gruppe in gruppen)
            {
                Viewmodel.VerfuegbareGruppen.Add(gruppe);
            }
        }
        private void BenutzerLaden()
        {
            var benutzer = Viewmodel.Nutzer;

        }
        //--------------------------------------------------------------------------

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