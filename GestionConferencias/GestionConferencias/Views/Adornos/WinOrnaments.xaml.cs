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
    /// Lógica de interacción para WinOrnaments.xaml
    /// </summary>
    public partial class WinOrnaments : UserControl
    {
        public WinOrnaments()
        {
            InitializeComponent();
            lbSaludo.Content = SessionController.UserName;
        }
    }
}
