using ApiComSwagger.Models;
using ApiComSwagger.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiComSwagger.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao registrar", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", typeof(ErrorGenericoViewModel))]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);             
            return Created("", cursoViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao listar", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Não autorizado", Type = typeof(ValidaCampoViewModelOutput))]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get ()
        {
            var cursos = new List<CursoViewModelOutput>();
            //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            cursos.Add(new CursoViewModelOutput
            {
                Login = "Login", //codigoUsuario.ToString(),
                Nome = "Curso 1",
                Descricao = "Descriçao do Curso 1"
            });

            return Ok(cursos);
        }

    }
}
