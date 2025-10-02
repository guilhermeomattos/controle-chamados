using ControleChamadosMVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ControleChamadosMVC.DAO;

public class ChamadoDAO
{
    private SqlParameter[] CriaParametros(ChamadoViewModel chamado)
    {
        SqlParameter[] p = new SqlParameter[7];
        p[0] = new SqlParameter("id", chamado.Id);
        p[1] = new SqlParameter("dataAbertura", chamado.DataAbertura);
        p[2] = new SqlParameter("descricaoProblema", chamado.DescricaoProblema);
        p[3] = new SqlParameter("descricaoAtendimento",
                                chamado.DescricaoAtendimento ?? (object)DBNull.Value);
        p[4] = new SqlParameter("dataAtendimento",
                                chamado.DataAtendimento ?? (object)DBNull.Value);
        p[5] = new SqlParameter("situacao", chamado.Situacao);
        p[6] = new SqlParameter("usuarioId",
                                chamado.UsuarioId ?? (object)DBNull.Value);
        return p;
    }

    public void Inserir(ChamadoViewModel chamado)
    {
        string sql = @"INSERT INTO chamados 
                        (id, dataAbertura, descricaoProblema, descricaoAtendimento, 
                         dataAtendimento, situacao, usuarioId)
                       VALUES 
                        (@id, @dataAbertura, @descricaoProblema, @descricaoAtendimento, 
                         @dataAtendimento, @situacao, @usuarioId)";
        HelperDAO.ExecutaSQL(sql, CriaParametros(chamado));
    }

    public void Alterar(ChamadoViewModel chamado)
    {
        string sql = @"UPDATE chamados 
                       SET dataAbertura = @dataAbertura,
                           descricaoProblema = @descricaoProblema,
                           descricaoAtendimento = @descricaoAtendimento,
                           dataAtendimento = @dataAtendimento,
                           situacao = @situacao,
                           usuarioId = @usuarioId
                       WHERE id = @id";
        HelperDAO.ExecutaSQL(sql, CriaParametros(chamado));
    }

    public void Excluir(int id)
    {
        string sql = "DELETE FROM chamados WHERE id = @id";
        SqlParameter[] p = { new SqlParameter("id", id) };
        HelperDAO.ExecutaSQL(sql, p);
    }

    public ChamadoViewModel Consulta(int id)
    {
        string sql = "SELECT * FROM chamados WHERE id = @id";
        SqlParameter[] p = { new SqlParameter("id", id) };
        DataTable tabela = HelperDAO.ExecutaSelect(sql, p);

        if (tabela.Rows.Count == 0)
            return null;
        else
            return MontaModel(tabela.Rows[0]);
    }

    public List<ChamadoViewModel> Listagem()
    {
        string sql = "SELECT * FROM chamados ORDER BY dataAbertura";
        DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
        List<ChamadoViewModel> lista = new List<ChamadoViewModel>();
        foreach (DataRow registro in tabela.Rows)
            lista.Add(MontaModel(registro));

        return lista;
    }

    public int ProximoId()
    {
        string sql = "SELECT ISNULL(MAX(id) + 1, 1) FROM chamados";
        DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
        return Convert.ToInt32(tabela.Rows[0][0]);
    }

    private ChamadoViewModel MontaModel(DataRow registro)
    {
        ChamadoViewModel c = new ChamadoViewModel();
        c.Id = Convert.ToInt32(registro["id"]);
        c.DataAbertura = Convert.ToDateTime(registro["dataAbertura"]);
        c.DescricaoProblema = registro["descricaoProblema"].ToString();
        c.DescricaoAtendimento = registro["descricaoAtendimento"] == DBNull.Value
                                ? null : registro["descricaoAtendimento"].ToString();
        c.DataAtendimento = registro["dataAtendimento"] == DBNull.Value
                                ? (DateTime?)null : Convert.ToDateTime(registro["dataAtendimento"]);
        c.Situacao = Convert.ToInt32(registro["situacao"]);
        c.UsuarioId = registro["usuarioId"] == DBNull.Value
                                ? (int?)null : Convert.ToInt32(registro["usuarioId"]);
        return c;
    }
}
