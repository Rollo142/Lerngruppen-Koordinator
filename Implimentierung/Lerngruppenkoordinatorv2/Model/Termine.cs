using System;
using System.Collections.Generic;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class Termine
    {
        public int Id { get; set; }
        public int BenutzerId { get; set; }
        public int LerngruppenId { get; set; }
        public string? Adresse { get; set; }
        public string? Fach { get; set; }
        public byte[]? Unterrichtsmaterial { get; set; }
        public decimal? Raum { get; set; }
        public DateTime? DatumUhrzeit { get; set; }

        public virtual Benutzer Benutzer { get; set; } = null!;
        public virtual Lerngruppe Lerngruppen { get; set; } = null!;
    }
}
