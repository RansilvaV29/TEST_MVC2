//using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using TEST_MVC2.Models;

namespace TEST_MVC2.Data
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Data Source=MSIGE68;Initial Catalog=Productos;User ID=sa; Password=admin123;TrustServerCertificate=True;";
        public List<Cliente> GetClientes()
        {
            List<Cliente> lst = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Cliente_SelectAll", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["codigo"]),
                        Cedula = rdr["cedula"]?.ToString() ?? string.Empty,
                        Apellidos = rdr["apellidos"]?.ToString() ?? string.Empty,
                        Nombres = rdr["nombres"]?.ToString() ?? string.Empty,
                        FechaNacimiento = rdr["fechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(rdr["fechaNacimiento"]) : DateTime.MinValue,
                        Mail = rdr["mail"]?.ToString() ?? string.Empty,
                        Telefono = rdr["telefono"]?.ToString() ?? string.Empty,
                        Direccion = rdr["direccion"]?.ToString() ?? string.Empty,
                        Estado = rdr["estado"] != DBNull.Value && Convert.ToBoolean(rdr["estado"])
                    };

                    lst.Add(cliente);
                }

                con.Close();
            }
            return lst;
        }
        public int InsertCliente(Cliente cliente)

        {
            int codigoGenerado = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);


                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        codigoGenerado = Convert.ToInt32(rdr["Codigo"]);
                    }
                }
                con.Close();

            }
            return codigoGenerado;
        }
        public Cliente GetClienteById(int codigo)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Cliente_SelectById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"]?.ToString() ?? string.Empty,
                        Apellidos = rdr["Apellidos"]?.ToString() ?? string.Empty,
                        Nombres = rdr["Nombres"]?.ToString() ?? string.Empty,
                        FechaNacimiento = rdr["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(rdr["FechaNacimiento"]) : DateTime.MinValue,
                        Mail = rdr["Mail"]?.ToString() ?? string.Empty,
                        Telefono = rdr["Telefono"]?.ToString() ?? string.Empty,
                        Direccion = rdr["Direccion"]?.ToString() ?? string.Empty,
                        Estado = rdr["Estado"] != DBNull.Value && Convert.ToBoolean(rdr["Estado"])
                    };
                }
                rdr.Close();
                con.Close();
            }
            return cliente;
        }



        public void UpdateCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                con.Close();
            }
        }
        public void DeleteCliente(int codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                con.Close();
            }
        }
    }
}

