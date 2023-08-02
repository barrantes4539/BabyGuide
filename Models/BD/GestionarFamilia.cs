using BabyGuide.Models.Listas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BabyGuide.Models.BD
{
    public class GestionarFamilia
    {
       
        public List<Familia> VerFamilia(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerFamilia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idb", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Familia> List = new List<Familia>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Familia Data = new Familia
                    {
                        name = row["Nombre"].ToString(),
                        rol = row["Rol"].ToString(),
                        id = row["idUsuario"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Familia>();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Roles> VerRoles()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerRoles", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Roles> List = new List<Roles>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Roles Data = new Roles
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idRoll"].ToString(),
                        // ...
                    };
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Roles>();
            }
            finally
            {
                connection.Close();
            }
        }
        public void ModificarRol(int idu, int idr, int idb)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spModificarFamilia", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idu", idu);
                command.Parameters.AddWithValue("@idr", idr);
                command.Parameters.AddWithValue("@idb", idb);
                

                command.ExecuteNonQuery();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
    
}