using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using GestionConferencias.Controllers;

namespace GestionConferencias.Views.Login
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        UserPersonController conPerson;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPerson();
        }

        void LoginPerson()
        {
            conPerson = new UserPersonController();
            DataTable table = new DataTable();

            try
            {
                table = conPerson.Login(txtUserName.Text, txtPassword.Password.ToString());
                if (table.Rows.Count > 0)
                {
                    SessionController.UserName= table.Rows[0][1].ToString();
                    SessionController.Status = byte.Parse(table.Rows[0][3].ToString());

                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("incorrecto");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void lbOlvide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
        }
    }
}
