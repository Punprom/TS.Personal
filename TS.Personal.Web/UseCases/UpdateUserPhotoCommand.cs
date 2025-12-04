using MediatR;
using Microsoft.AspNetCore.Components;

namespace TS.Personal.Web.UseCases;

internal record UpdateUserPhotoRequest(string UserId,byte[] PhotoData) : IRequest<UpdateUserPhotoResponse>;
internal record UpdateUserPhotoResponse(bool Success);
internal class UpdateUserPhotoCommand : IRequestHandler<UpdateUserPhotoRequest, 
    UpdateUserPhotoResponse>
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly NavigationManager _navigationManager;

    public UpdateUserPhotoCommand(IHttpClientFactory httpFactory, 
        NavigationManager navigationManager)
    {
        _httpFactory = httpFactory;
        _navigationManager = navigationManager;
    }

    public async Task<UpdateUserPhotoResponse> Handle(UpdateUserPhotoRequest request, 
        CancellationToken ct)
    {

        var baseUrl = _navigationManager.BaseUri;
        using var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);

        var response = await client.PostAsJsonAsync(
            $"api/users/photo", request, ct);

        return new UpdateUserPhotoResponse(response.IsSuccessStatusCode);
    }
}
