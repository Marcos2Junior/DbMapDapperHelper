using DbMapDapperHelper.Interfaces;

namespace DbMapDapperHelper.Test.DbModels
{
    [NomeTabela("tbl_usuario")]
    internal class Usuario : IDbMapModel
    {
        [NomeColuna("nome_usuario")]
        public string Nome { get; set; }

        [NomeColuna("email_usuario")]
        public string Email { get; set; }

        [NomeColuna("id_usuario")]
        public int Id { get; set; }
    }
}
