using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BabyGuide.Models.Listas;

namespace BabyGuide.Models.BD
{
    public class BabyGaleria
    {
        public void IngresarMultimedia(int idBebe, string TipoArchivo, byte[] Archivo, string Titulo, string Etapa, string Album)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spIngresarMultimedia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idBebe", idBebe);
                command.Parameters.AddWithValue("@TipoArchivo", TipoArchivo);
                command.Parameters.AddWithValue("@Archivo", Archivo);
                command.Parameters.AddWithValue("@Titulo", Titulo);
                command.Parameters.AddWithValue("@Etapa", Etapa);
                command.Parameters.AddWithValue("@Album", Album);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        public List<AlertasBebe> VerAlertas(int idBebe)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerAlertas", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idBebe", idBebe);
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