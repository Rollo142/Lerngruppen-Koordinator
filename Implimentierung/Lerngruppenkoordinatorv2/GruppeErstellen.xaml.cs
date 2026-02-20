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
        public partial class GruppeErstellenWindow : Window
        {
            public Lerngruppe NeueLerngruppe { get; set; }

            public GruppeErstellenWindow()
            {
                InitializeComponent();
            }

            private void Erstellen(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Name darf nicht leer sein!");
                    return;
                }

                if (!decimal.TryParse(txtRaum.Text, out decimal raum))
                {
                    MessageBox.Show("Raum muss eine Zahl sein!");
                    return;
                }

                NeueLerngruppe = new Lerngruppe
                {
                    Name = txtName.Text,
                    Fach = txtFach.Text,
                    Raum = raum,
                    Adresse = txtAdresse.Text,
                    Plz = txtPlz.Text,
                    DatumUhrzeit = dpDatum.SelectedDate
                };

                DialogResult = true;
                Close();
            }
        }
}
