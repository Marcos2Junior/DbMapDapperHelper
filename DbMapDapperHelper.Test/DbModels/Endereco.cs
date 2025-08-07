using DbMapDapperHelper.Interfaces;

namespace DbMapDapperHelper.Test.DbModels
{
    [NomeTabela("tbl_endereco")]
    public class Endereco : IDbMapModel
    {
        [NomeColuna("id_endereco")]
        public int Id { get; set; }

        [NomeColuna("rua_endereco")]
        public string Rua { get; set; }

        [NomeColuna("cidade_endereco")]
        public string Cidade { get; set; }
    }
}
