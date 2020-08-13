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
    public class EntradasBLL
    {
        public static bool Guardar(Entradas entrada)
        {
            if (!Existe(entrada.EntradaId))
                return Insertar(entrada);
            else
                return Modificar(entrada);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Entradas.Any(e => e.EntradaId  == id);
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

        private static bool Insertar(Entradas entrada)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entradas.Add(entrada);
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

        private static bool Modificar(Entradas entrada)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(entrada).State = EntityState.Modified;
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

        public static Entradas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Entradas entrada;

            try
            {
                entrada = contexto.Entradas.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entrada;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Entradas.Find(id);
                if (eliminar != null)
                {
                    contexto.Entradas.Remove(eliminar);
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

        public static List<Entradas> GetAmigos()
        {
            Contexto contexto = new Contexto();
            List<Entradas> lista = new List<Entradas>();

            try
            {
                lista = contexto.Entradas.ToList();
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

        public static List<Entradas> GetAmigos(Expression<Func<Entradas, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Entradas> lista = new List<Entradas>();

            try
            {
                lista = contexto.Entradas.Where(criterio).ToList();
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
