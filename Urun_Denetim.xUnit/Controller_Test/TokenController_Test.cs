using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basvurular.Business;
using Basvurular.DataAccess;
using Moq;
using Urun_Denetim.Controllers;
using Basvurular.Entities;
using Microsoft.AspNetCore.Mvc;
using Basvurular.Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace Urun_Denetim.xUnit.Controller_Test
{
	public class TokenController_Test
	{
		private readonly TokenController _controller;
		private readonly Mock<ITokenRepository> _mockRepo;
		private readonly Mock<IConfiguration> _mockConfig;
		private readonly TokenService _service;

		public TokenController_Test()
		{
			_mockRepo = new Mock<ITokenRepository>();
			_mockConfig = new Mock<IConfiguration>();

			// Mock IConfiguration values (Token:Key, Token:Issuer, Token:Audience)
			_mockConfig.Setup(c => c["Token:Key"]).Returns("this-is-a-very-secure-and-long-key-32char!"); // 32 karakterlik bir anahtar olmazsa hata verir
			_mockConfig.Setup(c => c["Token:Issuer"]).Returns("test-issuer");
			_mockConfig.Setup(c => c["Token:Audience"]).Returns("test-audience");

			_service = new TokenService(_mockRepo.Object, _mockConfig.Object);
			_controller = new TokenController(_service);
		}

		[Fact]
		public void TokenController_Login_ReturnsToken()
		{
			// Arrange
			var admin = new Admins { AdminAd = "testUser", AdminSifre = "password123" };
			var expectedToken = "fake-jwt-token";

			// ITokenRepository mock: Başarılı girişte admin döndür
			_mockRepo.Setup(r => r.Authenticate(admin.AdminAd, admin.AdminSifre)).Returns(new Admins
			{
				AdminAd = "testUser",
				AdminYetki = "Admin"
			});

			// Act
			var result = _controller.Login(admin) as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(200, result.StatusCode);
			Assert.IsType<string>(result.Value); // Token string döndürmeli
		}

		[Fact]
		public void TokenController_Login_ReturnsUnauthorized()
		{
			// Arrange
			var admin = new Admins { AdminAd = "wrongUser", AdminSifre = "wrongPassword" };

			// ITokenRepository mock: Geçersiz girişte null döndür
			_mockRepo.Setup(r => r.Authenticate(admin.AdminAd, admin.AdminSifre)).Returns((Admins)null);

			// Act
			var result = _controller.Login(admin) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(401, result.StatusCode);
			Assert.Equal("Geçersiz kullanıcı adı veya şifre.", result.Value);
		}

		[Fact]
		public void TokenController_Login_MissingCredentials_ReturnsBadRequest()
		{
			// Arrange
			var admin = new Admins { AdminAd = "", AdminSifre = "" };

			// Act
			var result = _controller.Login(admin) as ObjectResult;

			// Assert
			Assert.NotNull(result);
			Assert.Equal(400, result.StatusCode);
			Assert.Equal("Kullanıcı adı ve şifre gereklidir.", result.Value);
		}
	}
}
