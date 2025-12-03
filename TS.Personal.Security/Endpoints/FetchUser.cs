using FastEndpoints;
using TS.Personal.Core.Dtos;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Endpoints;

internal record FetchUserRequest(string UserId);
internal record FetchUserResponse(UserDto User);

internal class FetchUser : Endpoint<FetchUserRequest, FetchUserResponse>
{
    private readonly IUserService _service;

    public FetchUser(IUserService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("api/{userId}/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(FetchUserRequest request, 
        CancellationToken ct)
    {
         var result = await _service.GetUserProfileAsync(request.UserId);
         if (result is null)
         {
             await Send.NotFoundAsync(ct);
             return;
         }

       await Send.OkAsync(new FetchUserResponse(result), ct);
    }
}
