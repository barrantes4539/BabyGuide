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
    public class Expediente
    {
        public int idExpediente(int id)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVeridExpediente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                int idexp = (int)command.ExecuteScalar();

                return idexp;
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
        public List<AlergiasBebe> VerAlergiasBebe(int idexp)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerAlergiasBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<AlergiasBebe> List = new List<AlergiasBebe>();

                foreach (DataRow row in dataTable.Rows)
                {
                    AlergiasBebe Data = new AlergiasBebe
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idAlergia"].ToString(),
                        fecha = row["FechaAlergia"].ToString(),
                        // ...
                    };
                    DateTime fechaDateTime = DateTime.Parse(Data.fecha);

                    // Formatear la fecha como una cadena con el formato deseado ("dd/MM/yyyy")
                    string fechaFormateada = fechaDateTime.ToString("dd/MM/yyyy");

                    // Luego, puedes asignar la fecha formateada de vuelta a la propiedad "fecha" en tu objeto "Data"
                    Data.fecha = fechaFormateada;
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<AlergiasBebe>();
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
        public List<VacunasBebe> VerVacunasBebe(int idexp)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerVacunasBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<VacunasBebe> List = new List<VacunasBebe>();

                foreach (DataRow row in dataTable.Rows)
                {
                    VacunasBebe Data = new VacunasBebe
                    {
                        name = row["Nombre"].ToString(),
                        id = row["idVacuna"].ToString(),
                        fecha = row["FechaVacuna"].ToString(),
                        // ...
                    };
                    DateTime fechaDateTime = DateTime.Parse(Data.fecha);

                    // Formatear la fecha como una cadena con el formato deseado ("dd/MM/yyyy")
                    string fechaFormateada = fechaDateTime.ToString("dd/MM/yyyy");

                    // Luego, puedes asignar la fecha formateada de vuelta a la propiedad "fecha" en tu objeto "Data"
                    Data.fecha = fechaFormateada;
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<VacunasBebe>();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Diagnosticos> VerDiagnosticosBebe(int idexp)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerDiagnosticosBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<Diagnosticos> List = new List<Diagnosticos>();

                foreach (DataRow row in dataTable.Rows)
                {

                    Diagnosticos Data = new Diagnosticos
                    {
                        name = row["Padecimientos"].ToString(),
                        id = row["idDiagnostico"].ToString(),
                        fecha = row["Fecha"].ToString(),
                        // ...
                    };
                    DateTime fechaDateTime = DateTime.Parse(Data.fecha);

                    // Formatear la fecha como una cadena con el formato deseado ("dd/MM/yyyy")
                    string fechaFormateada = fechaDateTime.ToString("dd/MM/yyyy");

                    // Luego, puedes asignar la fecha formateada de vuelta a la propiedad "fecha" en tu objeto "Data"
                    Data.fecha = fechaFormateada;
                    List.Add(Data);
                }
                return List;
            }
            catch (Exception)
            {
                return new List<Diagnosticos>();
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
        public List<Medicamentos> VerMedicamentosBebe(int idexp)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spVerMedicamentosBebe", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", idexp);
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
        public void Modificar(int id, int idexp, string nom, string ape1, string ape2, string nac, string gen, int gest, double alt, 
            double pes, List<AlergiasBebe> alergiasagregar, List<Alergias> alergiaseliminar,
            List<VacunasBebe> vacunasagregar, List<Vacunas> vacunaseliminar,
            List<Diagnosticos> diagnosticosagregar, List<Diagnosticos> diagnosticoseliminar)
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                string connectionString = Conexion.cn;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("spModificarExpediente", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nom", nom);
                command.Parameters.AddWithValue("@ape1", ape1);
                command.Parameters.AddWithValue("@ape2", ape2);
                command.Parameters.AddWithValue("@nac", nac);
                command.Parameters.AddWithValue("@gen", gen);
                command.Parameters.AddWithValue("@gest", gest);
                command.Parameters.AddWithValue("@alt", alt);
                command.Parameters.AddWithValue("@pes", pes);
                command.ExecuteNonQuery();

                if (alergiasagregar != null)
                {
                    foreach (var elemento in alergiasagregar)
                    {
                        command = new SqlCommand("spIngresarAlergias", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@idaler", elemento.id);
                        command.Parameters.AddWithValue("@fecha", elemento.fecha);
                        command.ExecuteNonQuery();
                    }
                }
                if (alergiaseliminar != null)
                {
                    foreach (var elemento in alergiaseliminar)
                    {
                        command = new SqlCommand("spEliminarAlergias", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@idaler", elemento.id);
                        command.ExecuteNonQuery();
                    }
                }
                if (vacunasagregar != null)
                {
                    foreach (var elemento in vacunasagregar)
                    {
                        command = new SqlCommand("spIngresarVacunas", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@idvac", elemento.id);
                        command.Parameters.AddWithValue("@fecha", elemento.fecha);
                        command.ExecuteNonQuery();
                    }
                }
                if (vacunaseliminar != null)
                {
                    foreach (var elemento in vacunaseliminar)
                    {
                        command = new SqlCommand("spEliminarVacunas", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@idvac", elemento.id);
                        command.ExecuteNonQuery();
                    }
                }
                if (diagnosticosagregar != null)
                {
                    foreach (var elemento in diagnosticosagregar)
                    {
                        command = new SqlCommand("spIngresarDiagnosticos", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@pade", elemento.name);
                        command.Parameters.AddWithValue("@fecha", elemento.fecha);
                        command.ExecuteNonQuery();
                    }
                }
                if (diagnosticoseliminar != null)
                {
                    foreach (var elemento in diagnosticoseliminar)
                    {
                        command = new SqlCommand("spEliminarDiagnosticos", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idexp", idexp);
                        command.Parameters.AddWithValue("@pade", elemento.name);
                        command.ExecuteNonQuery();
                    }
                }
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