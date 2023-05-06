using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IngeSoftFinal.Models;

public partial class VotosContext : DbContext
{
    public VotosContext()
    {
    }

    public VotosContext(DbContextOptions<VotosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Estadistica> Estadisticas { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<Votante> Votantes { get; set; }

    public virtual DbSet<Voto> Votos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;userid=root;password=;database=votos;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("candidato");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Partido)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Estadistica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadistica");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Fraudes)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Votos)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sistema");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Fase)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Vigente).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Votante>(entity =>
        {
            entity.HasKey(e => e.Dpi).HasName("PRIMARY");

            entity.ToTable("votante");

            entity.Property(e => e.Dpi)
                .HasMaxLength(13)
                .HasColumnName("DPI");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.EstadoVoto).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Voto>(entity =>
        {
            entity.HasKey(e => new { e.VotanteDpi, e.CandidatoId }).HasName("PRIMARY");

            entity.ToTable("voto");

            entity.HasIndex(e => e.CandidatoId, "CandidatoID");

            entity.Property(e => e.VotanteDpi)
                .HasMaxLength(13)
                .HasColumnName("VotanteDPI");
            entity.Property(e => e.CandidatoId)
                .HasColumnType("int(11)")
                .HasColumnName("CandidatoID");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.IpCompu)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Votos)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("voto_ibfk_2");

            entity.HasOne(d => d.VotanteDpiNavigation).WithMany(p => p.Votos)
                .HasForeignKey(d => d.VotanteDpi)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("voto_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
