using FastEndpoints;
using TS.Personal.Core.Interfaces;

namespace TS.Personal.Security.Endpoints
{
    internal record UpdatePhotoRequest(string UserId, byte[] PhotoData);
    internal record UpdatePhotoResponse(bool Success);

    internal class UpdatePhoto : Endpoint<UpdatePhotoRequest, UpdatePhotoResponse>
    {
        private readonly IUserService _service;

        public UpdatePhoto(IUserService service)
        {
            _service = service;
        }

        public override void Configure()
        {
            Post("api/users/photo");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdatePhotoRequest req, CancellationToken ct)
        {
            await _service.UpdateUserProfileImageAsync(req.UserId, req.PhotoData);
            
            await Send.OkAsync(new UpdatePhotoResponse(true), cancellation: ct);
        }
    }
}
