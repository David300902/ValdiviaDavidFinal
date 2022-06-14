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
using System.Windows.Shapes;
using GestionConferencias.Model;
using GestionConferencias.Controllers;

namespace GestionConferencias.Views.Login
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        UserPersonController conPerson;
        UserPerson person;

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == txtRepeatPassword.Password)
                Insert();
            else
                MessageBox.Show("Las contraseñas no coinciden");
        }
        void Insert()
        {
            if (txtEmail.Text.Contains("@gmail.com"))
            {
                try
                {
                    conPerson = new UserPersonController();
                    person = new UserPerson(1, txtUserName.Text, txtEmail.Text, txtPassword.Password.ToString());
                    try
                    {
                        int req = conPerson.Insert(person);
                        if (req > 0)
                        {
                            MessageBox.Show("Insertado");
                            this.Close();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datos no ingresados");
                }

            }
            else
            {
                MessageBox.Show("Correo o imagenes Invalidos");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
