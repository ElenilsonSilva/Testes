using GestaoDeUsuarios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static int userIdCount = 0;
        static List<User> listaUsuarios = new List<User>();

        /**
         * GET api/User
         * 
         * MÉTODO PARA CONSULTAR TODOS OS USUARIOS CRIADOS
         * Se não tiver nenhum usuário na lista retorna o status code 204
         */
         
        [HttpGet]
        public IActionResult ListarTodosUsuarios()
        {
            if (listaUsuarios.Count == 0)
                return StatusCode(204);
            return Ok(listaUsuarios);
        }

        /**
         * GET api/User/id
         * O método busca pelo Id um usuario cadastrado e se encontrar retorna o usuario e suas informações 
         */

        [HttpGet("{id}")]
        public IActionResult ListarUsuarioPorId([FromRoute] int id)
        {
            var usuario = listaUsuarios.Where(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
                return StatusCode(400, "Usuario não existe na lista");
            return Ok(usuario);
        }

        /**
         * POST api/User
         * Cria um usuario na lista de acordo com o corpo da requisição 
         * 
         * Exemplo:
         *  {
         *      "Name": "Elenilson",
         *      "Avatar": "Perfil Negocio",
         *      "Email": "elenilson@gmail.com",
         *      "City": "Salinopolis"
         *  }
         *  
         *  RETURNO
         *  statuscode 201 - Usuario criado com sucesso 
         *  statuscode 400 - usuário não foi criado
         *
         */
        [HttpPost]
        public IActionResult CriarNovoUsuario([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
                return StatusCode(400, $"O campo {nameof(user.Name)} não está preenchido corretamente");
            if (string.IsNullOrWhiteSpace(user.Email))
                return StatusCode(400, $"O campo {nameof(user.Email)} não está preenchido corretamente");
            if (string.IsNullOrWhiteSpace(user.City))
                return StatusCode(400, $"O campo {nameof(user.City)} não está preenchido corretamente");

            user.Id = userIdCount++;

            listaUsuarios.Add(user);
            return StatusCode(201);  
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarUsuarioPorId([FromRoute] int id)
        {
            var usuarioParaDeletar = listaUsuarios.Where(x => x.Id == id).FirstOrDefault();
            if (usuarioParaDeletar == null)
                return StatusCode(400, "Usuario não existe na lista");

            listaUsuarios.Remove(usuarioParaDeletar);

            return StatusCode(200, $"{usuarioParaDeletar.Name} deletado com sucesso.");
        }
    }
}
