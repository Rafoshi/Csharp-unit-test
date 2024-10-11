using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_UsuarioOk()
        {
            List<Usuario> usuarios = new() {
                new Usuario() {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"

                }
            };
            _userRepositoryMock.Setup(r => r.ListarUsarios()).ReturnsAsync(usuarios);
            IActionResult result = await _controller.GetUsuario();
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult? okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarUsarios()).ReturnsAsync(([]));
            IActionResult result = await _controller.GetUsuario();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_listarUsuariosOk()
        {
            List<Usuario> usuarios = new List<Usuario>() { new()  {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"
                }
            };
        }

        [Fact]
        public async Task Post_listarUsuariosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Usuario usuario = new()
            {
                Email = "xxx@gmail.com",
                Id = 1,
                Nome = "Thiago xavier"
            };

            _userRepositoryMock.Setup(r => r.SalvarUsario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            var result = await _controller.Post(usuario);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_listarUsuariosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Usuario usuario = new()
            {
                Email = "xxx@gmail.com",
                Id = 1,
                Nome = "Thiago xavier"
            };

            _userRepositoryMock.Setup(r => r.AtualizarUsuario(usuario)).Returns(Task.CompletedTask);
            var result = await _controller.Put(usuario);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_listarUsuariosOk()
        {
            //Falha pois o repositorio não esta separado do controller
            Usuario usuario = new()
            {
                Email = "xxx@gmail.com",
                Id = 1,
                Nome = "Thiago xavier"
            };

            _userRepositoryMock.Setup(r => r.SalvarUsario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            var result = await _controller.Delete(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
