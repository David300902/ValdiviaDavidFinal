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
using GestionConferencias.Model;
using GestionConferencias.Controllers;
using GestionConferencias.Views.Conferencistas;
using GestionConferencias.Views.Adornos;

namespace GestionConferencias.Views.Conferencistas
{
    /// <summary>
    /// Lógica de interacción para RegisterLecturers.xaml
    /// </summary>
    public partial class RegisterLecturers : UserControl
    {
        int op;
        int idSelect;
        public RegisterLecturers(int opcion, int id = 0)
        {
            op = opcion;
            InitializeComponent();
            if(op == 2)
            {
                idSelect = id;
                btnRegister.Content = "Actualizar";
                conLecturer = new LecturerController();
                lecturer = conLecturer.Get(id);
                if (lecturer != null)
                {
                    txtFirstName.Text = lecturer.FirstName;
                    txtLastName.Text = lecturer.LastName;
                    txtSecLastName.Text = lecturer.SecondLastName;
                    txtBirthDate.Text = lecturer.BirthDate.ToString();
                    txtProfession.Text = lecturer.Profession;
                    txtPhrase.Text = lecturer.Pharse;
                    cmbGender.Text = lecturer.Gender.ToString();
                }
            }
        }
        Lecturer lecturer;
        LecturerController conLecturer;
        UserControl uc;

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CleanInputs();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (op == 1)
            {
                Insert();
                CleanInputs();
                
            }
            else
            {
                Update();
                CleanInputs();
            }
        }

        void Insert()
        {
            try
            {
                lecturer = new Lecturer(idSelect, txtFirstName.Text, txtLastName.Text, txtSecLastName.Text, DateTime.Parse(txtBirthDate.Text),
                            txtProfession.Text, txtPhrase.Text, char.Parse(cmbGender.SelectionBoxItem.ToString()));
                try
                {
                    conLecturer = new LecturerController();
                    int req = conLecturer.Insert(lecturer);
                    if (req > 0)
                    {
                        MessageBox.Show("Insertado");
                    }
                    else
                    {
                        MessageBox.Show("No insertado");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        void Update()
        {
            try
            {
                lecturer = new Lecturer(idSelect, txtFirstName.Text, txtLastName.Text, txtSecLastName.Text, DateTime.Parse(txtBirthDate.Text),
                            txtProfession.Text, txtPhrase.Text, char.Parse(cmbGender.SelectionBoxItem.ToString()));
                try
                {
                    int req = conLecturer.Update(lecturer);
                    if(req > 0){
                        MessageBox.Show("Actualizado");
                    }
                    else
                    {
                        MessageBox.Show("No Actualizado");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        void CleanInputs()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSecLastName.Text = "";
            txtBirthDate.Text = "";
            txtProfession.Text = "";
            txtPhrase.Text = "";
            txtSecLastName.Text = "";
        }
    }
}
