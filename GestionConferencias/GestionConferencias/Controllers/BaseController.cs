using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GestionConferencias.Controllers
{
    public class BaseController
    {

        static string connectionStr = @"Server=LAPTOP-OTLQ23JJ\SQLEXPRESS;Database=GestionConferencia;User Id=sa;Password=j4r4n4123;";

        public static SqlCommand BasicCommand()
        {
            SqlConnection connection = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }

        public static SqlCommand BasicCommand(string query)
        {
            SqlConnection connection = new SqlConnection(connectionStr);
            SqlCommand command = new SqlCommand(query, connection);
            return command;
        }

        public static int ExecuteCommand(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public DataTable ExecuteDataTable(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
                    
        }

        public SqlDataReader ExecuteDataReader(SqlCommand command)
        {
            SqlDataReader reader = null;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return reader;
        }
    }
}
