using FastEndpoints;
using TS.Personal.Core.Dtos;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Endpoints;

public record ListUserResponse
{
    public List<UserDto> Users { get; init; }
}

public class ListUser : EndpointWithoutRequest<ListUserResponse>
{
    private readonly IUserService _service;

    public ListUser(IUserService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _service.GetAllUsersAsync();
        var response = new ListUserResponse
        {
            Users = users
        };
        await Send.OkAsync(response, cancellation: ct);
    }
}
