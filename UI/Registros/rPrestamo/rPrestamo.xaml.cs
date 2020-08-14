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

namespace PrestamosJuegos.UI.Registros.rPrestamo
{
    /// <summary>
    /// Interaction logic for rPrestamo.xaml
    /// </summary>
    public partial class rPrestamo : Window
    {
        private Prestamos Prestamo = new Prestamos();
        private int previousLineCount = 0;//Variable que ayudara con el salto de linea.
        public rPrestamo()
        {
            InitializeComponent();
            this.DataContext = Prestamo;
            //ComboBox Amigo
            AmigoComboBox.ItemsSource = AmigosBLL.GetAmigos();
            AmigoComboBox.SelectedValuePath = "AmigoId";
            AmigoComboBox.DisplayMemberPath = "Nombres";
            //ComboBox Juego
            JuegoComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoComboBox.SelectedValuePath = "JuegoId";
            JuegoComboBox.DisplayMemberPath = "Descripcion";
        }

        //Busca un registro.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var encontrado = PrestamosBLL.Buscar(int.Parse(PrestamoIdTextBox.Text));
            if (encontrado != null)
            {
                Prestamo = encontrado;
                this.DataContext = Prestamo;
            }
            else
            {
                MessageBox.Show("Ese prestamo no existe en la base de datos.", "No se encontro.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Agrega un juego al DataGrid
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;

            var detalle = new PrestamosDetalle
            {
                Id = 0,
                PrestamoId = int.Parse(PrestamoIdTextBox.Text),
                JuegoId = int.Parse(JuegoComboBox.SelectedValue.ToString()),
                Cantidad = int.Parse(CantidadTextBox.Text)
            };

            detalle.Juego = (Juegos)JuegoComboBox.SelectedItem;
           
            Prestamo.PrestamosDetalles.Add(detalle);
            
            Prestamo.CantidadJuegos += int.Parse(CantidadTextBox.Text);

            Cargar();
            CantidadTextBox.Clear();
        }

        //Elimina una fila del DataGrid
        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            var detalle = (PrestamosDetalle)DetalleDataGrid.SelectedItem;
            Prestamo.CantidadJuegos -= detalle.Cantidad;
            Prestamo.PrestamosDetalles.RemoveAt(DetalleDataGrid.SelectedIndex);
            Cargar();
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

            if (PrestamosBLL.Guardar(Prestamo))
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
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PrestamosBLL.Eliminar(int.Parse(PrestamoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Este TextChanged dara un salto de linea al llegar al margen del TextBox
        private void ObservacionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ObservacionTextBox.LineCount > previousLineCount)
            {
                previousLineCount = ObservacionTextBox.LineCount;
            }
        }

        //Refresca el DataGrid
        public void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Prestamo;
        }

        //Limpia el registro
        public void Limpiar()
        {
            Prestamo = new Prestamos();
            this.DataContext = Prestamo;
        }

        //Valida los campos
        public bool Validar()
        {
            //Valida el Id
            if (!Regex.IsMatch(PrestamoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                    "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Valida que se seleccione un amigo
            if(AmigoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Asegúrese de haber seleccionado un amigo.",
                   "Campo Amigo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        
        //Valida los campos del detalle
        public bool ValidarAgregar()
        {
            //Valida que se haya seleccionado un juego
            if (JuegoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Asegúrese de haber seleccionado un juego.",
                   "Campo Juego", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Valida que se introduzaca una cantidad valida.
            if (!Regex.IsMatch(CantidadTextBox.Text, "^[1-9]+${1,9}"))
            {
                MessageBox.Show("Asegúrese de haber ingresado cantidad valida.",
                    "Cantidad no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Valida que la cantidad no sea mayor que la cantidad existente
            if(JuegosBLL.Existencia(int.Parse(JuegoComboBox.SelectedValue.ToString())) < int.Parse(CantidadTextBox.Text))
            {
                MessageBox.Show($"En el inventario solo quedan {JuegosBLL.Existencia(int.Parse(JuegoComboBox.SelectedValue.ToString()))} " +
                    $"unidades disponibles.",
                    "Cantidad insuficiente.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
