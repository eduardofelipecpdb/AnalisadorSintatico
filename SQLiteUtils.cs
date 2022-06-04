using System.Data.SQLite;

namespace AnalisadorSintatico
{
    public static class SQLiteUtils
    {
        public static readonly string stringConexao = "Data Source=AnalisadorSintatico.sqlite;Version=3;";
        public static void ExecutaQueryComum(string query)
        {
            var conexao = new SQLiteConnection(stringConexao);
            conexao.Open();

            SQLiteCommand command = new SQLiteCommand(query, conexao);
            command.ExecuteNonQuery();
            conexao.Close();
        }

        public static SQLiteDataReader executarQuerySelect(string query, SQLiteConnection conexao)
        {
            conexao.Open();

            SQLiteCommand command = new SQLiteCommand(query, conexao);
            var leitor = command.ExecuteReader();

            return leitor;
        }
        public static void CreateDatabase()
        {
            var conexao = new SQLiteConnection(stringConexao);
            //////SQLiteConnection.CreateFile("AnalisadorSintatico.sqlite"); //Cria o banco
            conexao.Open();

            // string sql = "CREATE TABLE tipos_estrutura(id_tipo_estrutura INT, nome_estrutura VARCHAR(45));" +
            //              "CREATE TABLE log_analises_erro(id_analise_erro INTEGER PRIMARY KEY AUTOINCREMENT, id_tipo_estrutura INT, id_analise INT, trecho_codigo VARCHAR(100));" + 
            //              "CREATE TABLE log_analises_sucesso(id_analise_sucesso INTEGER PRIMARY KEY AUTOINCREMENT, id_tipo_estrutura INT, trecho_codigo VARCHAR(100));" +
            //              "CREATE TABLE expressoes_analise(id_expressao INTEGER PRIMARY KEY AUTOINCREMENT, id_tipo_estrutura INT, expressao VARCHAR(200), mensagem_expressao VARCHAR(200));";
            //string sql = "INSERT INTO expressoes_analise (id_tipo_estrutura, expressao, mensagem_expressao) VALUES (1, 'if\\(', 'Erro de sintaxe, \"(\" esperado')";
            //string sql = "delete from expressoes_analise where id_expressao = 15";
            //string sql = "drop table analises";
            string sql = "";

            SQLiteCommand command = new SQLiteCommand(sql, conexao);
            command.ExecuteNonQuery();
            conexao.Close();
        }
    }
}