using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PrestamosJuegos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Ocurrio un error no controlado:\n{e.Exception.Message}",
                "Excepcion no controlada.", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}
