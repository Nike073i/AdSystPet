using AdSyst.AuthService.Application.Users.Commands.RegisterUser;

namespace AdSyst.AuthService.Api.IntegrationTests.Controllers
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly string RegisterEndpointUrl = "/api/users";

        public AuthControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task RegisterNewUser_Success()
        {
            var registerUserDto = CreateRegisterDto();
            var response = await MakeRegisterRequest(registerUserDto);
            CheckResponseStatus(response, HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("password")]
        [InlineData("")]
        [InlineData("pass")]
        public async Task RegisterNewUser_FailOnBadPassword(string password)
        {
            var registerUserDto = CreateRegisterDto(password: password);
            var response = await MakeRegisterRequest(registerUserDto);
            CheckResponseStatus(response, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("xyz")]
        [InlineData("")]
        public async Task RegisterNewUser_FailOnBadUsername(string username)
        {
            var registerUserDto = CreateRegisterDto(userName: username);
            var response = await MakeRegisterRequest(registerUserDto);
            CheckResponseStatus(response, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("")]
        public async Task RegisterNewUser_FailOnBadFirstName(string firstName)
        {
            var registerUserDto = CreateRegisterDto(firstName: firstName);
            var response = await MakeRegisterRequest(registerUserDto);
            CheckResponseStatus(response, HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("")]
        public async Task RegisterNewUser_FailOnBadLastName(string lastName)
        {
            var registerUserDto = CreateRegisterDto(lastName: lastName);
            var response = await MakeRegisterRequest(registerUserDto);
            CheckResponseStatus(response, HttpStatusCode.BadRequest);
        }

        private Task<HttpResponseMessage> MakeRegisterRequest(RegisterUserCommand registerUserDto)
        {
            var client = _factory.CreateClient();
            var jsonContent = JsonContent.Create(registerUserDto);
            return client.PostAsync(RegisterEndpointUrl, jsonContent);
        }

        private static void CheckResponseStatus(
            HttpResponseMessage response,
            HttpStatusCode expectedCode
        )
        {
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(expectedCode);
        }

        private static RegisterUserCommand CreateRegisterDto(
            string userName = "Sema23145",
            string firstName = "Semen",
            string lastName = "Ivanov",
            string password = "F!lippov123",
            string email = "nike073i@mail.ru",
            DateTimeOffset? birthday = null
        )
        {
            birthday ??= DateTimeOffset.Now.AddYears(-14);
            return new(userName, firstName, lastName, password, email, birthday.Value);
        }
    }
}
