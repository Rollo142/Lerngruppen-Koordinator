using LerngruppekoordinatorAufgabe2;
using LerngruppekoordinatorAufgabe2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
namespace LerngruppekoordinatorAufgabe2
{
        public partial class GruppeSuchenWindow : Window
        {
            private List<Lerngruppe> _alleGruppen;
            public ObservableCollection<Lerngruppe> GefilterteGruppen { get; set; } = new();
            public event EventHandler<Lerngruppe>? GruppeBeigetreten;
            private void Beitreten(object sender, RoutedEventArgs e)
            {
            var button = sender as Button;
            var gruppe = button?.Tag as Lerngruppe;
            if (gruppe == null) return;

            GruppeBeigetreten?.Invoke(this, gruppe);
            MessageBox.Show($"'{gruppe.Name}' wurde zur Liste hinzugefügt!");
            }
            public GruppeSuchenWindow(List<Lerngruppe> alleGruppen)
            {
                InitializeComponent();
                DataContext = this;
                _alleGruppen = alleGruppen;

                
                foreach (var g in _alleGruppen)
                    GefilterteGruppen.Add(g);
            }

            private void txtSuche_TextChanged(object sender, TextChangedEventArgs e)
            {
                GefilterteGruppen.Clear();

                var name = txtSuche.Text.ToLower();
                var fach = txtFachSuche.Text.ToLower();

                var gefiltert = _alleGruppen.Where(g =>
                    (string.IsNullOrEmpty(name) || g.Name.ToLower().Contains(name)) &&
                    (string.IsNullOrEmpty(fach) || g.Fach.ToLower().Contains(fach))
                );

                foreach (var g in gefiltert)
                    GefilterteGruppen.Add(g);
            }
        }
}

