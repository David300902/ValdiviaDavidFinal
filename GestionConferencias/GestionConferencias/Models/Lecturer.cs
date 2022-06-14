using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionConferencias.Model
{
    public class Lecturer
    {

        public int ID{ get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string SecondLastName{ get; set; }
        public DateTime BirthDate{ get; set; }
        public string Profession{ get; set; }
        public string Pharse{ get; set; }
        public char Gender { get; set; }
        public string FullName{ get; set; }

        public Lecturer(int iD, string firstName, string lastName, string secondLastName, 
            DateTime birthDate, string profession, string pharse, char gender)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            SecondLastName = secondLastName;
            BirthDate = birthDate;
            Profession = profession;
            Pharse = pharse;
            Gender = gender;
        }

        public Lecturer(int iD, string fullName, string pharse)
        {
            ID = iD;
            FullName = fullName;
            Pharse = pharse;
        }

    }
}
