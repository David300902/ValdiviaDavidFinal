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
using Microsoft.Maps.MapControl.WPF;
using GestionConferencias.Controllers;
using GestionConferencias.Model;

namespace GestionConferencias.Views.Conferencias
{
    /// <summary>
    /// Lógica de interacción para ListConferences.xaml
    /// </summary>
    public partial class ListConferences : UserControl
    {
        public ListConferences()
        {
            InitializeComponent();
            if(SessionController.Status == 1)
            {
                listConferencesView.Margin = new Thickness(-150,0,150,0);
            }
            SelectLecturers();
            Select();
            cargarCards(conferences);
            
        }
        LecturerController controllerLecturer;
        ConferencesController conConference;
        List<Conferences> conferences;
        List<Lecturer> lecturersList;

        void cargarCards(List<Conferences> listConferences)
        {
            List.Children.Clear();
            for (int i = 0; i < listConferences.Count; i++)
            {
                Cards(
                    listConferences[i].Title,
                    listConferences[i].FullNameLecturer,
                    listConferences[i].PharseLecturer,
                    listConferences[i].StartDate.ToString(),
                    listConferences[i].StartTime,
                    listConferences[i].EndTime,
                    listConferences[i].PeopleCapacity.ToString(),
                    listConferences[i].Latitude,
                    listConferences[i].Longitude
                    );
            }
        }
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
                conferences = new List<Conferences>();
                for (int i = 0; i < conference.Rows.Count; i++)
                {
                    var extra = (from n in lecturersList where n.ID == int.Parse(conference.Rows[i][9].ToString()) select new { name = n.FullName, pharse = n.Pharse }).ToList();

                    conferences.Add(new Conferences
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
                        extra[0].name.ToString(),
                        extra[0].pharse.ToString()
                    ));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void Cards(string title, string lecturer, string pharse, string date, string starTime, string endTime, string capacity, string latitude, string longitude)
        {
            int x = conferences.Count;
            List.Height = x * 250;
            Grid g = new Grid();
            g.Width = 1000;
            g.Height = 230;
            g.Margin = new Thickness(10);
            List.Children.Add(g);

            Border borde = new Border();
            borde.Background = Brushes.Red;
            borde.CornerRadius = new CornerRadius(10);
            g.Children.Add(borde);

            Border borde2 = new Border();
            borde2.Background = Brushes.Black;
            borde2.CornerRadius = new CornerRadius(10,80,10,10);
            g.Children.Add(borde2);

            StackPanel panel = new StackPanel();
            panel.Width = 1100;
            panel.Height = 200;
            panel.Margin = new Thickness(10);
            panel.Orientation = Orientation.Horizontal;
            g.Children.Add(panel);


            Map map = new Map();
            map.Height = 200;
            map.Width = 190;
            map.ZoomLevel = 13;
            map.CredentialsProvider = new ApplicationIdCredentialsProvider("AqkXPDVPLaPmnT5_sar3UEr51_aZmJ9DS5dfUE0RSUzCayts0Ct-b1XPyz3ssIN0");
            Location point = new Location(double.Parse(latitude), double.Parse(longitude));
            Pushpin p = new Pushpin();
            p.Location = point;
            map.Children.Add(p);
            map.Center = point;
            map.Margin = new Thickness(5);
            panel.Children.Add(map);


            StackPanel panel2 = new StackPanel();
            panel2.Width = 800;
            panel2.Height = 200;
            panel2.Margin = new Thickness(10);
            panel2.Orientation = Orientation.Vertical;
            panel.Children.Add(panel2);

            Label titulo = new Label();
            titulo.HorizontalAlignment = HorizontalAlignment.Center;
            titulo.Margin = new Thickness(0);
            titulo.Height = 50;
            titulo.Content = title;
            titulo.FontSize = 30;
            titulo.Foreground = Brushes.White;
            panel2.Children.Add(titulo);

            TextBlock frase = new TextBlock();
            frase.HorizontalAlignment = HorizontalAlignment.Center;
            frase.Width = 700;
            frase.TextWrapping = TextWrapping.Wrap;
            frase.Text = "Orador: " + lecturer + "\n" + "\"" + pharse + "\"";
            frase.TextAlignment = TextAlignment.Center;
            frase.Margin = new Thickness(5);
            frase.FontSize = 16;
            frase.Foreground = Brushes.White;
            panel2.Children.Add(frase);

            StackPanel panel3 = new StackPanel();
            panel3.Width = 800;
            panel3.Height = 60;
            panel3.Margin = new Thickness(10);
            panel3.Orientation = Orientation.Horizontal;
            panel2.Children.Add(panel3);



            TextBlock frase2 = new TextBlock();
            frase2.HorizontalAlignment = HorizontalAlignment.Center;
            frase2.Width = 182;
            frase2.TextWrapping = TextWrapping.Wrap;
            frase2.Text = "Fecha:" + "\n" + date;
            frase2.TextAlignment = TextAlignment.Center;
            frase2.Margin = new Thickness(5);
            frase2.FontSize = 16;
            frase2.Foreground = Brushes.White;
            panel3.Children.Add(frase2);

            TextBlock frase3 = new TextBlock();
            frase3.HorizontalAlignment = HorizontalAlignment.Center;
            frase3.Width = 182;
            frase3.TextWrapping = TextWrapping.Wrap;
            frase3.Text = "Inicio: " + "\n" + starTime;
            frase3.TextAlignment = TextAlignment.Center;
            frase3.Margin = new Thickness(5);
            frase3.FontSize = 16;
            frase3.Foreground = Brushes.White;
            panel3.Children.Add(frase3);

            TextBlock frase4 = new TextBlock();
            frase4.HorizontalAlignment = HorizontalAlignment.Center;
            frase4.Width = 182;
            frase4.TextWrapping = TextWrapping.Wrap;
            frase4.Text = "Finalizacion:" + "\n" + endTime;
            frase4.TextAlignment = TextAlignment.Center;
            frase4.Margin = new Thickness(5);
            frase4.FontSize = 16;
            frase4.Foreground = Brushes.White;
            panel3.Children.Add(frase4);

            TextBlock frase5 = new TextBlock();
            frase5.HorizontalAlignment = HorizontalAlignment.Center;
            frase5.Width = 182;
            frase5.TextWrapping = TextWrapping.Wrap;
            frase5.Text = "Capacidad:" + "\n" + capacity + " personas";
            frase5.TextAlignment = TextAlignment.Center;
            frase5.Margin = new Thickness(5);
            frase5.FontSize = 16;
            frase5.Foreground = Brushes.White;
            panel3.Children.Add(frase5);







        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            string x ="", y = "";
            if(txtHoraInicio.SelectedTime.ToString() != "")
            {
                string hInicio = txtHoraInicio.SelectedTime.ToString();
                var inicio = hInicio.Split(' ');
                x = inicio[1].Trim();
            }
            if (txtHoraFin.SelectedTime.ToString() != "")
            {
                string hFin = txtHoraFin.SelectedTime.ToString();
                var fin = hFin.Split(' ');
                y = fin[1].Trim();
            }

            if (txtDate.Text != "") {
                
                var filtroFecha = from item in conferences
                                  where item.StartDate == DateTime.Parse(txtDate.Text) || item.StartTime == x || item.EndTime == y
                                  select item;
                cargarCards(filtroFecha.ToList());
            }
            else
            {
                MessageBox.Show("debe ingresar la fecha para filtrar");
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cargarCards(conferences);
        }
    }
}
