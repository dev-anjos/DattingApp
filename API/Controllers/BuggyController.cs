
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth(){
        return "secret text";
    }
    
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = context.Users.Find(-1); //passando id que não existe
        if (thing == null) return NotFound();
        return thing;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError() //return AppUser caso 
    {
        var thing = context.Users.Find(-1) ?? 
        throw new Exception("Erro no servidor");
        return thing;
    }

    [HttpGet("bad-request")]
    public ActionResult<string>  GetBadRequest(){
        return BadRequest("bad request");
    }
}
