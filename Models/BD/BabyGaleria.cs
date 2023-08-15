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

        public List<ListaBabyGaleria> VerBabyGaleria(int idBebe)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerBabyGaleria", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idBebe", idBebe);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<ListaBabyGaleria> List = new List<ListaBabyGaleria>();

                foreach (DataRow row in dataTable.Rows)
                {
                    byte[] imageBytes = (byte[])row["Archivo"]; // Obtener los datos binarios de la imagen
                    string base64String = Convert.ToBase64String(imageBytes); // Convertir a base64

                    ListaBabyGaleria Data = new ListaBabyGaleria
                    {
                        idBebe = row["idBebe"].ToString(),
                        TipoArchivo = row["TipoArchivo"].ToString(),
                        Archivo = base64String,
                        Titulo = row["Titulo"].ToString(),
                        Etapa = row["Etapa"].ToString(),
                        Fecha = row["Fecha"].ToString(),
                        Album = row["Album"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<ListaBabyGaleria>();
            }
            finally
            {
                connection.Close();
            }
        }

        public List<ListaBabyGaleria> VerUltrasonidos(int idBebe)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerUltrasonidos", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idBebe", idBebe);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<ListaBabyGaleria> List = new List<ListaBabyGaleria>();

                foreach (DataRow row in dataTable.Rows)
                {
                    byte[] imageBytes = (byte[])row["Archivo"]; // Obtener los datos binarios de la imagen
                    string base64String = Convert.ToBase64String(imageBytes); // Convertir a base64

                    ListaBabyGaleria Data = new ListaBabyGaleria
                    {
                        idBebe = row["idBebe"].ToString(),
                        TipoArchivo = row["TipoArchivo"].ToString(),
                        Archivo = base64String,
                        Titulo = row["Titulo"].ToString(),
                        Etapa = row["Etapa"].ToString(),
                        Fecha = row["Fecha"].ToString(),
                        Album = row["Album"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<ListaBabyGaleria>();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}