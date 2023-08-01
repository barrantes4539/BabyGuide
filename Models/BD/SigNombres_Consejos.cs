using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

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
    }
}