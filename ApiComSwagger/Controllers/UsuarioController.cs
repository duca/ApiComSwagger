/* Copyright (c) 2021 Eduardo L
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN */

using ApiComSwagger.Business.Entities;
using ApiComSwagger.Filters;
using ApiComSwagger.Infrastructure.Data;
using ApiComSwagger.Models;
using ApiComSwagger.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ApiComSwagger.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {        
        /// <summary>
        /// Serviço de logar
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna status ok, dados do usuario e token de acesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios faltando", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type =typeof(ErrorGenericoViewModel))]
        [HttpPost]
        [Route("logar")]        
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuarioViewModelOutput = new UsuarioViewModelOutput
            {
                Login = "Eduardo",
                Email = "edumlopes@gmail.com",
                Codigo = 1
            };

            var secret = Encoding.ASCII.GetBytes("ePt(yaqbFNU4bHhF2Y*J&)32Pv^$1JhG*C_x33p4eVxIxLeW");
            var symmetricSecureityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecureityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSeceurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSeceurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSeceurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput.Login
            });
        }

        /// <summary>
        /// Registrar usuário
        /// </summary>
        /// <param name="registroViewModeLinput"></param>
        /// <returns>Retorna status Created</returns>
        [SwaggerResponse(statusCode: 201, description:"Sucesso ao registrar", Type =typeof(RegistroViewModeLinput))]
        [SwaggerResponse(statusCode: 401, description: "Campos obrigatórios faltando", Type =typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", typeof(ErrorGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar (RegistroViewModeLinput registroViewModeLinput)
        {
            var options = new DbContextOptionsBuilder<CursoDbContext>();
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ApiComSwagger;Integrated Security=True");
            CursoDbContext context = new CursoDbContext(options.Options);

            var pendingMigs = context.Database.GetPendingMigrations();
            if (pendingMigs.Count() > 0)
            {
                context.Database.Migrate();
            }

            var usuario = new Usuario();
            usuario.Login = registroViewModeLinput.Login;
            usuario.Email = registroViewModeLinput.Email;
            usuario.Senha= registroViewModeLinput.Senha;
            context.Usuario.Add(usuario);
            context.SaveChanges();

            return Created("", registroViewModeLinput);
        }
        
    }
}
