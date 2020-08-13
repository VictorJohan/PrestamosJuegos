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
    public class AmigosBLL
    {
        public static bool Guardar(Amigos amigo)
        {
            if (!Existe(amigo.AmigoId))
                return Insertar(amigo);
            else
                return Modificar(amigo);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Amigos.Any(a => a.AmigoId == id);
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

        private static bool Insertar(Amigos amigo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Amigos.Add(amigo);
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

        private static bool Modificar(Amigos amigo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(amigo).State = EntityState.Modified;
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

        public static Amigos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Amigos amigo;

            try
            {
                amigo = contexto.Amigos.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return amigo;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Amigos.Find(id);
                if(eliminar != null)
                {
                    contexto.Amigos.Remove(eliminar);
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

        public static List<Amigos> GetAmigos()
        {
            Contexto contexto = new Contexto();
            List<Amigos> lista = new List<Amigos>();

            try
            {
                lista = contexto.Amigos.ToList();
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

        public static List<Amigos> GetAmigos(Expression<Func<Amigos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Amigos> lista = new List<Amigos>();

            try
            {
                lista = contexto.Amigos.Where(criterio).ToList();
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

        public static bool ExisteEmail(string email)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Amigos.Any(a => a.Email.Equals(email));
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

        public static bool ExisteCelular(string celular)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Amigos.Any(a => a.Celular.Equals(celular));
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

        public static bool ExisteTelefono(string telefono)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Amigos.Any(a => a.Telefono.Equals(telefono));
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
    }
}
