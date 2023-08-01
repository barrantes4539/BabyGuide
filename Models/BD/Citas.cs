using BabyGuide.Models.Listas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.BD
{
    public class Citas
    {
        public int idExpediente(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVeridExpediente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                int idexp = (int)command.ExecuteScalar();

                return idexp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<CitasM> VerCitasBebe(int idexp)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerCitas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<CitasM> List = new List<CitasM>();

                foreach (DataRow row in dataTable.Rows)
                {
                    CitasM Data = new CitasM
                    {
                        titulo = row["Titulo"].ToString(),
                        id = row["idCita"].ToString(),
                        fecha = row["Fecha"].ToString(),
                        descripcion = row["Descripcion"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<CitasM>();
            }
            finally
            {
                connection.Close();
            }
        }
 
        public void CrearCita(int idexp, string titu, string desc, string fecha)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spIngresarCitas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
                command.Parameters.AddWithValue("@titu", titu);
                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@desc", desc);
                

                command.ExecuteNonQuery();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public void EliminarCita(int idexp, int idcita)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spEliminarCitas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idexp", idexp);
                command.Parameters.AddWithValue("@idcita", idcita);


                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
    
}