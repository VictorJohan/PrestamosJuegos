using PrestamosJuegos.UI.Consultas.cAmigo;
using PrestamosJuegos.UI.Registros.rAmigo;
using PrestamosJuegos.UI.Registros.rEntrada;
using PrestamosJuegos.UI.Registros.rJuego;
using PrestamosJuegos.UI.Registros.rPrestamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrestamosJuegos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            rAmigo rAmigo = new rAmigo();
            rAmigo.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            rEntrada rEntrada = new rEntrada();
            rEntrada.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rJuego rJuego = new rJuego();
            rJuego.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            rPrestamo rPrestamo = new rPrestamo();
            rPrestamo.Show();
        }

        //Consultas-------------------------
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            cAmigo cAmigo = new cAmigo();
            cAmigo.Show();
        }
    }
}
