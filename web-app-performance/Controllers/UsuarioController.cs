using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySqlConnector;
using Newtonsoft.Json;
using StackExchange.Redis;
using web_app_domain;
using web_app_repository;


namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private static ConnectionMultiplexer redis;
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository) { 
         _repository = repository; 
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            //string key = "getusuario";
            //redis = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase db = redis.GetDatabase();
            //await db.KeyExpireAsync(key, TimeSpan.FromSeconds(10));
            //string user =await db.StringGetAsync(key);
            //if (!string.IsNullOrEmpty(user)) {
            //    return Ok(user);
            
            //}
            var usuarios = await _repository.ListarUsarios();
            if(usuarios is null)
                return NotFound();
            string usuariosJson= JsonConvert.SerializeObject(usuarios);
            //await db.StringSetAsync(key,usuariosJson);

            return Ok(usuarios);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {


            await _repository.SalvarUsario(usuario);

            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);




            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Usuario usuario)
        {
            await _repository.AtualizarUsuario(usuario);



            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);




            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {


            await _repository.RemoverUsuario(id);

            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);




            return Ok();
        }
    }
}
