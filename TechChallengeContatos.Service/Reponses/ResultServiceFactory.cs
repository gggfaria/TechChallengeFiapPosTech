using System.Net;
using TechChallengeContatos.Domain.Entities;

namespace TechChallengeContatos.Service.Reponses;

public static class ResultServiceFactory
{
    public static ResultService Ok(string mensagem)
    {
        return new ResultService(
            mensagem,
            (int)HttpStatusCode.OK,
            sucesso: true
        );
    }

    public static ResultService Created(string mensagem)
    {
        return new ResultService(
            mensagem,
            (int)HttpStatusCode.Created,
            sucesso: true
        );
    }

    public static ResultService NoContent()
    {
        return new ResultService(null, (int)HttpStatusCode.NoContent, true);
    }

    public static ResultService NoContent(string mensagem)
    {
        return new ResultService(mensagem, (int)HttpStatusCode.NoContent, true);
    }

    public static ResultService BadRequest(string mensagem)
    {
        return new ResultService(
            mensagem,
            (int)HttpStatusCode.BadRequest,
            erros: null
        );
    }

    public static ResultService BadRequest(ICollection<DadosInvalidosResult> erros, string mensagem)
    {
        return new ResultService(
            mensagem,
            (int)HttpStatusCode.BadRequest,
            erros: erros
        );
    }

    public static ResultService NotFound(string mensagem)
    {
        return new ResultService(mensagem, (int)HttpStatusCode.NotFound);
    }

    public static ResultService Forbidden(string mensagem)
    {
        return new ResultService(mensagem, (int)HttpStatusCode.Forbidden);
    }
    
    public static ResultService Unauthorized(string mensagem)
    {
        return new ResultService(mensagem, (int)HttpStatusCode.Unauthorized);
    }


    public static ResultService InternalServerError(string mensagem)
    {
        return new ResultService(mensagem, (int)HttpStatusCode.InternalServerError);
    }
}

/// <summary>
/// Factory for the results service with result data generic - ResponseFactory
/// </summary>
/// <typeparam name="T">type result</typeparam>
public static class ResultServiceFactory<TResult> where TResult : class
{
    public static ResultService<TResult> Ok(TResult dados, string mensagem = null)
    {
        return new ResultService<TResult>(
            statusCode: (int)HttpStatusCode.OK,
            sucesso: true,
            dados: dados,
            mensagem: mensagem
        );
    }


    public static ResultService<TResult> Created(TResult dados, string mensagem = null)
    {
        return new ResultService<TResult>(
            statusCode: (int)HttpStatusCode.Created,
            sucesso: true,
            dados: dados,
            mensagem: mensagem
        );
    }

    public static ResultService<TResult> NoContent(TResult dados, string mensagem = null)
    {
        return new ResultService<TResult>
        (
            statusCode: (int)HttpStatusCode.NoContent,
            sucesso: true,
            dados: dados,
            mensagem: mensagem
        );
    }


}