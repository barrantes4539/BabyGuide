using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BabyGuide.Models.BD
{
    public class Bebes
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public bool Genero { get; set; }
        public string FechaNacimiento { get; set; }
    }
    public class Perfil
    {
        public void AgregarBebe(int idb, int idu,string nom, string ape1, string ape2, string gen, string nac, int gest, string claveBebe)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spIngresarBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idb", idb);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@ape1", ape1);
                command.Parameters.AddWithValue("@ape2", ape2);
                command.Parameters.AddWithValue("@gen", gen);
                command.Parameters.AddWithValue("@nac", nac);
                command.Parameters.AddWithValue("@gest", gest);
                command.Parameters.AddWithValue("@idu", idu);
                command.Parameters.AddWithValue("@claveBebe", claveBebe);
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

        public DataTable CargarPerfil(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerUsuario", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
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

        public string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
    }
}