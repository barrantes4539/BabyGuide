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
    }
}