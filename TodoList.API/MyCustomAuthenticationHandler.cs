using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TodoList.DAL.Interfaces;
using TodoList.DAL.Models;

namespace TodoList.API
{
    public class MyCustomAuthenticationHandler : AuthenticationHandler<MyCustomAuthenticationSchemeOptions>
    {
        private readonly IRepository<UserDal> _userRepository;

        public MyCustomAuthenticationHandler(
            IOptionsMonitor<MyCustomAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IRepository<UserDal> repository) : base(options, logger, encoder, clock)
        {
            _userRepository = repository;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorizationHeader = Request.Headers["User-ID"];

            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.NoResult();
                //return AuthenticateResult.Fail("User ID is not provided");

            if (!int.TryParse(authorizationHeader, out var userId))
                return AuthenticateResult.Fail("User ID should be an integer");

            var user = await _userRepository.GetAsync(userId);
            if (user is null)
                return AuthenticateResult.NoResult();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Regular")
            };

            var claimsPrimcipal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            var ticket = new AuthenticationTicket(
                claimsPrimcipal,
                new AuthenticationProperties { IsPersistent = false },
                Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
