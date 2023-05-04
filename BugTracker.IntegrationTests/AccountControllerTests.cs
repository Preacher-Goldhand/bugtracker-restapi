using BugTracker.IntegrationTests.Helpers;
using BugTracker.Models.AuthenticationDtos;
using BugTracker.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace BugTracker.IntegrationTests
{
    public class AccountControllerTests
    {
        private HttpClient _client;
        private Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();

        public AccountControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();

            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ForRegisteredUser_ReturnsOk()
        {
            // arrange

            _accountServiceMock
                .Setup(e => e.GenerateJwt(It.IsAny<LoginEmployeeDto>()))
                .Returns("jwt");

            var loginDto = new LoginEmployeeDto()
            {
                EmployeeEmail = "darwin@test.com",
                EmployeePasswordHash = "darwin123"
            };

            var httpContent = loginDto.ToJsonHttpContent();

            // act

            var response = await _client.PostAsync("/bugtracker/account/login", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ChangePassword_ForRegisteredUser_ReturnsOk()
        {
            // arrange

            _accountServiceMock
                .Setup(e => e.GenerateJwt(It.IsAny<LoginEmployeeDto>()))
                .Returns("jwt");

            var changePasswordDto = new ChangePasswordDto()
            {
                EmployeeEmail = "darwin@test.com",
                CurrentPasswordHash = "chdarwin123",
                NewPasswordHash = "newPassword123"
            };

            var httpContent = changePasswordDto.ToJsonHttpContent();

            // act

            var response = await _client.PostAsync("/bugtracker/account/changePassword", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            // arrange

            var registerUser = new RegisterEmployeeDto()
            {
                FirstName = "Test",
                LastName = "Testowy",
                Department = "DevTools",
                EmployeeEmail = "test@test.com",
                EmployeePasswordHash = "password123",
                ConfirmEmployeePasswordHash = "password123"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // act

            var response = await _client.PostAsync("/bugtracker/account/register", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidModel_ReturnsBadRequest()
        {
            // arrange

            var registerUser = new RegisterEmployeeDto()
            {
                EmployeePasswordHash = "password123",
                ConfirmEmployeePasswordHash = "123"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // act

            var response = await _client.PostAsync("/bugtracker/account/register", httpContent);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}