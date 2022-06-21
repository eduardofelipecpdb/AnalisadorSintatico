namespace AnalisadorSintatico
{
    class Estrutura 
    {
        public string? trechoCodigo { get; set; }
    }
    class If : Estrutura
    {
        public If(string trechoCodigo)
        {
            this.trechoCodigo = trechoCodigo;
        }
        public readonly int tipoEstrutura = 1;
    }

    class For : Estrutura
    {
        public For(string trechoCodigo)
        {
            this.trechoCodigo = trechoCodigo;
        }
        public readonly int tipoEstrutura = 2;
    }
}