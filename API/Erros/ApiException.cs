using System;

namespace API.Erros;

public class ApiException(int statusCode, string message, string? details) 
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}

//tmabém pode ser criado forme abaixo, acima é utilizando o construtor padrao da classe(primario)

// public class ApiException
// {
//     public int StatusCode { get; set; }
//     public string Message { get; set; }
//     public string? Details { get; set; }

//     public ApiException(int statusCode, string message, string? details)
//     {
//         StatusCode = statusCode;
//         Message = message;
//         Details = details;
//     }
// }