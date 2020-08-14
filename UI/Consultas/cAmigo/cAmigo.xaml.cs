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

namespace PrestamosJuegos.UI.Consultas.cAmigo
{
    /// <summary>
    /// Interaction logic for cAmigo.xaml
    /// </summary>
    public partial class cAmigo : Window
    {
        string[] filtro = {"Id", "Nombres", "Dirección", "Teléfono", "Celular", "Email"};
        public cAmigo()
        {
            InitializeComponent();
            FiltroComBox.ItemsSource = filtro;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var lista = new List<Amigos>();

            if(CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[1-9]+$"))
                        {
                            MessageBox.Show("Asegúrese de haber ingresado un Id de caracter numerico y que sea mayor a 0.",
                                "Id no valido", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        lista = AmigosBLL.GetAmigos(a => a.AmigoId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
                        {
                            MessageBox.Show("Solo se admiten carácteres alfabeticos.", "Nombre no válido.",
                               MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        lista = AmigosBLL.GetAmigos(a => a.Nombres == CriterioTexBox.Text);
                        break;
                    case 2:
                        lista = AmigosBLL.GetAmigos(a => a.Direccion == CriterioTexBox.Text);
                        break;
                    case 3:
                        if (!Regex.IsMatch(CriterioTexBox.Text, @"\d{3}\-\d{3}\-\d{4}"))
                        {
                            MessageBox.Show("Asegúrese de haber cumplido con el siguiente formato: 111-111-1111.", "Número celular no válido.",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        lista = AmigosBLL.GetAmigos(a => a.Telefono == CriterioTexBox.Text);
                        break;
                    case 4:
                        if (!Regex.IsMatch(CriterioTexBox.Text, @"\d{3}\-\d{3}\-\d{4}"))
                        {
                            MessageBox.Show("Asegúrese de haber cumplido con el siguiente formato: 111-111-1111.", "Número celular no válido.",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        lista = AmigosBLL.GetAmigos(a => a.Celular == CriterioTexBox.Text);
                        break;
                    case 5:
                        if (!Regex.IsMatch(CriterioTexBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
                        {
                            MessageBox.Show("La direccón de correo electrónico que ha introducido no es válida.", "Campo Email.",
                               MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        lista = AmigosBLL.GetAmigos(a => a.Telefono == CriterioTexBox.Text);
                        break;

                }
            }
            else
            {
                lista = AmigosBLL.GetAmigos();
            }

            if (DesdeDatePicker.SelectedDate != null)
                lista = AmigosBLL.GetAmigos(a => a.FechaNacimiento.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                lista = AmigosBLL.GetAmigos(a => a.FechaNacimiento.Date <= HastaDatePicker.SelectedDate);

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = lista;
        }
    }
}
