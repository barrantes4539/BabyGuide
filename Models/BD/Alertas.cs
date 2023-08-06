using BabyGuide.Models.Listas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.BD
{
    public class Alertas
    {

        public List<AlertasBebe> VerAlertas()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerAlertas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", 305340319);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<AlertasBebe> List = new List<AlertasBebe>();

                foreach (DataRow row in dataTable.Rows)
                {
                    AlertasBebe Data = new AlertasBebe
                    {
                        idAlerta = row["idAlerta"].ToString(),
                        Titulo = row["Titulo"].ToString(),
                        Hora = row["Hora"].ToString(),
                        idCategoria = row["idCategoria"].ToString(),
                        Estado = row["Estado"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<AlertasBebe>();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}