using api_mobile.Tools;
using api_mobile.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace api_mobile.Middlewares
{
    public class ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger<ErrorMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);

                await UnAuthorized(context);

            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"Erro ocorrido com TraceId: {context.TraceIdentifier}");

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Type = "https://learn.microsoft.com/pt-br/troubleshoot/developer/webapps/iis/www-administration-management/http-status-code",
                Extensions =
                        {
                            { "traceId", context.TraceIdentifier },
                            { "Logref", Guid.NewGuid().ToString() },
                            { "Message", "Messagem padrão que não é especifica do erro"}
                        }
            };

            int statusCode;
            ErrorResponseVm errorResponseVm;

            switch (ex)
            {
                //HTTP 400
                case Microsoft.IdentityModel.Tokens.SecurityTokenMalformedException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Não autorizado.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 401....explicando o erro melhor..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-401#:~:text=O%20Erro%20401%20indica%20um,exigem%20um%20login%20para%20acesso.";
                    break;
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponseVm = new ErrorResponseVm("ErroID400", "Requisição inválida.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 400....explicando..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://learn.microsoft.com/pt-br/iis/troubleshoot/diagnosing-http-errors/troubleshooting-http-400-errors-in-iis";
                    break;

                //HTTP 401
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Não autorizado.");
                    problemDetails.Extensions["Messagem Específica do erro"] = "Erro 401....explicando o erro melhor..";
                    problemDetails.Extensions["Saiba mais sobre o erro"] = "https://www.hostinger.com.br/tutoriais/erro-401#:~:text=O%20Erro%20401%20indica%20um,exigem%20um%20login%20para%20acesso.";
                    break;

                //HTTP 500
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponseVm = new ErrorResponseVm("ErroID500", "Ocorreu um erro interno no servidor.");
                    break;
            }

            problemDetails.Status = statusCode;
            problemDetails.Title = Tool.GetErrorTitle(statusCode);
            problemDetails.Detail = errorResponseVm.Errors.FirstOrDefault()?.Message;

            await ChangeContext(context, problemDetails, statusCode);
        }

        private static async Task UnAuthorized(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Unauthorized,
                    Tool.GetErrorTitle((int)HttpStatusCode.Unauthorized))
                    , (int)HttpStatusCode.Unauthorized);

            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Forbidden,
                    Tool.GetErrorTitle((int)HttpStatusCode.Forbidden))
                    , (int)HttpStatusCode.Forbidden);
        }

        public static async Task ChangeContext<T>(HttpContext context, T problemDetails, int status)
        {
            context.Response.StatusCode = status;
            var result = JsonConvert.SerializeObject(problemDetails);
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }
    }
}