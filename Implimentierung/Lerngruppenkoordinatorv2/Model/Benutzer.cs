using System.ComponentModel;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class Benutzer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Benutzer()
        {
            Termine = new HashSet<Termine>();
        }

        public int Id { get; set; }

        private string _name = null!;
        private string? _adresse;
        private string? _plz;
        private string? _studiengang;
        private int? _fachsemester;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string? Adresse
        {
            get => _adresse;
            set { _adresse = value; OnPropertyChanged(nameof(Adresse)); }
        }
        public string? Plz
        {
            get => _plz;
            set { _plz = value; OnPropertyChanged(nameof(Plz)); }
        }
        public string? Studiengang
        {
            get => _studiengang;
            set { _studiengang = value; OnPropertyChanged(nameof(Studiengang)); }
        }
        public int? Fachsemester
        {
            get => _fachsemester;
            set { _fachsemester = value; OnPropertyChanged(nameof(Fachsemester)); }
        }

        public virtual ICollection<Termine> Termine { get; set; }
    }
}