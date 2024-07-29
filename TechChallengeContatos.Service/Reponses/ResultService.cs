using System.Text.Json.Serialization;
using TechChallengeContatos.Domain.Entities;

namespace TechChallengeContatos.Service.Reponses
{
    public class ResultService
    {
        #region Ctor

        public ResultService(string mensagem, int statusCode, bool sucesso = false,
            ICollection<DadosInvalidosResult> erros = null)
        {
            Mensagem = mensagem;
            StatusCode = statusCode;
            Sucesso = sucesso;
            Erros = erros ?? new List<DadosInvalidosResult>();
        }

        #endregion

        #region Props

        [JsonPropertyName("mensagem")]
        public string Mensagem { get; protected set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; protected set; }

        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; protected set; }

        [JsonPropertyName("erros")]
        public ICollection<DadosInvalidosResult> Erros { get; protected set; }

        #endregion
    }

    public class ResultService<TDataReturn> : ResultService
    {
        public ResultService(TDataReturn dados, string mensagem, int statusCode, bool sucesso = false, ICollection<DadosInvalidosResult> erros = null)
            : base(mensagem, statusCode, sucesso, erros)
        {
            Dados = dados;
        }

        [JsonPropertyName("dados")]
        public TDataReturn Dados { get; private set; }
    }
}