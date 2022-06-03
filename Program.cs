using System.Data.SQLite;
using System.Text.RegularExpressions;

bool checaExpressoes(dynamic codigo)
{
    foreach (var expressao in codigo.expressoes)
        if (!Regex.IsMatch(codigo.trechoCodigo, expressao.Item1))
            throw new Exception(expressao.Item2);

    return true;
}

void analisaTrecho(dynamic codigo)
{
    try
    {
        if (checaExpressoes(codigo))
            Console.WriteLine("Trecho compilado com sucesso");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        LogErroDeSintaxe(codigo);
    }
}

var trechoIf = new If("if(numero1outroNumero)");
analisaTrecho(trechoIf);

CreateDatabase();
//https://web.archive.org/web/20190910153157/http://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
void LogErroDeSintaxe(dynamic codigo)
{
    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=AnalisadorSintatico.sqlite;Version=3;");
    m_dbConnection.Open();
    //string sql = $"select * from tipos_estrutura where nome_estrutura = '{codigo.GetType().ToString()}'";
    string sql = $"INSERT INTO log_analises_erro (id_tipo_estrutura, trecho_codigo) VALUES ((select id_tipo_estrutura from tipos_estrutura where nome_estrutura = '{codigo.GetType().ToString()}'), '{codigo.trechoCodigo}')";
    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
    command.ExecuteNonQuery();
    // command.ExecuteNonQuery();
    // SQLiteDataReader reader = command.ExecuteReader();
    // while (reader.Read())
    //    Console.WriteLine("Id: " + reader["id_tipo_estrutura"] + "\tNome: " + reader["nome_estrutura"]);
}

void CreateDatabase()
{
    //SQLiteConnection.CreateFile("AnalisadorSintatico.sqlite");//Cria o banco
    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=AnalisadorSintatico.sqlite;Version=3;");
    m_dbConnection.Open();

    //string sql = "CREATE TABLE tipos_estrutura(id_tipo_estrutura INT, nome_estrutura VARCHAR(45));CREATE TABLE log_analises_erro(id_analise_erro INT, id_tipo_estrutura INT);CREATE TABLE log_analises_sucesso(id_analise_sucesso INT, id_tipo_estrutura INT);";
    //string sql = "INSERT INTO tipos_estrutura VALUES (2, 'For')";
    //string sql = "delete from tipos_estrutura where id_tipo_estrutura = 1";
    //string sql = "drop table log_analises_sucesso";
    // string sql = "CREATE TABLE log_analises_sucesso(id_analise_sucesso INTEGER PRIMARY KEY AUTOINCREMENT, id_tipo_estrutura INT, trecho_codigo VARCHAR(100))";
    // SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
    // command.ExecuteNonQuery();
}

class If
{
    public If(string trechoCodigo)
    {
        this.trechoCodigo = trechoCodigo;
    }

    public string trechoCodigo { get; set; }
    public List<(string, string)> expressoes = new List<(string, string)>
    {
        (@"if", "Uma estrutura de condição deve começar com a palavra if"),
        (@"if\(", "Erro de sintaxe, \"(\" esperado"),
        (@"if\([a-zA-Z0-9]{1,}", "Para fins de comparação, deve-se inicialmente apresentar uma variavel"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|<", "Erro de sintaxe, operador de condição esperado"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|< [a-zA-Z0-9]{1,}", "Erro de sintaxe, devem haver dois elementos na comparação"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|< [a-zA-Z0-9]{1,}\)", "Erro de sintaxe, \")\" esperado"),
    };
}

class For
{
    public For(string trechoCodigo)
    {
        this.trechoCodigo = trechoCodigo;
    }

    public string trechoCodigo { get; set; }
    public List<(string, string)> expressoes = new List<(string, string)>
    {
        (@"if", "Uma estrutura de condição deve começar com a palavra if"),
        (@"if\(", "Erro de sintaxe, \"(\" esperado"),
        (@"if\([a-zA-Z0-9]{1,}", "Para fins de comparação, deve-se inicialmente apresentar uma variavel"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|<", "Erro de sintaxe, operador de condição esperado"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|< [a-zA-Z0-9]{1,}", "Erro de sintaxe, devem haver dois elementos na comparação"),
        (@"if\([a-zA-Z0-9]{1,} <=|>=|==|!=|>|< [a-zA-Z0-9]{1,}\)", "Erro de sintaxe, \")\" esperado"),
    };
}