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

namespace PrestamosJuegos.UI.Consultas.cPrestamo
{
    /// <summary>
    /// Interaction logic for cPrestamo.xaml
    /// </summary>
    public partial class cPrestamo : Window
    {
        string[] filtro = { "Id", "Amigo Id", "Observación", "Cantidad Juegos" };
        public cPrestamo()
        {
            InitializeComponent();
            FiltroComBox.ItemsSource = filtro;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Prestamos>();

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
                        lista =PrestamosBLL.GetPrestamos(p => p.PrestamoId == int.Parse(CriterioTexBox.Text));
                        break;

                    case 1://Amigo Id
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+$"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                                "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista =PrestamosBLL.GetPrestamos(p => p.AmigoId == int.Parse(CriterioTexBox.Text));
                        break;

                    case 2://Observacion
                        //Valida que se introduzaca una cantidad valida.
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+${1,9}"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado cantidad valida.",
                                "Cantidad no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista =PrestamosBLL.GetPrestamos(p => p.Observacion == CriterioTexBox.Text);
                        break;

                    case 3://Cantidad de juegos
                        //Valida que se introduzaca una cantidad valida.
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+${1,9}"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado cantidad valida.",
                                "Cantidad no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = PrestamosBLL.GetPrestamos(p => p.CantidadJuegos == int.Parse(CriterioTexBox.Text));
                        break;

                }
            }
            else
            {
                lista =PrestamosBLL.GetPrestamos(p => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                lista = PrestamosBLL.GetPrestamos(e => e.Fecha.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                lista = PrestamosBLL.GetPrestamos(e => e.Fecha.Date <= HastaDatePicker.SelectedDate);

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = lista;
        }
    }
}
