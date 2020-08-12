﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrestamosJuegos.Entidades
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public int AmigoId { get; set; }
        public string Observacion { get; set; }
        public int CantidadJuegos { get; set; }

        [ForeignKey("AmigoId")]
        public virtual Amigos Amigo { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<PrestamosDetalle> PrestamosDetalles { get; set; }

    }
}
