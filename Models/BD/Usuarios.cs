using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace BabyGuide.Models.BD
{
    public class Usuarios
    {

        public string VerificarCredenciales(string correoE, string password)
        {
            string mensaje = "";

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("VerificarCredenciales", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_correoE", correoE);
                    cmd.Parameters.AddWithValue("@p_password", password);

                    SqlParameter outputParam = new SqlParameter("@p_mensaje", SqlDbType.VarChar, 100);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@p_mensaje"].Value.ToString();
                }
            }

            return mensaje;
        }

        // Función para registrar un nuevo usuario
        public string RegistrarUsuario(string nombre, string apellido1, string apellido2, string correoE, string password)
        {
            string mensaje = "";

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("RegistrarUsuario", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_nombre", nombre);
                    cmd.Parameters.AddWithValue("@p_apellido1", apellido1);
                    cmd.Parameters.AddWithValue("@p_apellido2", apellido2);
                    cmd.Parameters.AddWithValue("@p_correoE", correoE);
                    cmd.Parameters.AddWithValue("@p_password", password);

                    SqlParameter outputParam = new SqlParameter("@p_mensaje", SqlDbType.VarChar, 100);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@p_mensaje"].Value.ToString();
                }
            }

            return mensaje;
        }
        public int IdUsuarioLogueado(string correoUsuarioLogueado)
        {
            int idUsuario = 0; // Inicializar a 0 como valor predeterminado.

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT idUsuario FROM BabyGuide.Usuarios WHERE CorreoE = @correoUsuario", oConexion);
                    cmd.Parameters.AddWithValue("@correoUsuario", correoUsuarioLogueado);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        idUsuario = Convert.ToInt32(reader["idUsuario"]);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones

                Console.WriteLine(ex.Message);
            }

            return idUsuario;
        }


    }
}
