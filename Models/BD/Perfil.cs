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
        public void AgregarBebe(string nom, string ape1, string ape2, int gen, string nac, int gest)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spIngresarBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@ape1", ape1);
                command.Parameters.AddWithValue("@ape2", ape2);
                command.Parameters.AddWithValue("@gen", gen);
                command.Parameters.AddWithValue("@nac", nac);
                command.Parameters.AddWithValue("@gest", gest);
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