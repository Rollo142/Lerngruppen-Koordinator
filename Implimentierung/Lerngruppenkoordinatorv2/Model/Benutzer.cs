using System;
using System.Collections.Generic;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class Benutzer
    {
        public Benutzer()
        {
            Termine = new HashSet<Termine>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Adresse { get; set; }
        public string? Plz { get; set; }
        public string? Studiengang { get; set; }
        public int? Fachsemester { get; set; }

        public virtual ICollection<Termine> Termine { get; set; }
    }
}
