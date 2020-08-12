﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrestamosJuegos.DAL;

namespace PrestamosJuegos.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200812021328_Migracion_Inicial")]
    partial class Migracion_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("PrestamosJuegos.Entidades.Amigos", b =>
                {
                    b.Property<int>("AmigoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Celular")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("AmigoId");

                    b.ToTable("Amigos");
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.Entradas", b =>
                {
                    b.Property<int>("EntrdaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<int>("JuegoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EntrdaId");

                    b.ToTable("Entradas");
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.Juegos", b =>
                {
                    b.Property<int>("JuegoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<int>("Existencia")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("TEXT");

                    b.Property<double>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("JuegoId");

                    b.ToTable("Juegos");
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.Prestamos", b =>
                {
                    b.Property<int>("PrestamoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmigoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CantidadJuegos")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacion")
                        .HasColumnType("TEXT");

                    b.HasKey("PrestamoId");

                    b.HasIndex("AmigoId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.PrestamosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JuegoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrestamoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JuegoId");

                    b.HasIndex("PrestamoId");

                    b.ToTable("PrestamosDetalle");
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.Prestamos", b =>
                {
                    b.HasOne("PrestamosJuegos.Entidades.Amigos", "Amigo")
                        .WithMany()
                        .HasForeignKey("AmigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrestamosJuegos.Entidades.PrestamosDetalle", b =>
                {
                    b.HasOne("PrestamosJuegos.Entidades.Juegos", "Juego")
                        .WithMany()
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrestamosJuegos.Entidades.Prestamos", null)
                        .WithMany("PrestamosDetalles")
                        .HasForeignKey("PrestamoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
