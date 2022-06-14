using Microsoft.Maps.MapControl.WPF;
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

namespace GestionConferencias.Views.Conferencias
{
    /// <summary>
    /// Lógica de interacción para RegisterConference.xaml
    /// </summary>
    public partial class RegisterConference : UserControl
    {
        int op;
        int idSelect;
        public RegisterConference(int opcion, int id = 0)
        {
            op = opcion;
            InitializeComponent();
            if (op == 2)
            {
                idSelect = id;
                btnInsert.Content = "Actualizar";
                conConference = new ConferencesController();
                conference = conConference.Get(id);
                if (conference != null)
                {
                    txtCapacity.Text = conference.PeopleCapacity.ToString();
                    txtDescription.Text = conference.Description;
                    txtEndTime.Text = conference.EndTime;
                    txtStartDate.Text = conference.StartDate.ToString();
                    txtStartTime.Text = conference.StartTime;
                    txtTitle.Text = conference.Title;
                    point = new Location(double.Parse(conference.Latitude), double.Parse(conference.Longitude));
                    p = new Pushpin();
                    p.Location = point;
                    txtLocation.Center = point;
                    txtLocation.Children.Add(p);
                }
            }
        }
        Conferences conference;
        ConferencesController conConference;

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            txtLocation.Focus();
            txtLocation.ZoomLevel++;
        }
        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            txtLocation.Focus();
            txtLocation.ZoomLevel--;
        }
        private void btnRoad_Click(object sender, RoutedEventArgs e)
        {
            txtLocation.Focus();
            txtLocation.Mode = new RoadMode();
        }
        private void btnAerial_Click(object sender, RoutedEventArgs e)
        {
            txtLocation.Focus();
            txtLocation.Mode = new AerialMode(true);
        }
        Location point;
        Pushpin p;
        private void txtLocation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtLocation.Children.Clear();
            e.Handled = true;
            var mousePosition = e.GetPosition((UIElement)sender);
            point = txtLocation.ViewportPointToLocation(mousePosition);

            p = new Pushpin();
            p.Location = point;
            txtLocation.Children.Add(p);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLecturer();
        }
        void LoadLecturer()
        {
            LecturerController conLecturer = new LecturerController();
            try
            {
                cmbLecturer.ItemsSource = null;
                cmbLecturer.DisplayMemberPath = "fullName";
                cmbLecturer.SelectedValuePath = "id";
                cmbLecturer.ItemsSource = conLecturer.SelectID().DefaultView;
                cmbLecturer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }
        void ClearInputs()
        {
            txtCapacity.Text = "";
            txtDescription.Text = "";
            txtEndTime.Text = "";
            txtStartDate.Text = "";
            txtStartTime.Text = "";
            txtTitle.Text = "";
            txtLocation.Children.Clear();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if(op == 1)
            {
                Insert();
            }
            else
            {
                Update();
            }
        }
        void Insert()
        {
            try
            {
                conference = new Conferences(1, DateTime.Parse(txtStartDate.Text), txtStartTime.Text, txtEndTime.Text, txtDescription.Text,int.Parse(txtCapacity.Text),
                    point.Longitude.ToString(), point.Latitude.ToString(), txtTitle.Text,int.Parse(cmbLecturer.SelectedValue.ToString()));
                try
                {
                    conConference = new ConferencesController();
                    int req = conConference.Insert(conference);
                    if (req > 0)
                    {
                        MessageBox.Show("Insertado");
                        ClearInputs();
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
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        void Update()
        {
            try
            {
                conference = new Conferences(idSelect, DateTime.Parse(txtStartDate.Text), txtStartTime.Text, txtEndTime.Text, txtDescription.Text, int.Parse(txtCapacity.Text),
                    point.Longitude.ToString(), point.Latitude.ToString(), txtTitle.Text, int.Parse(cmbLecturer.SelectedValue.ToString()));
                try
                {
                    conConference = new ConferencesController();
                    int req = conConference.Update(conference);
                    if (req > 0)
                    {
                        MessageBox.Show("Actualizado");
                        ClearInputs();
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
    }
}
