using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    // http://localhost:5000/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //Referencia: Codigo de status de respostas HTTP
        //https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status

        private IUserService Service;

        public UsersController(IUserService service)
        {
            Service = service;
        }


        //localhost:5000/api/users
        [HttpGet] //retorna todos os registros da tabela       
        public async Task<ActionResult> GetAll()
        {
            // pode-se usar a chamada da Interface diretamente no metodo
            // nesse caso nao é necessario declarar um construtor
            // Exemplo: public async Task<ActionResult> GetAll([FromServices] IUserService service) 
            
            
            //Se o modelstate não for valido
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); // erro http 400: solicitação invalida
            }

            try
            {
                return Ok(await Service.GetAll());
            }
            catch (ArgumentException ex)
            {
                return StatusCode ((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //localhost:5000/api/users/<guid>
        [HttpGet] //retorna um unico registro, atraves de seu id
        [Route("{id}", Name="GetWithId")]
        public async Task<ActionResult> Get(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await Service.Get(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost] //cria um  registro
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await Service.Post(user);
                if(result != null)
                {
                    return Created(new Uri(Url.Link("GetWithId", new {id = result.Id})), result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }            
        }  

        [HttpPut] //atualiza um registro
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await Service.Put(user);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }    

        [HttpDelete ("{id}")]//Deleta um registro
        public async Task<ActionResult> Delete(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await Service.Delete(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }  
    }
}