using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // cada controlador tem uma rota, o nome da rota fica no colchetes
public class BaseApiController : ControllerBase
{
}
