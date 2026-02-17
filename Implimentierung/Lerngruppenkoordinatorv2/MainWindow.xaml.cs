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

namespace LerngruppekoordinatorAufgabe2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LerngruppenKoordinatorDBContext dBContext;
        public ObservableCollection<Lerngruppe> VerfuegbareGruppen;
        public ObservableCollection<Termine> MeineGruppen;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            dBContext = new LerngruppenKoordinatorDBContext();
            VerfuegbareGruppen = new ObservableCollection<Lerngruppe>();
            MeineGruppen = new ObservableCollection<Termine>();
            LernGruppenLaden();
            MeineGruppenLaden();
        }

        private void MeineGruppenLaden()
        {
            var meinegruppe = dBContext.Termine.ToList();
            foreach (var t in meinegruppe)
            {
                MeineGruppen.Add(t);
            }
            DataContext = meinegruppe;
        }

        private void GruppeErstellen(object sender, RoutedEventArgs e)
        {
            
        }

        private void LerngruppeSuchen(object sender, RoutedEventArgs e)
        {

        }

        private void Einstellungen(object sender, RoutedEventArgs e)
        {

        }
        private void LernGruppenLaden()
        {
            var gruppen = dBContext.Lerngruppe.ToList();

            foreach (var gruppe in gruppen)
            { 
            VerfuegbareGruppen.Add(gruppe);
            }
            DataContext = gruppen;
        }
    }
}