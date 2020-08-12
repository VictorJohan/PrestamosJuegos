using Microsoft.EntityFrameworkCore;
using PrestamosJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PrestamosJuegos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Juegos> Juegos { get; set; }
        public DbSet<Entradas> Entradas { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\PrestamosJuegos.db");
        }
    }
}
