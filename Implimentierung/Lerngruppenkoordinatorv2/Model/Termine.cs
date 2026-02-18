using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Xml.Linq;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class Termine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id {
                get => _id;
                set { _id = value; OnPropertyChanged(nameof(Id)); }
            }
        private int _id {  get; set; }
        public int BenutzerId{
            get => _benutzerid;
            set { _benutzerid= value; OnPropertyChanged(nameof(BenutzerId)); }
        }
        private int _benutzerid { get; set; }
        public int LerngruppenId {
                get => _lerngruppenid;
                set { _lerngruppenid = value; OnPropertyChanged(nameof(LerngruppenId)); }
            }
        private int _lerngruppenid { get; set; }
        public string? Adresse
        {
            get => _adresse;
            set { _adresse = value; OnPropertyChanged(nameof(Adresse)); }
        }
        private string? _adresse { get; set; }
        public string? Fach {
            get => _fach;
            set { _fach = value; OnPropertyChanged(nameof(Fach)); }
        }
        private string? _fach { get; set; }

        //public byte[]? Unterrichtsmaterial { get; set; }
        public decimal? Raum{
            get => _raum;
            set { _raum = value; OnPropertyChanged(nameof(Raum)); }
        }
        private decimal? _raum { get; set; }
        public DateTime? DatumUhrzeit {
                get => _datumuhrzeit;
                set { _datumuhrzeit = value; OnPropertyChanged(nameof(DatumUhrzeit)); }
            }
       
        private DateTime? _datumuhrzeit { get; set; }
        public virtual Benutzer Benutzer { get; set; } = null!;
        public virtual Lerngruppe Lerngruppen { get; set; } = null!;
        public string? Name => Lerngruppen?.Name;
        public string? Plz => Lerngruppen?.Plz;
        public DateTime? GruppenDatum => Lerngruppen?.DatumUhrzeit;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
