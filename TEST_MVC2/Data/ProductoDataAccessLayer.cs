using System.Data;
using Microsoft.Data.SqlClient;
using TEST_MVC2.Models;

namespace TEST_MVC2.Data
{
    public class ProductoDataAccessLayer
    {
        string connectionString = "Data Source=MSIGE68;Initial Catalog=Productos;User ID=sa; Password=admin123;TrustServerCertificate=True;";

        public List<Producto> GetProductos()
        {
            List<Producto> lst = new List<Producto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Producto_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Producto producto = new Producto
                    {
                        ProductoID = Convert.ToInt32(rdr["ProductoID"]),
                        Nombre = rdr["Nombre"]?.ToString() ?? string.Empty,
                        Descripcion = rdr["Descripcion"]?.ToString() ?? string.Empty
                    };

                    lst.Add(producto);
                }

                con.Close();
            }
            return lst;
        }

        public int InsertProducto(Producto producto)
        {
            int productoID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Producto_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        productoID = Convert.ToInt32(rdr["ProductoID"]);
                    }
                }
                con.Close();
            }
            return productoID;
        }

        public Producto GetProductoById(int id)
        {
            Producto producto = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Producto_SelectById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductoID", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    producto = new Producto
                    {
                        ProductoID = Convert.ToInt32(rdr["ProductoID"]),
                        Nombre = rdr["Nombre"]?.ToString() ?? string.Empty,
                        Descripcion = rdr["Descripcion"]?.ToString() ?? string.Empty
                    };
                }
                rdr.Close();
                con.Close();
            }
            return producto;
        }

        public void UpdateProducto(Producto producto)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Producto_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Producto_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductoID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
