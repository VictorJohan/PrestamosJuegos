using Microsoft.EntityFrameworkCore;
using PrestamosJuegos.DAL;
using PrestamosJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

namespace PrestamosJuegos.BLL
{
    public class EntradasBLL
    {
        public static bool Guardar(Entradas entrada)
        {
            if (!Existe(entrada.EntradaId))
            {
                IncrementaInventario(entrada);
                return Insertar(entrada);
            }
            else
            {
                if (ModificaInventario(entrada))
                    return Modificar(entrada);
                else
                    return false;
            }
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Entradas.Any(e => e.EntradaId == id);
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

        public static List<Entradas> GetEntradas()
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

        public static List<Entradas> GetEntradas(Expression<Func<Entradas, bool>> criterio)
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

        public static void IncrementaInventario(Entradas entrada)
        {
            Juegos juego = JuegosBLL.Buscar(entrada.JuegoId);
            juego.Existencia += entrada.Cantidad;
            JuegosBLL.Guardar(juego);
        }

        public static bool ModificaInventario(Entradas NuevaEntrada)
        {
            Entradas entrada = Buscar(NuevaEntrada.EntradaId);//Se buscala entrada anterior
            Juegos juego = JuegosBLL.Buscar(NuevaEntrada.JuegoId);//Se busca el juego a modificar

            juego.Existencia -= entrada.Cantidad;//Se le resta la cantidad de la entrada anterior.
            juego.Existencia += NuevaEntrada.Cantidad;//Se le suma la nueva cantidad.

            //Se puede dar el caso de que se preste una una cantidad X de juegos y se quiera modificar 
            //la entrada por una cantidad menor a la que se presto y el inventario quede en - 
            if (juego.Existencia < 0)
            {
                MessageBox.Show("No puedes realizar este cambio porque al parecer prestaste una cantidad mayor de la que ahora quieres " +
                    "ingresar.",
                    "Ha ocurrido un conflicto.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            JuegosBLL.Guardar(juego);
            return true;
        }
    }
}
