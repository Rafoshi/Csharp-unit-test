using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class UsuarioRepository
    {
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;

        public UsuarioRepository()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Delete_UsuarioOk()
        {
            var usuario = new Usuario()
            {
                Email = "xxx@gmail.com",
                Id = 1,
                Nome = "Thiago xavier"
            };

            _userRepositoryMock.Setup(r => r.SalvarUsario(usuario)).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(r => r.RemoverUsuario(usuario.Id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(usuario.Id);
            Assert.IsType<OkResult>(result);
            var okResult = result as OkResult;
        }

        [Fact]
        public async Task Get_UsuarioOk()
        {
            var usuarios = new List<Usuario>() {
                new() {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"

                }, new() {
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
    }
}
