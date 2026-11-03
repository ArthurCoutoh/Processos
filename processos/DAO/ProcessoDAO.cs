using processos.Configs;
using processos.Model;
using MySql.Data.MySqlClient;

namespace processos.DAO;

public class ProcessoDAO
{
    private readonly Conexao _conexao;

    public ProcessoDAO(Conexao conexao)
    {
        _conexao = conexao;
    }

    // AULA 08/09: lista os registros.
    public List<Processo> Listar()
    {
        var lista = new List<Processo>();
        using var conn = _conexao.GetConnection();
        using var comando = _conexao.CreateCommand("SELECT * FROM processos ORDER BY id_pro DESC;", conn);
        using var leitor = comando.ExecuteReader();

        while (leitor.Read())
        {
            lista.Add(MapearProcesso(leitor));
        }

        return lista;
    }

    // PDF EDIÇÃO: busca um único processo. Se não existir, retorna null.
    public Processo? ObterPorId(int id)
    {
        using var conn = _conexao.GetConnection();
        const string sql = @"SELECT id_pro, numero_pro, data_pro, interessado_pro,
                                    assunto_pro, descricao_pro, situacao_pro
                             FROM processos WHERE id_pro = @id";
        using var comando = _conexao.CreateCommand(sql, conn);
        comando.Parameters.AddWithValue("@id", id);
        using var leitor = comando.ExecuteReader();

        if (!leitor.Read())
        {
            return null;
        }

        return MapearProcesso(leitor);
    }

    // Cadastro: necessário porque Cadastro.razor atende as duas rotas.
    public void Inserir(Processo processo)
    {
        using var conn = _conexao.GetConnection();
        const string sql = @"INSERT INTO processos
                            (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro)
                            VALUES (@numero, @data, @interessado, @assunto, @descricao, @situacao)";
        using var comando = _conexao.CreateCommand(sql, conn);
        AdicionarParametros(comando, processo);
        comando.ExecuteNonQuery();
    }

    // PDF EDIÇÃO: UPDATE somente do ID selecionado.
    public void Atualizar(Processo processo)
    {
        using var conn = _conexao.GetConnection();
        const string sql = @"UPDATE processos SET
                                numero_pro = @numero,
                                data_pro = @data,
                                interessado_pro = @interessado,
                                assunto_pro = @assunto,
                                descricao_pro = @descricao,
                                situacao_pro = @situacao
                            WHERE id_pro = @id";
        using var comando = _conexao.CreateCommand(sql, conn);
        AdicionarParametros(comando, processo);
        comando.Parameters.AddWithValue("@id", processo.Id);
        comando.ExecuteNonQuery();
    }

    private static void AdicionarParametros(MySqlCommand comando, Processo processo)
    {
        comando.Parameters.AddWithValue("@numero", processo.Numero);
        comando.Parameters.AddWithValue("@data", (object?)processo.Data ?? DBNull.Value);
        comando.Parameters.AddWithValue("@interessado", processo.Interessado);
        comando.Parameters.AddWithValue("@assunto", processo.Assunto);
        comando.Parameters.AddWithValue("@descricao", processo.Descricao ?? string.Empty);
        comando.Parameters.AddWithValue("@situacao", processo.Situacao);
    }

    // Reaproveita o mapeamento e o DAOHelper da aula 08/09.
    private static Processo MapearProcesso(MySqlDataReader leitor)
    {
        return new Processo
        {
            Id = leitor.GetInt32("id_pro"),
            Numero = DAOHelper.GetString(leitor, "numero_pro"),
            Data = DAOHelper.GetDateTime(leitor, "data_pro"),
            Interessado = DAOHelper.GetString(leitor, "interessado_pro"),
            Assunto = DAOHelper.GetString(leitor, "assunto_pro"),
            Descricao = DAOHelper.GetString(leitor, "descricao_pro"),
            Situacao = DAOHelper.GetString(leitor, "situacao_pro")
        };
    }
}
