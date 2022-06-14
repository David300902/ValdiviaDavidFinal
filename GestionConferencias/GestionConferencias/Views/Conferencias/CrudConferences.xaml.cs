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
using GestionConferencias.Model;
using GestionConferencias.Views;

namespace GestionConferencias.Views.Conferencias
{
    /// <summary>
    /// Lógica de interacción para CrudConferences.xaml
    /// </summary>
    public partial class CrudConferences : UserControl
    {
        public CrudConferences()
        {
            InitializeComponent();
        }
        int id;
        string fullName = "";
        ConferencesController conConference;
        LecturerController controllerLecturer;
        Lecturer lecturer;
        Conferences conference;
        UserControl uc;
        private void dtgConferences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgConferences.SelectedItem != null && dtgConferences.Items.Count > 0)
            {
                int x = dtgConferences.SelectedIndex;
                conference = conferencesList[x];
                id = conferencesList[x].ID;
                fullName = conferencesList[x].FullNameLecturer;
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            uc = new RegisterConference(1);
            GridConferences.Children.Clear();
            GridConferences.Children.Add(uc);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            uc = new RegisterConference(2, id);
            GridConferences.Children.Clear();
            GridConferences.Children.Add(uc);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
            Select();
        }
        void Delete()
        {
            if (conference != null && dtgConferences.SelectedItems != null)
            {
                if (MessageBox.Show("esta seguro de eliminarlo?", "eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    conConference = new ConferencesController();
                    try
                    {
                        int n = conConference.Delete(conference);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SelectLecturers();
            Select();
        }

        List<Conferences> conferencesList;
        List<Lecturer> lecturersList;
        void SelectLecturers()
        {
            controllerLecturer = new LecturerController();
            try
            {
                var lecturer = controllerLecturer.SelectID();
                lecturersList = new List<Lecturer>();
                for (int i = 0; i < lecturer.Rows.Count; i++)
                {
                    lecturersList.Add(new Lecturer
                    (
                        int.Parse(lecturer.Rows[i][0].ToString()),
                        lecturer.Rows[i][1].ToString(),
                        lecturer.Rows[i][2].ToString()
                    ));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void Select()
        {
            conConference = new ConferencesController();
            try
            {
                var conference = conConference.Select();
                conferencesList = new List<Conferences>();
                for (int i = 0; i < conference.Rows.Count; i++)
                {
                    var extra = (from n in lecturersList where n.ID == int.Parse(conference.Rows[i][9].ToString()) select new { name = n.FullName, pharse = n.Pharse }).ToList();

                    conferencesList.Add(new Conferences
                    (
                        int.Parse(conference.Rows[i][0].ToString()),
                        DateTime.Parse(conference.Rows[i][1].ToString()),
                        conference.Rows[i][2].ToString(),
                        conference.Rows[i][3].ToString(),
                        conference.Rows[i][4].ToString(),
                        int.Parse(conference.Rows[i][5].ToString()),
                        conference.Rows[i][7].ToString(),
                        conference.Rows[i][6].ToString(),
                        conference.Rows[i][8].ToString(),
                        int.Parse(conference.Rows[i][9].ToString()),
                        extra[0].name.ToString(),
                        extra[0].pharse.ToString()
                    ));
                }
                dtgConferences.ItemsSource = conferencesList;
                dtgConferences.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
