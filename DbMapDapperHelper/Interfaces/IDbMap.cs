using System.Linq.Expressions;

namespace DbMapDapperHelper.Interfaces
{
    public interface IDbMap<T> where T : IDbMapModel, new()
    {
        /// <summary>
        /// Instância de referência para acesso às propriedades do tipo T.
        /// </summary>
        T Ref { get; }
        /// <summary>
        /// Retorna o nome da tabela definido no atributo [NomeTabela] com alias (ex: "MinhaTabela t1").
        /// </summary>
        string TabelaAs { get; }
        /// <summary>
        /// Retorna apenas o nome da tabela definido no atributo [NomeTabela].
        /// </summary>
        string Tabela { get; }
        /// <summary>
        /// Retorna a coluna mapeada no banco com alias (ex: "t1.NomeColuna") a partir de uma expressão.
        /// </summary>
        /// <typeparam name="TProp">Tipo da propriedade</typeparam>
        /// <param name="expr">Expressão que aponta para a propriedade</param>
        string Col<TProp>(Expression<Func<T, TProp>> expr);
        /// <summary>
        /// Retorna a coluna com alias e com alias de retorno para uso no SELECT (ex: "t1.NomeColuna as NomePropriedade").
        /// </summary>
        /// <typeparam name="TProp">Tipo da propriedade</typeparam>
        /// <param name="expr">Expressão que aponta para a propriedade</param>
        string ColAs<TProp>(Expression<Func<T, TProp>> expr);
        /// <summary>
        /// Retorna apenas o nome da coluna no banco (sem alias), extraído do atributo [NomeColuna].
        /// </summary>
        /// <typeparam name="TProp">Tipo da propriedade</typeparam>
        /// <param name="expr">Expressão que aponta para a propriedade</param>
        string NCol<TProp>(Expression<Func<T, TProp>> expr);
        /// <summary>
        /// Retorna o nome da propriedade C# a partir da expressão informada.
        /// </summary>
        /// <typeparam name="TProp">Tipo da propriedade</typeparam>
        /// <param name="expr">Expressão que aponta para a propriedade</param>
        string NomePropriedade<TProp>(Expression<Func<T, TProp>> expr);
        /// <summary>
        /// Retorna todas as colunas da entidade com alias e alias de retorno para SELECT (ex: "t1.Cod as Cod").
        /// </summary>
        string SelecionaTodasColunaAs();
        /// <summary>
        /// Gera a seleção de todas as colunas da entidade <typeparamref name="T"/> com alias e alias-as-nome,
        /// retornando também a última propriedade (pós-filtro) como splitOn para uso com Dapper.
        /// Permite ignorar propriedades específicas usando expressões lambda.
        /// </summary>
        /// <param name="ignorarPropriedades">Expressões das propriedades que não devem ser incluídas na seleção.</param>
        /// <returns>Tuple com a seleção (`select`) e nome da última propriedade para uso no `splitOn`.</returns>
        (string select, string splitOn) SelecionaTodasColunaAsComSplitOn(params Expression<Func<T, object>>[] ignorarPropriedades);
    }
}
