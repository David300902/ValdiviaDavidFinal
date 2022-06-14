using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionConferencias.Model
{
    public class Conferences
    {

        public int ID{ get; set; }
        public DateTime StartDate{ get; set; }
        public string StartTime{ get; set; }
        public string EndTime{ get; set; }
        public string Description{ get; set; }
        public int PeopleCapacity{ get; set; }
        public string Longitude{ get; set; }
        public string Latitude{ get; set; }
        public string Title{ get; set; }
        public int LecturerID{ get; set; }
        public string FullNameLecturer{ get; set; }
        public string PharseLecturer{ get; set; }

        public Conferences(int iD, DateTime startDate, string startTime, string endTime, string description, 
            int peopleCapacity, string longitude, string latitude, string title, int lecturerID)
        {
            ID = iD;
            StartDate = startDate;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            PeopleCapacity = peopleCapacity;
            Longitude = longitude;
            Latitude = latitude;
            Title = title;
            LecturerID = lecturerID;
        }

        public Conferences(int iD, DateTime startDate, string startTime, 
            string endTime, string description, int peopleCapacity, 
            string longitude, string latitude, string title, 
            string fullNameLecturer, string pharseLecturer)
        {
            ID = iD;
            StartDate = startDate;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            PeopleCapacity = peopleCapacity;
            Longitude = longitude;
            Latitude = latitude;
            Title = title;
            FullNameLecturer = fullNameLecturer;
            PharseLecturer = pharseLecturer;
        }

        public Conferences(int iD, DateTime startDate, string startTime, string endTime, string description, 
            int peopleCapacity, string longitude, string latitude, string title, int lecturerID, string fullNameLecturer, string pharseLecturer) : this(iD, startDate, startTime, endTime, description, peopleCapacity, longitude, latitude, title, lecturerID)
        {
            FullNameLecturer = fullNameLecturer;
            PharseLecturer = pharseLecturer;
        }
    }
}
