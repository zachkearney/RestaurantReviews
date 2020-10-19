﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReviewsApi.ApiModels.ApiModels;
using RestaurantReviewsApi.Bll.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviewsApi.Controllers
{

    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthProvider _authProvider;

        public AuthController(ILogger<AuthController> logger, IAuthProvider authProvider)
        {
            _logger = logger;
            _authProvider = authProvider;
        }

        /// <summary>
        /// Returns an accesstoken for the given username.
        /// Using a username containing the word "Admin" will grant Restaurant Post, Patch, and Delete.
        /// </summary>
        /// <param name="userName"></param>        
        [HttpPost("{userName}")]
        [ProducesResponseType(typeof(AccessTokenApiModel), 200)]
        [ProducesResponseType(403)]
        [AllowAnonymous]
        public IActionResult Login(string userName)
        {
            try
            {
                if (_authProvider.AuthenticateUser(userName))
                    return Ok(_authProvider.GetAccessTokenApiModel(userName));
                else
                    return Forbid();
            }
            catch (Exception e)
            {
                _logger.LogError(default, e);
                return StatusCode(500);
            }      
        }
    }
}
