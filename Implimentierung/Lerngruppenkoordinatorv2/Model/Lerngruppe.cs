using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class Lerngruppe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Lerngruppe()
        {
            Termine = new HashSet<Termine>();
        }

        public int Id { get; set; }
        private string? _name =  null!;
        private string? _fach;
        private decimal _raum;
        private string? _adresse;
        private string? _plz;
        //private byte[]? _unterrichtsmaterial;

        public string Name {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Fach {
            get => _fach;
            set { _fach = value; OnPropertyChanged(nameof(Fach)); }
        }
        public decimal Raum {
            get => _raum;
            set { _raum = value; OnPropertyChanged(nameof(Raum)); }
        }
        public string Adresse {
            get => _adresse;
            set { _adresse = value; OnPropertyChanged(nameof(Adresse)); }
        }

        public string Plz
        {
            get => _plz;
            set { _plz = value; OnPropertyChanged(nameof(Plz)); }
        }

        //public byte[] Unterrichtsmaterial
        //{
        //    get => _unterrichtsmaterial;
        //    set { _unterrichtsmaterial = value; OnPropertyChanged(nameof(Unterrichtsmaterial)); }
        //}

        public virtual ICollection<Termine> Termine { get; set; }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
