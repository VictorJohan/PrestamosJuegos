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
    public class JuegosBLL
    {
        public static bool Guardar(Juegos juego)
        {
            if (!Existe(juego.JuegoId))
                return Insertar(juego);
            else
                return Modificar(juego);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Juegos.Any(j => j.JuegoId == id);
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

        private static bool Insertar(Juegos juego)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Juegos.Add(juego);
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

        private static bool Modificar(Juegos juego)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(juego).State = EntityState.Modified;
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

        public static Juegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Juegos juego;

            try
            {
                juego = contexto.Juegos.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return juego;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Juegos.Find(id);
                if (eliminar != null)
                {
                    contexto.Juegos.Remove(eliminar);
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

        public static List<Juegos> GetAmigos()
        {
            Contexto contexto = new Contexto();
            List<Juegos> lista = new List<Juegos>();

            try
            {
                lista = contexto.Juegos.ToList();
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

        public static List<Juegos> GetAmigos(Expression<Func<Juegos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Juegos> lista = new List<Juegos>();

            try
            {
                lista = contexto.Juegos.Where(criterio).ToList();
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
