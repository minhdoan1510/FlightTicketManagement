using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Shared.APIResponse
{
    public class ApiJsonResult : ActionResult
    {
        private HttpStatusCode _statusCode;
        private object _result;

        public ApiJsonResult(HttpStatusCode code)
        {
            _statusCode = code;
        }
        public ApiJsonResult(object result, HttpStatusCode code)
        {
            _statusCode = code;
            _result = result;
        }
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;
            var response = httpContext.Response;

            response.StatusCode = (int)_statusCode;
            response.ContentType = "application/json; charset=utf-8";
            var wFactory = httpContext.RequestServices.GetRequiredService<IHttpResponseStreamWriterFactory>();
            var options = httpContext.RequestServices.GetRequiredService<IOptions<MvcJsonOptions>>().Value;

            var serializerSettings = options.SerializerSettings;

            using(var writer = wFactory.CreateWriter(response.Body , Encoding.UTF8))
            {
                using (var jsonW = new JsonTextWriter(writer))
                {
                    jsonW.CloseOutput = false;
                    var jsonSer = JsonSerializer.Create(serializerSettings);
                    jsonSer.Serialize(jsonW, _result);
                }
            }
            return Task.CompletedTask;
        }
    }
}
