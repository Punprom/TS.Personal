using MediatR;
using Microsoft.AspNetCore.Components;

namespace TS.Personal.Web.UseCases
{
    internal record GetUserImageRequest(string UserId) : IRequest<GetUserImageResponse>;
    internal record GetUserImageResponse(byte[]? ImageData);

    internal class GetUserImageQuery : IRequestHandler<GetUserImageRequest, GetUserImageResponse>
    {

        private readonly IHttpClientFactory _httpFactory;
        private readonly NavigationManager _navigationManager;

        public GetUserImageQuery(IHttpClientFactory httpFactory,
            NavigationManager navigationManager)
        {
            _httpFactory = httpFactory;
            _navigationManager = navigationManager;
        }

        public async Task<GetUserImageResponse> Handle(GetUserImageRequest request, 
            CancellationToken ct)
        {
            var baseUrl = _navigationManager.BaseUri;
            using var client = _httpFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            var imageData = await client.GetByteArrayAsync(
                    $"api/users/{request.UserId}/image", ct);

            return new GetUserImageResponse(imageData);
        }
    }
}
