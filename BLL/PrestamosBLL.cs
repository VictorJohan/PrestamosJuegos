using Microsoft.EntityFrameworkCore;
using PrestamosJuegos.DAL;
using PrestamosJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PrestamosJuegos.BLL
{
    class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            if (!Existe(prestamo.PrestamoId))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Insertar(Prestamos prestamo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                foreach (var item in prestamo.PrestamosDetalles)
                {
                    item.Juego.Existencia -= item.Cantidad;
                    contexto.Entry(item.Juego).State = EntityState.Modified;
                }
                contexto.Prestamos.Add(prestamo);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Modificar(Prestamos prestamo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;
            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM PrestamosDetalle Where PrestamoId={prestamo.PrestamoId}");
                foreach (var item in prestamo.PrestamosDetalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(prestamo).State = EntityState.Modified;
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos prestamo;

            try
            {
                prestamo = contexto.Prestamos.Where(p => p.PrestamoId == id).Include(p => p.PrestamosDetalles)
                    .ThenInclude(d => d.Juego).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamo;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Prestamos.Find(id);
                if(eliminar != null)
                {
                    contexto.Entry(eliminar).State = EntityState.Deleted;
                    ok = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Prestamos> lista = new List<Prestamos>();

            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
