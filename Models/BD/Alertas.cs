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

        public List<CategoriasAlertas> VerCategoriasAlertas()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerCategoriasAlertas", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<CategoriasAlertas> List = new List<CategoriasAlertas>();

                foreach (DataRow row in dataTable.Rows)
                {
                    CategoriasAlertas Data = new CategoriasAlertas
                    {
                        idCategoria = row["idCategoria"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<CategoriasAlertas>();
            }
            finally
            {
                connection.Close();
            }
        }

        public void IngresarAlerta(string Titulo, string Hora, int idCategoria, int idBebe)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spIngresarAlerta", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Titulo", Titulo);
                command.Parameters.AddWithValue("@Hora", Hora);
                command.Parameters.AddWithValue("@idCategoria", idCategoria);
                command.Parameters.AddWithValue("@idBebe", idBebe);
                command.Parameters.AddWithValue("@Estado", 1);

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

        public void EliminarAlerta(int idAlerta)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spEliminarAlerta", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idAlerta", idAlerta);

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

        public void ModificarEstado(int idAlerta, int Estado)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spModificarEstadoAlerta", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idAlerta", idAlerta);
                command.Parameters.AddWithValue("@Estado", Estado);

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
    }
}