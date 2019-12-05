using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore3.Template.Api.Constants;
using NetCore3.Template.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace NetCore3.Template.Api.Controllers
{
    /// <summary>
    /// ApplicationUser Controller
    /// </summary>
    [ApiVersion(Versions.Version1)]
    [Produces("application/json")]
    [ApiController]
    [Route(Routes.ApplicationUserBase)]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ILogger<ApplicationUserController> _logger;

        /// <summary>
        /// ApplicationUserController constructor
        /// </summary>
        /// <param name="logger"></param>
        public ApplicationUserController(ILogger<ApplicationUserController> logger)
        {
            _logger = logger;
        }

        [Route("getall"), HttpGet]
        [ApiVersion(Versions.Version1)]
        [SwaggerResponse(StatusCodes.Status200OK, DefaultRouteDescriptions.GetSuccessfulResponse, typeof(IEnumerable<ApplicationUser>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, DefaultRouteDescriptions.NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, DefaultRouteDescriptions.BadRequest, typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, DefaultRouteDescriptions.NotFound)]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
            return Infrastructure.ApplicationUserQueries.GetApplicationUsers();
        }
    }
}
