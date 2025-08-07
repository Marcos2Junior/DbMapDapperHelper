using DbMapDapperHelper.Test.DbModels;
using FluentAssertions;

namespace DbMapDapperHelper.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Deve_retornar_nome_coluna_corretamente()
        {
            var _map = DbMap.Map<Usuario>();

            _map.Col(x => x.Nome).Should().Be("t1.nome_usuario");
            _map.ColAs(x => x.Email).Should().Be("t1.email_usuario as Email");
            _map.NCol(x => x.Id).Should().Be("id_usuario");
            _map.NomePropriedade(x => x.Nome).Should().Be(nameof(Usuario.Nome));
        }

        [Fact]
        public void Deve_retornar_join_com_colunas_certas()
        {
            var (usuario, endereco) = DbMap.Map<Usuario, Endereco>();
            var join = $@"SELECT 
            {usuario.ColAs(x => x.Email)}, 
            {endereco.ColAs(x => x.Cidade)}
            FROM {usuario}
            INNER JOIN {endereco} ON {usuario.Col(x => x.Id)} = {endereco.Col(x => x.Id)}";

            join.Should().Contain("t1.email_usuario as Email");
            join.Should().Contain("t2.cidade_endereco as Cidade");
            join.Should().Contain("INNER JOIN tbl_endereco t2 ON t1.id_usuario = t2.id_endereco");
            join.Should().Contain("FROM tbl_usuario t1");
        }

        [Fact]
        public void Deve_retornar_todas_as_colunas_com_as()
        {
            var _map = DbMap.Map<Usuario>();

            var resultado = _map.SelecionaTodasColunaAs();

            resultado.Should().Contain("t1.nome_usuario as Nome");
            resultado.Should().Contain("t1.email_usuario as Email");
            resultado.Should().Contain("t1.id_usuario as Id");
        }

        [Fact]
        public void Deve_retornar_splitOn_corretamente()
        {
            var _map = DbMap.Map<Usuario>();

            var (sql, splitOn) = _map.SelecionaTodasColunaAsComSplitOn();

            sql.Should().Contain("t1.nome_usuario as Nome");
            splitOn.Should().Be("Id");
        }

        [Fact]
        public void Deve_ignorar_propriedades_se_passadas()
        {
            var _map = DbMap.Map<Usuario>();

            var (sql, _) = _map.SelecionaTodasColunaAsComSplitOn(x => x.Id);

            sql.Should().NotContain("id_usuario");
            sql.Should().Contain("nome_usuario");
            sql.Should().Contain("email_usuario");
        }
    }
}
