using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionConferencias.Model
{
    public class UserPerson
    {

        public int ID{ get; set; }
        public string BussinesName{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }


        public UserPerson(int iD, string bussinesName, string email, string password)
        {
            ID = iD;
            BussinesName = bussinesName;
            Email = email;
            Password = password;
        }
    }
}
