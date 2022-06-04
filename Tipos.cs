namespace AnalisadorSintatico
{
    class If
    {
        public If(string trechoCodigo)
        {
            this.trechoCodigo = trechoCodigo;
        }
        public readonly int tipoEstrutura = 1;

        public string trechoCodigo { get; set; }
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
}