﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L01_2020AC602.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Publicacione> Publicaciones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Lily\\MSSQLSERVER01; Database=blog; Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.HasKey(e => e.CalificacionId).HasName("PK__califica__FA55ACB95EC77F6B");

            entity.ToTable("calificaciones");

            entity.Property(e => e.CalificacionId).HasColumnName("calificacionId");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.CometarioId).HasName("PK__comentar__9C549A7F4BE3A994");

            entity.ToTable("comentarios");

            entity.Property(e => e.CometarioId).HasColumnName("cometarioId");
            entity.Property(e => e.Comentario1)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
        });

        modelBuilder.Entity<Publicacione>(entity =>
        {
            entity.HasKey(e => e.PublicacionId).HasName("PK__publicac__28F7E7A1E7B53EE9");

            entity.ToTable("publicaciones");

            entity.Property(e => e.PublicacionId).HasColumnName("publicacionId");
            entity.Property(e => e.Descripcion)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__roles__540236347AAD6758");

            entity.ToTable("roles");

            entity.Property(e => e.RolId).HasColumnName("rolId");
            entity.Property(e => e.Rol)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__A5B1AB8E438C087F");

            entity.ToTable("usuarios");

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.RolId).HasColumnName("rolId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
