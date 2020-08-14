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

namespace PrestamosJuegos.UI.Consultas.cJuego
{
    /// <summary>
    /// Interaction logic for cJuego.xaml
    /// </summary>
    public partial class cJuego : Window
    {
        string[] filtro = {"Juego Id", "Descripción", "Precio", "Existencia"};
        public cJuego()
        {
            InitializeComponent();
            FiltroComBox.ItemsSource = filtro;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Juegos>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0://Id
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+$"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                                "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = JuegosBLL.GetJuegos(e => e.JuegoId == int.Parse(CriterioTexBox.Text));
                        break;

                    case 1://Descripcion
                        lista = JuegosBLL.GetJuegos(j => j.Descripcion == CriterioTexBox.Text);
                        break;

                    case 2://Precio
                           //válida que haya un dato válido en el precio.
                        if (!Regex.IsMatch(CriterioTexBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
                        {
                            MessageBox.Show("Solo puede introducir carácteres numéricos.", "Campo Precio.",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        lista = JuegosBLL.GetJuegos(j => j.Precio == double.Parse(CriterioTexBox.Text));
                        break;

                    case 3://Existencia
                        //Valida que se introduzaca una cantidad valida.
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+${1,9}"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado cantidad valida.",
                                "Cantidad no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = JuegosBLL.GetJuegos(e => e.Existencia == int.Parse(CriterioTexBox.Text));
                        break;

                }
            }
            else
            {
                lista = JuegosBLL.GetJuegos();
            }

            if (DesdeDatePicker.SelectedDate != null)
                lista = JuegosBLL.GetJuegos(j => j.FechaCompra.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                lista = JuegosBLL.GetJuegos(j => j.FechaCompra.Date <= HastaDatePicker.SelectedDate);

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = lista;
        }
    }
}
