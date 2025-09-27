using Microsoft.Data.SqlClient;

namespace ControleChamadosMVC.DAO
{
    public static class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST; Initial Catalog=AULADB; Integrated Security=true; TrustServerCertificate=true";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
