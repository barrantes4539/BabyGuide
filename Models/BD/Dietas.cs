using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.BD
{
    public class Dietas
    {
        public string ObtenerPasosPorIngredienteYEtapa(string ingrediente, int etapa)
        {
            string pasos = "";

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ObtenerPasosPorEtapa", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ingrediente", ingrediente);
                    command.Parameters.AddWithValue("@etapa", etapa);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pasos = reader["Pasos"].ToString();
                        }
                    }
                }
            }

            return pasos;
        }

        public List<string> ObtenerListaDeIngredientes()
        {
            List<string> listaIngredientes = new List<string>();

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_ListarIngredientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ingrediente = reader["Ingredientes"].ToString();
                            listaIngredientes.Add(ingrediente);
                        }
                    }
                }
            }

            return listaIngredientes;
        }

        public void CrearDieta(string ingrediente, string pasos, int anioDieta)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_CreacionDieta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ingrediente", ingrediente);
                    command.Parameters.AddWithValue("@pasos", pasos);
                    command.Parameters.AddWithValue("@añoxdieta", anioDieta);

                    command.ExecuteNonQuery();
                }
            }
        }

        public int idUsuarioLogueado(string correo)
        {
            int idUsuario = 0;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();

                    // Utilizar una consulta parametrizada
                    string consulta = "SELECT idUsuario FROM BabyGuide.Usuarios WHERE CorreoE = @CorreoE";
                    SqlCommand ocmdContador = new SqlCommand(consulta, oconexion);

                    // Agregar el parámetro al comando
                    ocmdContador.Parameters.AddWithValue("@CorreoE", correo);

                    // Ejecutar la consulta y obtener el resultado
                    object resultado = ocmdContador.ExecuteScalar();

                    // Verificar si el resultado es válido y asignarlo a la variable idUsuario
                    if (resultado != null && int.TryParse(resultado.ToString(), out idUsuario))
                    {
                        // El valor se pudo convertir a un entero correctamente.
                        // idUsuario ahora contiene el resultado de la consulta.
                    }
                    else
                    {
                        // No se encontró un usuario con el correo especificado.
                        // Aquí puedes agregar la lógica necesaria en caso de que el correo no exista en la base de datos.
                        idUsuario = 0; // Asignar 0 como valor por defecto si no se encontró el usuario.
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí, si es necesario.
                // Puedes registrar o mostrar un mensaje de error según tus necesidades.
            }

            return idUsuario;
        }



    }
}