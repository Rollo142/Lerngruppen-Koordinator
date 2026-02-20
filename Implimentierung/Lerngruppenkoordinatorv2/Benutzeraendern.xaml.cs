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
    public partial class BenutzerAendernWindow : Window
    {
        private Benutzer _benutzer;
        public ObservableCollection<Benutzer> Benutzer { get; set; } = new();

        public BenutzerAendernWindow(Benutzer benutzer)
        {
            InitializeComponent();
            _benutzer = benutzer;
            txtName.Text = benutzer.Name;
            txtAdresse.Text = benutzer.Adresse;
            txtPlz.Text = benutzer.Plz;
            txtStudiengang.Text = benutzer.Studiengang;
            txtFachsemester.Text = benutzer.Fachsemester?.ToString();
            DataContext = this;
            using var db = new LerngruppenKoordinatorDBContext();
            var alleBenutzer = db.Benutzer.ToList();
            foreach (var b in alleBenutzer)
            {
            Benutzer.Add(b);
            }
                
        }
        private void BenutzerAuswaehlen(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ausgewaehlter = button?.Tag as Benutzer;
            if (ausgewaehlter == null) return;

            
            _benutzer.Id = ausgewaehlter.Id;
            txtName.Text = ausgewaehlter.Name;
            txtAdresse.Text = ausgewaehlter.Adresse;
            txtPlz.Text = ausgewaehlter.Plz;
            txtStudiengang.Text = ausgewaehlter.Studiengang;
            txtFachsemester.Text = ausgewaehlter.Fachsemester?.ToString();
        }


        private void Speichern(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name darf nicht leer sein!");
                return;
            }
            _benutzer.Name = txtName.Text;
            _benutzer.Adresse = txtAdresse.Text;
            _benutzer.Plz = txtPlz.Text;
            _benutzer.Studiengang = txtStudiengang.Text;
            _benutzer.Fachsemester = int.TryParse(txtFachsemester.Text, out int fs) ? fs : null;
            DialogResult = true; 
            Close();
        }
    }
}