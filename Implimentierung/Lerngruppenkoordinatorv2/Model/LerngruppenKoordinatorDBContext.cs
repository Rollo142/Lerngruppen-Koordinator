using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LerngruppekoordinatorAufgabe2.Model
{
    public partial class LerngruppenKoordinatorDBContext : DbContext
    {
        public LerngruppenKoordinatorDBContext()
        {
        }

        public LerngruppenKoordinatorDBContext(DbContextOptions<LerngruppenKoordinatorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Benutzer> Benutzer { get; set; } = null!;
        public virtual DbSet<Lerngruppe> Lerngruppe { get; set; } = null!;
        public virtual DbSet<Termine> Termine { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LerngruppenKoordinatorDB;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benutzer>(entity =>
            {
                entity.ToTable("BENUTZER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADRESSE");

                entity.Property(e => e.Fachsemester).HasColumnName("FACHSEMESTER");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Plz)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PLZ");

                entity.Property(e => e.Studiengang)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STUDIENGANG");
            });

            modelBuilder.Entity<Lerngruppe>(entity =>
            {
                entity.ToTable("LERNGRUPPE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ADRESSE");

                entity.Property(e => e.Fach)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FACH");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Plz)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PLZ");

                entity.Property(e => e.Raum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("RAUM");

                //entity.Property(e => e.Unterrichtsmaterial).HasColumnName("UNTERRICHTSMATERIAL");
            });

            modelBuilder.Entity<Termine>(entity =>
            {
                entity.ToTable("TERMINE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADRESSE");

                entity.Property(e => e.BenutzerId).HasColumnName("BenutzerID");

                entity.Property(e => e.DatumUhrzeit)
                    .HasColumnType("datetime")
                    .HasColumnName("DATUM_UHRZEIT");

                entity.Property(e => e.Fach)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FACH");

                entity.Property(e => e.LerngruppenId).HasColumnName("LerngruppenID");

                entity.Property(e => e.Raum)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("RAUM");

                entity.HasOne(d => d.Benutzer)
                    .WithMany(p => p.Termine)
                    .HasForeignKey(d => d.BenutzerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TERMINE__Benutze__60A75C0F");

                entity.HasOne(d => d.Lerngruppen)
                    .WithMany(p => p.Termine)
                    .HasForeignKey(d => d.LerngruppenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TERMINE__Lerngru__619B8048");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
