using PrestamosJuegos.BLL;
using PrestamosJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrestamosJuegos.UI.Registros.rAmigo
{
    /// <summary>
    /// Interaction logic for rAmigo.xaml
    /// </summary>
    public partial class rAmigo : Window
    {
        //todo: Hacer las validaciones de los campos.
        private Amigos Amigo = new Amigos();
        public rAmigo()
        {
            InitializeComponent();
            this.DataContext = Amigo;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = AmigosBLL.Buscar(int.Parse(AmigoIdTextBox.Text));
            if(encontrado != null)
            {
                Amigo = encontrado;
                this.DataContext = Amigo;
            }
            else
            {
                MessageBox.Show("Ese amigo no existe en la base de datos.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AmigosBLL.Guardar(Amigo))
            {
                Limpiar();
                MessageBox.Show("Guardado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AmigosBLL.Eliminar(int.Parse(AmigoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Limpiar()
        {
            Amigo = new Amigos();
            this.DataContext = Amigo;
        }
    }
}
