using System.Net;
using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FinaktivaCobro
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("cobroDiario")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                string[] nombreParametro = new string[] { };
                string[] parametro = new string[] { };
                int rows = new Connect().EjecutarSP("cobrodiario", nombreParametro, parametro);

                response.WriteString("Cobro diario ejecutado con exito");
                return response;
            }
            catch (Exception e)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.WriteString("Error: " + e.Message);
                return response;
            }

        }
    }
}
