using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PzuCwiczenia.Infrastructure.ServiceInterfaces;
using PzuCwiczenia.WebApi.ViewModel.Requests;

namespace PzuCwiczenia.WebApi.Authentication
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public IActionResult SignIn(SignInRequest request)
        {
            string token = string.Empty;

            try
            {
                token = authenticationService.Auhtenticate(request.UserName, request.Password);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
        }
    }
}
