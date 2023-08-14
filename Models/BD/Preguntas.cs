using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BabyGuide.Models.Listas;

namespace BabyGuide.Models.BD
{
    public class Preguntas
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public DataTable BuscarRespuesta(string preg)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spBuscarRespuesta", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pregunta", preg);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    //return ex.InnerException.Message;
                }
                else
                {
                    //return ex.Message;
                }
                DataTable dataTable = new DataTable();
                return dataTable;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Preguntas> VerPreguntas()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spBuscarPreguntas", connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@ced", ced);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Preguntas> List = new List<Preguntas>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Preguntas Data = new Preguntas
                    {
                        Pregunta = row["Pregunta"].ToString(),
                        Respuesta = row["Respuesta"].ToString(),
                        // ...
                    };

                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Preguntas>();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}