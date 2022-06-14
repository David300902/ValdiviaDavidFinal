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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionConferencias.Controllers;
using GestionConferencias.Model;
using GestionConferencias.Views;

namespace GestionConferencias.Views.Conferencistas
{
    /// <summary>
    /// Lógica de interacción para ListLecturers.xaml
    /// </summary>
    public partial class ListLecturers : UserControl
    {
        public ListLecturers()
        {
            InitializeComponent();
        }
        int id;
        LecturerController controllerLecturer;
        Lecturer lecturer;
        UserControl uc;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Select();
        }
        List<Lecturer> lecturersList;
        void Select()
        {
            controllerLecturer = new LecturerController();
            dtgLecturers.ItemsSource = null;
            try
            {
                var s = controllerLecturer.Select();
                lecturersList = new List<Lecturer>();
                for (int i = 0; i < s.Rows.Count; i++)
                {
                    lecturersList.Add(new Lecturer(
                            int.Parse(s.Rows[i][0].ToString()),
                            s.Rows[i][1].ToString(),
                            s.Rows[i][2].ToString(),
                            s.Rows[i][3].ToString(),
                            DateTime.Parse(s.Rows[i][4].ToString()),
                            s.Rows[i][5].ToString(),
                            s.Rows[i][6].ToString(),
                            Char.Parse(s.Rows[i][7].ToString())
                        ));
                }
                dtgLecturers.ItemsSource = lecturersList;
                dtgLecturers.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            uc = new RegisterLecturers(1);
            GridLecturers.Children.Clear();
            GridLecturers.Children.Add(uc);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            uc = new RegisterLecturers(2, id);
            GridLecturers.Children.Clear();
            GridLecturers.Children.Add(uc);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            Select();
        }

        void Delete()
        {
            if (lecturer != null && dtgLecturers.SelectedItems != null)
            {
                if (MessageBox.Show("esta seguro de eliminarlo?", "eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    controllerLecturer = new LecturerController();
                    try
                    {
                        int n = controllerLecturer.Delete(lecturer);
                        if (n > 0)
                        {
                            MessageBox.Show("eliminado ;v");
                            Select();
                        }
                        else
                        {
                            MessageBox.Show("fallo de eliminacion");
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            else
            {
                MessageBox.Show("debe seleccionar un item");
            }
        }

        private void dtgLecturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgLecturers.SelectedItem != null && dtgLecturers.Items.Count > 0)
            {
                int x = dtgLecturers.SelectedIndex;
                lecturer = lecturersList[x];
                id = lecturersList[x].ID;
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }

        }
    }
}
