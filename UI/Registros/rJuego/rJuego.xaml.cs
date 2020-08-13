using PrestamosJuegos.BLL;
using PrestamosJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestamosJuegos.UI.Registros.rJuego
{
    /// <summary>
    /// Interaction logic for rJuego.xaml
    /// </summary>
    public partial class rJuego : Window
    {
        private Juegos Juego = new Juegos();
        public rJuego()
        {
            InitializeComponent();
            this.DataContext = Juego;
        }

        //Busca un registro.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var encontrado = JuegosBLL.Buscar(int.Parse(JuegoIdTextBox.Text));
            if (encontrado != null)
            {
                Juego = encontrado;
                this.DataContext = Juego;
            }
            else
            {
                MessageBox.Show("Ese juego no existe en la base de datos.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Limpia los campos del registro para crear uno nuevo.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Guarda un registro en la base de datos.
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (JuegosBLL.Guardar(Juego))
            {
                Limpiar();
                MessageBox.Show("Guardado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Elimina un registro de la base de datos.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (JuegosBLL.Eliminar(int.Parse(JuegoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Limpia los campos del registro.
        public void Limpiar()
        {
            Juego = new Juegos();
            this.DataContext = Juego;
        }

        //Valida los campos del registro.
        public bool Validar()
        {
            //Valida el Id
            if (!Regex.IsMatch(JuegoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //válida que no hayan campos vacíos.
            if (DescripcionTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de haber llenado todos los campos.", "Campos vacíos",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que haya un dato válido en el precio.
            if (!Regex.IsMatch(PrecioTextBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
            {
                MessageBox.Show("Solo puede introducir carácteres numéricos.", "Campo Precio.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que no se creen varios registro del mismo juego
            var juego = JuegosBLL.ExisteJuego(DescripcionTextBox.Text);
            if (juego != null)
            {
                if ((DescripcionTextBox.Text == juego.Descripcion) && (int.Parse(JuegoIdTextBox.Text) != juego.JuegoId))
                {
                    MessageBox.Show($"Este juego ya existe con el Id: {juego.JuegoId}.", "Existente en el inventario.",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
