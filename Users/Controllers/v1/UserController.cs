using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Users.Application.Commands;
using Users.Application.Queries;
using Users.Application.Responses;

namespace Users.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAllUsers()
        {
            IEnumerable<UserResponseModel> users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet]
        [Route("{IdUtente}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserResponseModel>> GetUserById([FromRoute] GetUserByIdQuery query)
        {
            UserResponseModel user = await _mediator.Send(query);
            return Ok(user);
        }

        [HttpGet]
        [Route("Check/Email")]
        [SwaggerOperation(OperationId ="CheckIfUserExistsAsync")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> CheckUserIfExists([FromQuery] CheckUserByEmailQuery query)
        {
            bool check = await _mediator.Send(query);
            return Ok(check);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Created();
        }

        [HttpDelete]
        [Route("Delete/{IdUtente}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("Update/{IdUtente}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserResponseModel>> UpdateUser([FromRoute] int IdUtente, [FromBody] UpdateUserBody body)
        {
            UserResponseModel userUpdated = await _mediator.Send(new UpdateUserCommand()
            {
                IdUtente = IdUtente,
                Body = body
            });
            return Ok(userUpdated);
        }
    }
}
