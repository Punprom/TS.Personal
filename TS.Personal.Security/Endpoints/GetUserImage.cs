using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Endpoints;

internal record GetUserImageRequest(string UserId);
internal record GetUserImageResponse(byte[]? ImageData);

internal class GetUserImage 
    :Endpoint<GetUserImageRequest, GetUserImageResponse>
{
    private readonly IUserService _service;
    public GetUserImage(IUserService service)
    {
        _service = service;
    }
    public override void Configure()
    {
        Get("api/users/{UserId}/image");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetUserImageRequest request, CancellationToken ct)
    {
        var imageData = await _service.GetUserProfileImageAsync(userId: request.UserId);

        await Send.OkAsync(new GetUserImageResponse(imageData), cancellation: ct);
    }
}
