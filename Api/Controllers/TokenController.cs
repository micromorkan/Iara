using Api.Auth;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GetToken()
        {
            Usuario user = new Usuario { Nome = "Usuário", Login = "User" };

            var token = TokenService.GenerateToken(user);

            return new
            {
                nome = user.Nome,
                token = token
            };
        }
    }
}