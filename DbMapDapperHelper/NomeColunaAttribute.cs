namespace DbMapDapperHelper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NomeColunaAttribute(string nome) : Attribute
    {
        public string Nome { get; init; } = nome;
    }
}
