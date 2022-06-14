using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionConferencias.Model;
using System.Data.SqlClient;

namespace GestionConferencias.Controllers
{
    public class UserPersonController : BaseController
    {
        public int Delete(UserPerson t)
        {
            string query = @"UPDATE UserPerson
                        SET status = 0, lastUpdate=GETDATE()
                        WHERE id = @id";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@id", t.ID);
            try
            {
                return ExecuteCommand(command);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Insert(UserPerson t)
        {
            string query = @"INSERT INTO UserPerson(bussinesName,email,password)
                             VALUES(@bussinesName,@email,HASHBYTES('md5',@password))";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@bussinesName", t.BussinesName);
            command.Parameters.AddWithValue("@email", t.Email);
            command.Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
            try
            {
                return ExecuteCommand(command);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DataTable Login(string userName, string password)
        {
            string query = @" SELECT id, bussinesName, email, status
	                          FROM UserPerson
	                          WHERE (status = 1 OR status = 2) AND bussinesName=@bussinesName AND password= HASHBYTES('md5',@password)";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@bussinesName", userName);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;

            try
            {
                return ExecuteDataTable(command);
            }
            catch (Exception ex)
            {
                //Log
                throw ex;
            }
        }

        public int Update(UserPerson t)
        {
            string query = @"UPDATE UserPerson
                SET bussinesName = @bussinesName, password = HASHBYTES('md5',@password), lastUpdate=GETDATE(),
                        email = @email
                WHERE id = @id";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@id", t.ID);
            command.Parameters.AddWithValue("@bussinesName", t.BussinesName);
            command.Parameters.AddWithValue("@password", t.Password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@email", t.Email);

            try
            {
                return ExecuteCommand(command);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
