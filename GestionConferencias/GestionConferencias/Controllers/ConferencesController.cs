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
    public class ConferencesController : BaseController, BaseInterface<Conferences>
    {
        public Conferences Get(int id)
        {
            Conferences p = null;
            string query = @"SELECT id, startDate, startTime, endTime, description, peopleCapacity, latitude, longitude, title, lecturerID
                                FROM Conferences
                                WHERE id = @id";
            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = (SqlDataReader)ExecuteDataReader(command);
                while (reader.Read())
                {
                    p = new Conferences(
                        int.Parse(reader[0].ToString()),
                        DateTime.Parse(reader[1].ToString()),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        int.Parse(reader[5].ToString()),
                        reader[7].ToString(),
                        reader[6].ToString(),
                        reader[8].ToString(),
                        int.Parse(reader[9].ToString())
                        );
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

        public int Delete(Conferences t)
        {
            string query = @"UPDATE Conferences
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

        public int Insert(Conferences t)
        {
            string query = @"INSERT INTO Conferences(startDate, startTime, endTime, description, peopleCapacity, latitude, longitude, title, lecturerID)
                            VALUES(@startDate, @startTime,@endTime ,@description, @peopleCapacity, @latitude, @longitude, @title, @lecturerID)";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@startDate", t.StartDate);
            command.Parameters.AddWithValue("@startTime", t.StartTime);
            command.Parameters.AddWithValue("@endTime", t.EndTime);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@peopleCapacity", t.PeopleCapacity);
            command.Parameters.AddWithValue("@latitude", t.Latitude);
            command.Parameters.AddWithValue("@longitude", t.Longitude);
            command.Parameters.AddWithValue("@title", t.Title);
            command.Parameters.AddWithValue("@lecturerID", t.LecturerID);

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
            string query = @"SELECT id, startDate, startTime, endTime, description, peopleCapacity, latitude, longitude, title, lecturerID
                                FROM Conferences
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

        public int Update(Conferences t)
        {
            string query = @"UPDATE Conferences
                SET startDate = @startDate, startTime = @startTime, endTime = @endTime,
                                description = @description, peopleCapacity=@peopleCapacity, latitude=@latitude, longitude=@longitude, 
                                title=@title, lecturerID=@lecturerID,lastUpdate=GETDATE()
                WHERE id = @id";

            SqlCommand command = BasicCommand(query);
            command.Parameters.AddWithValue("@startDate", t.StartDate);
            command.Parameters.AddWithValue("@startTime", t.StartTime);
            command.Parameters.AddWithValue("@endTime", t.EndTime);
            command.Parameters.AddWithValue("@description", t.Description);
            command.Parameters.AddWithValue("@peopleCapacity", t.PeopleCapacity);
            command.Parameters.AddWithValue("@latitude", t.Latitude);
            command.Parameters.AddWithValue("@longitude", t.Longitude);
            command.Parameters.AddWithValue("@title", t.Title);
            command.Parameters.AddWithValue("@lecturerID", t.LecturerID);
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
