using ControleChamadosMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleChamadosMVC.DAO
{
    public class UsuarioDAO
    {
        private SqlParameter[] CriaParametros(UsuarioViewModel usuario)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("id", usuario.Id);
            p[1] = new SqlParameter("nome", usuario.Nome);
            return p;
        }

        public void Inserir(UsuarioViewModel usuario)
        {
            string sql = "INSERT INTO usuarios (id, nome) VALUES (@id, @nome)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }

        public void Alterar(UsuarioViewModel usuario)
        {
            string sql = "UPDATE usuarios SET nome = @nome WHERE id = @id";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }

        public void Excluir(int id)
        {
            string sql = "DELETE FROM usuarios WHERE id = @id";
            SqlParameter[] p = { new SqlParameter("id", id) };
            HelperDAO.ExecutaSQL(sql, p);
        }

        public UsuarioViewModel Consulta(int id)
        {
            string sql = "SELECT * FROM usuarios WHERE id = @id";
            SqlParameter[] p = { new SqlParameter("id", id) };
            DataTable tabela = HelperDAO.ExecutaSelect(sql, p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);
        }

        public List<UsuarioViewModel> Listagem()
        {
            string sql = "SELECT * FROM usuarios ORDER BY nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }

        public int ProximoId()
        {
            string sql = "SELECT ISNULL(MAX(id) + 1, 1) FROM usuarios";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }

        private UsuarioViewModel MontaModel(DataRow registro)
        {
            UsuarioViewModel u = new UsuarioViewModel();
            u.Id = Convert.ToInt32(registro["id"]);
            u.Nome = registro["nome"].ToString();
            return u;
        }
    }
}
