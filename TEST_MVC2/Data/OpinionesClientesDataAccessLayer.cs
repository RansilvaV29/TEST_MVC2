using System.Data;
using Microsoft.Data.SqlClient;
using TEST_MVC2.Models;

namespace TEST_MVC2.Data
{
    public class OpinionesClientesDataAccessLayer
    {
        string connectionString = "Data Source=MSIGE68;Initial Catalog=Productos;User ID=sa; Password=admin123;TrustServerCertificate=True;";

        public List<OpinionesClientes> GetOpiniones()
        {
            List<OpinionesClientes> lst = new List<OpinionesClientes>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Opinion_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    OpinionesClientes opinion = new OpinionesClientes
                    {
                        OpinionID = Convert.ToInt32(rdr["OpinionID"]),
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        ProductoID = rdr["ProductoID"] != DBNull.Value ? Convert.ToInt32(rdr["ProductoID"]) : (int?)null,
                        Calificacion = Convert.ToInt32(rdr["Calificacion"]),
                        Comentario = rdr["Comentario"]?.ToString() ?? string.Empty,
                        Fecha = Convert.ToDateTime(rdr["Fecha"])
                    };

                    lst.Add(opinion);
                }

                con.Close();
            }
            return lst;
        }

        public int InsertOpinion(OpinionesClientes opinion)
        {
            int opinionID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Opinion_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", opinion.Codigo);  // Cambiado de ClienteID a Codigo
                cmd.Parameters.AddWithValue("@ProductoID", (object)opinion.ProductoID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", (object)opinion.Comentario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        opinionID = Convert.ToInt32(rdr["OpinionID"]);
                    }
                }
                con.Close();
            }
            return opinionID;
        }


        public OpinionesClientes GetOpinionById(int id)
        {
            OpinionesClientes opinion = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Opinion_SelectById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OpinionID", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    opinion = new OpinionesClientes
                    {
                        OpinionID = Convert.ToInt32(rdr["OpinionID"]),
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        ProductoID = rdr["ProductoID"] != DBNull.Value ? Convert.ToInt32(rdr["ProductoID"]) : (int?)null,
                        Calificacion = Convert.ToInt32(rdr["Calificacion"]),
                        Comentario = rdr["Comentario"]?.ToString() ?? string.Empty,
                        Fecha = Convert.ToDateTime(rdr["Fecha"])
                    };
                }
                rdr.Close();
                con.Close();
            }
            return opinion;
        }

        public void DeleteOpinion(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Opinion_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OpinionID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateOpinion(OpinionesClientes opinion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Opinion_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OpinionID", opinion.OpinionID);
                cmd.Parameters.AddWithValue("@ProductoID", (object)opinion.ProductoID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Calificacion", opinion.Calificacion);
                cmd.Parameters.AddWithValue("@Comentario", (object)opinion.Comentario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}
