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
using GestionConferencias.Controllers;

namespace GestionConferencias.Views.Adornos
{
    /// <summary>
    /// Lógica de interacción para WinMenu.xaml
    /// </summary>
    public partial class WinMenu : UserControl
    {
        public WinMenu()
        {
            InitializeComponent();
            if(SessionController.Status == 1)
            {
                grid1.Visibility = Visibility.Hidden;
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        UserControl uc;
        private void btnConferencista_Click(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(uc);
            uc = new Views.Conferencistas.ListLecturers();
            Main.Children.Add(uc);
        }

        private void btnConferencias_Click(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(uc);
            uc = new Views.Conferencias.CrudConferences();
            Main.Children.Add(uc);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(uc);
            uc = new Conferencias.ListConferences();
            Main.Children.Add(uc);
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            Main.Children.Remove(uc);
            uc = new Views.Conferencias.ListConferences();
            Main.Children.Add(uc);
        }
    }
}
