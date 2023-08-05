using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
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

        //Indicador 
        public int TotalUsuarios()
        {
            int totalUsuarios = 0; // Inicializar a 0 como valor predeterminado.

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(idUsuario) AS TotalUsuarios FROM [BabyGuide].[Usuarios]", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        totalUsuarios = Convert.ToInt32(reader["TotalUsuarios"]);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
            }

            return totalUsuarios;
        }
        public string CodigoVerificacionLogin()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

        public bool InsertarCodigoVerificacion(string correoUsuario, string codigoVerificacion, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE BabyGuide.Usuarios SET CodigoVer = @codigoVerificacion WHERE CorreoE = @correoUsuario", oConexion);
                    cmd.Parameters.AddWithValue("@codigoVerificacion", codigoVerificacion);
                    cmd.Parameters.AddWithValue("@correoUsuario", correoUsuario);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

        public string CodigoVerificacion(string correoUsuario)
        {
            string codigoVerificacion = null;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT CodigoVer FROM BabyGuide.Usuarios WHERE CorreoE = @correoUsuario", oConexion);
                    cmd.Parameters.AddWithValue("@correoUsuario", correoUsuario);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        codigoVerificacion = reader["CodigoVer"].ToString();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }

            return codigoVerificacion;
        }
        public bool EliminarCodigo(string correoCliente)
        {
            bool resultado = false;


            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE BabyGuide.Usuarios SET CodigoVer = NULL WHERE CorreoE = @CorreoUs", oConexion);
                    cmd.Parameters.AddWithValue("@CorreoUs", correoCliente);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                resultado = false;

            }

            return resultado;
        }

        public int? idBebe(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVeridBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                int? idexp = (int?)command.ExecuteScalar();

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


    }
}
