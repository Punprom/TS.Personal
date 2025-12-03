using MediatR;
using Microsoft.AspNetCore.Components;
using TS.Personal.Core.Dtos;

namespace TS.Personal.Web.UseCases;

internal record GetUserRequest(string UserId) : IRequest<GetUserResponse>;
internal record GetUserResponse(UserDto? Profile);

internal class FetchUserQuery : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly NavigationManager _navigationManager;

    public FetchUserQuery(IHttpClientFactory httpFactory, 
        NavigationManager navigationManager)
    {
        _httpFactory = httpFactory;
        _navigationManager = navigationManager;
    }

    public async Task<GetUserResponse> Handle(GetUserRequest request, 
        CancellationToken ct)
    {
        var baseUrl = _navigationManager.BaseUri;
        using var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);

        var response = await client.GetFromJsonAsync<UserDto>($"api/users/{request.UserId}/users", ct);
        if (response == null)
        {
            return new GetUserResponse(null);
        }

        return new GetUserResponse(response);
    }
}
