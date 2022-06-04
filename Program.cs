using System.Data.SQLite;
using System.Text.RegularExpressions;
using AnalisadorSintatico;
//https://web.archive.org/web/20190910153157/http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/

var trechoIf = new If("if(valor");
analisaTrecho(trechoIf);

void analisaTrecho(dynamic codigo)
{
    try
    {
        if (checaExpressoes(codigo))
            Console.WriteLine("Trecho compilado com sucesso");
        LogAnaliseSucesso(codigo);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        LogAnaliseErro(codigo, ex.Message);
    }
}

bool checaExpressoes(dynamic codigo)
{
    string query = $"select * from expressoes_analise where id_tipo_estrutura = {codigo.tipoEstrutura}";
    var conexao = new SQLiteConnection(SQLiteUtils.stringConexao);
    conexao.Open();
    SQLiteCommand command = new SQLiteCommand(query, conexao);
    var reader = command.ExecuteReader();

    while (reader.Read())
        if (!Regex.IsMatch(codigo.trechoCodigo, reader["expressao"].ToString()))
        {
            var idExpressao = reader["id_expressao"].ToString();
            reader.Close();
            throw new Exception(idExpressao);
        }
        

    return true;
}

void LogAnaliseErro(dynamic codigo, object idExpressao)
{
    string query = $"INSERT INTO log_analises_erro (id_tipo_estrutura, id_analise, trecho_codigo) VALUES ({codigo.tipoEstrutura}, {idExpressao}, '{codigo.trechoCodigo}')";
    SQLiteUtils.ExecutaQueryComum(query);
}

void LogAnaliseSucesso(dynamic codigo)
{
    string query = $"INSERT INTO log_analises_sucesso (id_tipo_estrutura, trecho_codigo) VALUES ({codigo.tipoEstrutura}, {codigo.trechoCodigo})";
    SQLiteUtils.ExecutaQueryComum(query);
}