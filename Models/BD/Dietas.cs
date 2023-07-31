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


    }
}