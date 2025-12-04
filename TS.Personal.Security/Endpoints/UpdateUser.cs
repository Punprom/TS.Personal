using FastEndpoints;
using Serilog;
using TS.Personal.Core.Dtos;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Endpoints;

internal record UpdateUserRequest(UserDto User);
 
internal class UpdateUser : Endpoint<UpdateUserRequest>
{
    private readonly IUserService _service;
    private readonly ILogger _logger;

    public UpdateUser(IUserService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Put("api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserRequest request, CancellationToken ct)
    {
        //_logger.Information("Updating user profile for UserId: {UserId}", request.User.Id);
         await _service.UpdateUserProfileAsync(request.User);

        await Send.OkAsync(cancellation: ct);
    }
}