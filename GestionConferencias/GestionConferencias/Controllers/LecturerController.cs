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
    public class LecturerController : BaseController, BaseInterface<Lecturer>
    {

        public DataTable SelectID()
        {
            string query = @"SELECT id, CONCAT(firstName, ' ', lastName,' ',secondLastName) AS fullName, phrase
                                FROM Lecturer
                                WHERE status = 1";
            SqlCommand command = BasicCommand(query);
            try
            {
                return ExecuteDataTable(command);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int Delete(Lecturer t)
        {
            string query = @"UPDATE Lecturer
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
        public Lecturer Get(int id)
        {
            Lecturer p = null;
            string query = @"SELECT id, firstName, lastName, secondLastName, birthDate, profession, phrase, gender
                                FROM Lecturer
                                WHERE id = @id";
            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = (SqlDataReader)ExecuteDataReader(command);
                while (reader.Read())
                {       
                    p = new Lecturer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(),
                        DateTime.Parse(reader[4].ToString()), reader[5].ToString(),reader[6].ToString(), Char.Parse(reader[7].ToString()));            
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }

            return p;
        }


        public int Insert(Lecturer t)
        {
            string query = @"INSERT INTO Lecturer(firstName, lastName, secondLastName, birthDate, profession, phrase, gender)
                            VALUES(@firstName, @lastName,@secondLastName ,@birthDate, @profession, @phrase, @gender)";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@firstName", t.FirstName);
            command.Parameters.AddWithValue("@lastName", t.LastName);
            command.Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            command.Parameters.AddWithValue("@birthDate", t.BirthDate);
            command.Parameters.AddWithValue("@profession", t.Profession);
            command.Parameters.AddWithValue("@phrase", t.Pharse);
            command.Parameters.AddWithValue("@gender", t.Gender);

            try
            {
                return ExecuteCommand(command);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT id, firstName, lastName, secondLastName, birthDate, profession, phrase, gender  
                                FROM Lecturer
                                WHERE status = 1";
            SqlCommand command = BasicCommand(query);
            try
            {
                return ExecuteDataTable(command);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Update(Lecturer t)
        {
            string query = @"UPDATE Lecturer
                SET firstName = @firstName, lastName = @lastName, secondLastName = @secondLastName,
                                birthDate = @birthDate, profession=@profession, phrase=@phrase, gender=@gender, lastUpdate=GETDATE()
                WHERE id = @id";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@firstName", t.FirstName);
            command.Parameters.AddWithValue("@lastName", t.LastName);
            command.Parameters.AddWithValue("@secondLastName", t.SecondLastName);
            command.Parameters.AddWithValue("@birthDate", t.BirthDate);
            command.Parameters.AddWithValue("@profession", t.Profession);
            command.Parameters.AddWithValue("@phrase", t.Pharse);
            command.Parameters.AddWithValue("@gender", t.Gender);
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
    }
}
