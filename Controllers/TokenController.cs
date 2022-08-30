using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using AuthorizationService.Provider;
using log4net;

namespace AuthorizationService.Controllers          
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILog _log4net = LogManager.GetLogger(typeof(TokenController));
        private IConfiguration config;
        private readonly IAuthProvider objProvider;
        public TokenController(IConfiguration config,IAuthProvider objProvider)
        {
            this.config = config;
            this.objProvider = objProvider;
        }       

        [HttpPost]
        public IActionResult Login([FromBody] Authenticate loginCredentials)
        {
            _log4net.Info(" Http Post request" +nameof(Login));
            if (loginCredentials == null)
            {
                return BadRequest();
            }
            try
            {
                IActionResult response = Unauthorized();
                Authenticate userCredentials = objProvider.AuthenticateUser(loginCredentials);

                if (userCredentials != null)
                {
                    string tokenString = objProvider.GenerateJSONWebToken(userCredentials, config);
                    response = Ok(tokenString);
                    return response;
                }

                return Unauthorized("Invalid Credentials");
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured "+e.Message+" from " +nameof(Login));
                return StatusCode(500);
            }
            
        }
        
        
    }
}
