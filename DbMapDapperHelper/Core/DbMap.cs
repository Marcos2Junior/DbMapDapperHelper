using DbMapDapperHelper.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace DbMapDapperHelper.Core
{
    public class DbMap<T> : IDbMap<T> where T : IDbMapModel, new()
    {

        public T Ref { get; init; }

        private readonly string _alias;

        internal DbMap(uint segmentoAlias = 1)
        {
            _alias = $"t{segmentoAlias}";
            Ref = new T();
        }


        public string TabelaAs => $"{NomeTabelaAttribute().Nome} {_alias}";


        public override string ToString() => TabelaAs;


        public string Tabela => NomeTabelaAttribute().Nome;


        public string Col<TProp>(Expression<Func<T, TProp>> expr) => $"{_alias}.{NCol(expr)}";


        public string ColAs<TProp>(Expression<Func<T, TProp>> expr)
        {
            var propInfo = PropertyInfo(expr);
            var attr = NomeColunaAttribute(propInfo);
            return $"{_alias}.{attr.Nome} as {propInfo.Name}";
        }


        public string NCol<TProp>(Expression<Func<T, TProp>> expr)
        {
            var propInfo = PropertyInfo(expr);
            return NomeColunaAttribute(propInfo).Nome;
        }


        public string NomePropriedade<TProp>(Expression<Func<T, TProp>> expr) => PropertyInfo(expr).Name;


        public string SelecionaTodasColunaAs()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var columns = props.Select(prop => $"{_alias}.{NomeColunaAttribute(prop).Nome} as {prop.Name}");
            return string.Join(", ", columns);
        }


        public (string select, string splitOn) SelecionaTodasColunaAsComSplitOn(params Expression<Func<T, object>>[] ignorarPropriedades)
        {
            var ignorarNomes = new HashSet<string>(
                ignorarPropriedades.Select(expr => PropertyInfo(expr).Name)
            );

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => !ignorarNomes.Contains(p.Name))
                                 .ToArray();

            if (props.Length == 0)
                throw new InvalidOperationException("Nenhuma propriedade restante após aplicar os filtros de exclusão.");

            var select = props.Select(prop => $"{_alias}.{NomeColunaAttribute(prop).Nome} as {prop.Name}");
            var splitOn = props.Last().Name;

            return (string.Join(", ", select), splitOn);
        }


        /// <summary>
        /// Retorna o atributo [NomeTabela] da entidade T.
        /// Lança exceção se o atributo não estiver presente.
        /// </summary>
        private static NomeTabelaAttribute NomeTabelaAttribute()
        {
            return typeof(T).GetCustomAttribute<NomeTabelaAttribute>()
                ?? throw new InvalidOperationException($"A classe '{typeof(T).Name}' precisa do atributo [NomeTabela].");
        }

        /// <summary>
        /// Retorna o atributo [NomeColuna] da propriedade informada.
        /// Lança exceção se o atributo não estiver presente.
        /// </summary>
        /// <param name="propertyInfo">Propriedade a ser avaliada</param>
        private static NomeColunaAttribute NomeColunaAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<NomeColunaAttribute>()
                ?? throw new Exception($"A propriedade '{propertyInfo.Name}' precisa de [NomeColuna].");
        }

        /// <summary>
        /// Extrai a PropertyInfo a partir de uma expressão lambda do tipo x => x.Propriedade.
        /// Suporta também conversões implícitas como (object)x.Propriedade.
        /// </summary>
        /// <typeparam name="TProp">Tipo da propriedade</typeparam>
        /// <param name="expr">Expressão lambda que aponta para a propriedade</param>
        /// <returns>Informações da propriedade</returns>
        private static PropertyInfo PropertyInfo<TProp>(Expression<Func<T, TProp>> expr)
        {
            if (expr.Body is not MemberExpression memberExpr)
            {
                if (expr.Body is UnaryExpression unary && unary.Operand is MemberExpression inner)
                {
                    memberExpr = inner;
                }
                else
                {
                    throw new ArgumentException("Expressão inválida");
                }
            }

            return memberExpr.Member as PropertyInfo
                ?? throw new ArgumentException("Expressão não aponta para uma propriedade");
        }
    }
}
