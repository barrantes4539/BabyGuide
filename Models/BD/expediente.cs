using BabyGuide.Models.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.BD
{
    public class Expediente
    {
        public List<Alergias> VerAlergias()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerAlergias", connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@ced", ced);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Alergias> List = new List<Alergias>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Alergias Data = new Alergias
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idAlergia"].ToString(),
                        // ...
                    };

                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Alergias>();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Vacunas> VerVacunas()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerVacunas", connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@ced", ced);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Vacunas> List = new List<Vacunas>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Vacunas Data = new Vacunas
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idVacuna"].ToString(),
                        // ...
                    };

                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Vacunas>();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Medicamentos> VerMedicamentos()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerMedicamentos", connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@ced", ced);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Medicamentos> List = new List<Medicamentos>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Medicamentos Data = new Medicamentos
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idMedicamento"].ToString(),
                        // ...
                    };

                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Medicamentos>();
            }
            finally
            {
                connection.Close();
            }
        }
        public DataTable CargarExpediente()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerExpediente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    //return ex.InnerException.Message;
                }
                else
                {
                    //return ex.Message;
                }
                DataTable dataTable = new DataTable();
                return dataTable;
            }
            finally
            {
                connection.Close();
            }
        }
    }
    
}