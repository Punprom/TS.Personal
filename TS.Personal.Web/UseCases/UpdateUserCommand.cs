using MediatR;
using Microsoft.AspNetCore.Components;
using TS.Personal.Core.Dtos;

namespace TS.Personal.Web.UseCases;

internal record UpdateUserProfileRequest(UserDto User) : IRequest<UpdateUserProfileResponse>;
internal record UpdateUserProfileResponse(bool Success);

internal class UpdateUserCommand
    : IRequestHandler<UpdateUserProfileRequest, UpdateUserProfileResponse>
{

    private readonly IHttpClientFactory _httpFactory;
    private readonly NavigationManager _navigationManager;

    public UpdateUserCommand(
        IHttpClientFactory httpFactory,
        NavigationManager navigationManager)
    {
        _httpFactory = httpFactory;
        _navigationManager = navigationManager;
    }

    public async Task<UpdateUserProfileResponse> Handle(UpdateUserProfileRequest request,
        CancellationToken ct)
    {
        var baseUrl = _navigationManager.BaseUri;
        using var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);
        var response = await client.PutAsJsonAsync("api/users", request, ct);
        
        return response.IsSuccessStatusCode
            ? new UpdateUserProfileResponse(true)
            : new UpdateUserProfileResponse(false);
    }
}
