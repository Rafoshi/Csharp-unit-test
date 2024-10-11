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
        public async Task Get_UsuarioOk() {
            var usuarios = new List<Usuario>() {
                new Usuario() {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"

                }
            };
            _userRepositoryMock.Setup(r => r.ListarUsarios()).ReturnsAsync(usuarios);
            var result = await _controller.GetUsuario();
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));

        }
        [Fact]
        public async Task Get_ListarRetornarNotFound() {
            _userRepositoryMock.Setup(u => u.ListarUsarios()).ReturnsAsync((IEnumerable));
        }
        [Fact]
        public async Task Get_listarUsuariosOk() {
            var usuarios = new List<Usuario>() { new Usuario()  {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"

                } 
            };
        }
    }
}