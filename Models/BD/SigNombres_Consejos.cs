using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BabyGuide.Models.Listas;

namespace BabyGuide.Models.BD
{
    public class SigNombres_Consejos
    {
        public List<string> ListarNombres()
        {
            List<string> nombres = new List<string>();

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                string sqlQuery = "SELECT Nombre FROM [BabyGuide].[SignificadosNombres]";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nombres.Add(reader["Nombre"].ToString());
                        }
                    }
                }
            }

            return nombres;
        }
        public string ObtenerSignificadoPorNombre(string nombre)
        {
            string significado = "";

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_obtener_significado_por_nombre", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", nombre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            significado = reader["Significado"].ToString();
                        }
                    }
                }
            }

            return significado;
        }

        public string ObtenerSignificadoPorNombreC(string nombre)
        {
            string significado = "";

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_obtener_significado_por_nombre", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre", nombre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            significado = reader["Significado"].ToString();
                        }
                    }
                }
            }

            return significado;
        }
        //public SignificadoNombre ObtenerSignificadoPorNombreC(string nombre)
        //{
        //    SignificadoNombre resultado = new SignificadoNombre();

        //    using (SqlConnection connection = new SqlConnection(Conexion.cn))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("sp_obtener_significado_por_nombre", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@nombre", nombre);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    resultado.Nombre = reader["Nombre"].ToString();
        //                    resultado.Significado = reader["Significado"].ToString();
        //                }
        //            }
        //        }
        //    }

        //    return resultado;
        //}

        public void ActualizarSignificado(string nombre, string nuevoSignificado)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                string sqlQuery = "UPDATE [BabyGuide].[SignificadosNombres] SET Significado = @Significado WHERE Nombre = @Nombre";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Significado", nuevoSignificado);
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Consejos> ListarConsejos()
        {
            List<Consejos> consejos = new List<Consejos>();

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_listar_consejos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string titulo = reader["Titulo"].ToString();
                            string descripcion = reader["Descripcion"].ToString();

                            // Crear un objeto Consejo y agregarlo a la lista
                            Consejos consejo = new Consejos
                            {
                                Titulo = titulo,
                                Descripcion = descripcion
                            };
                            consejos.Add(consejo);
                        }
                    }
                }
            }

            return consejos;
        }

        public string ObtenerDescripcionPorTitulo(string titulo)
        {
            string descripcion = null;

            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                string sqlQuery = "SELECT Descripcion FROM [BabyGuide].[Consejos] WHERE Titulo = @Titulo";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", titulo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            descripcion = reader["Descripcion"].ToString();
                        }
                    }
                }
            }

            return descripcion;
        }

        public void ActualizarDescripcion(string titulo, string descripcion)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.cn))
            {
                connection.Open();

                string sqlQuery = "UPDATE [BabyGuide].[Consejos] SET Descripcion = @Descripcion WHERE Titulo = @Titulo";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Titulo", titulo);

                    command.ExecuteNonQuery();
                }
            }
        }



      

    }
}