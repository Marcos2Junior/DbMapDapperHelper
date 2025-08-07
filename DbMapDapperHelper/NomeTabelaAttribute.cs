namespace DbMapDapperHelper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NomeTabelaAttribute(string nome) : Attribute
    {
        public string Nome { get; init; } = nome;
    }
}
