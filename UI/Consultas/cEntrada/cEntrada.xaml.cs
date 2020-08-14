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

namespace PrestamosJuegos.UI.Consultas.cEntrada
{
    /// <summary>
    /// Interaction logic for cEntrada.xaml
    /// </summary>
    public partial class cEntrada : Window
    {
        string[] filtro = { "Id", "Juego Id", "Cantidad"};
        public cEntrada()
        {
            InitializeComponent();
            FiltroComBox.ItemsSource = filtro;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Entradas>();

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
                        lista = EntradasBLL.GetEntradas(e => e.EntradaId == int.Parse(CriterioTexBox.Text));
                        break;

                    case 1://Juego Id
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+$"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                                "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = EntradasBLL.GetEntradas(e => e.JuegoId == int.Parse(CriterioTexBox.Text));
                        break;

                    case 2://Cantidad
                        //Valida que se introduzaca una cantidad valida.
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+${1,9}"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado cantidad valida.",
                                "Cantidad no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = EntradasBLL.GetEntradas(e => e.Cantidad == int.Parse(CriterioTexBox.Text));
                        break;

                }
            }
            else
            {
                lista = EntradasBLL.GetEntradas();
            }

            if (DesdeDatePicker.SelectedDate != null)
                lista = EntradasBLL.GetEntradas(e => e.Fecha.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                lista = EntradasBLL.GetEntradas(e => e.Fecha.Date <= HastaDatePicker.SelectedDate);

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = lista;
        }
    }
}
