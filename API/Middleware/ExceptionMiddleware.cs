using System.Net;
using System.Text.Json;
using API.Erros;

namespace API.Middleware;

// Ilogger serve para monitorar e registrar eventos
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
	IHostEnvironment env)
{
	
  public async Task InvokeAsync(HttpContext context)
	{
		try
		{
				await next(context);
		}
		catch (Exception ex)
		{
			
			logger.LogError(ex, ex.Message);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			// se estiver em ambiente de desenvolvimento
			var response = env.IsDevelopment() 
			// como fosse if else
			? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
			: new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");
		
			var options = new JsonSerializerOptions // para formatar o json
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase // para deixar o json em camelCase
			};

			var json = JsonSerializer.Serialize(response, options);

			await context.Response.WriteAsync(json); // escreve o json no body da respostade
		}
  }
}
