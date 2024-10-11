using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class UsuarioRepository
    {
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;
        public UsuarioRepository() {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }
        [Fact]
        public async Task Get_UsuarioOk()
        {
            var usuarios = new List<Usuario>() {
                new Usuario() {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Thiago xavier"

                }, new Usuario() {
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
