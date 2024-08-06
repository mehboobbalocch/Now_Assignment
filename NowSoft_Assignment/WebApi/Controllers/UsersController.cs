using Application.Exceptions;
using Application.Features.User.Commands;
using Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupUserCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("balance")]
        public async Task<IActionResult> Balance()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userIdString = userIdClaim?.Value;

                if (string.IsNullOrEmpty(userIdString))
                {
                    return Unauthorized("User ID claim not found.");
                }

                var query = new GetUserBalanceQuery { UserId = userIdString };
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{role}")]
        public async Task<IActionResult> GetRole()
        {
            var role = await _mediator.Send(new GetRoleQuery());

            return Ok(role);
        }
    }
}
