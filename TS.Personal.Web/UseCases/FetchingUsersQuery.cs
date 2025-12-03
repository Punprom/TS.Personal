using MediatR;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TS.Personal.Core.Dtos;

namespace TS.Personal.Web.UseCases;

internal record FetchingUsersRequest() : IRequest<FetchingUsersResponse>;
internal record FetchingUsersResponse(List<UserDto> Users);

internal class FetchingUsersHandler : IRequestHandler<FetchingUsersRequest, FetchingUsersResponse>
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly NavigationManager _navigationManager;

    public FetchingUsersHandler(IHttpClientFactory httpFactory, NavigationManager navigationManager)
    {
        _httpFactory = httpFactory;
        _navigationManager = navigationManager;
    }

    public async Task<FetchingUsersResponse> Handle(FetchingUsersRequest request, CancellationToken ct)
    {
        var baseUrl = _navigationManager.BaseUri;
        using var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);

        using var resp = await client.GetAsync("api/users", ct);
        resp.EnsureSuccessStatusCode();
        var content = await resp.Content.ReadAsStringAsync(ct);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        IEnumerable<UserDto>? users;
        try
        {
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                // API returned an array at root: [ { ... }, ... ]
                users = JsonSerializer.Deserialize<IEnumerable<UserDto>>(root.GetRawText(), options)
                        ?? Array.Empty<UserDto>();
            }
            else if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("users", out var usersElement))
            {
                // API returned wrapper object: { "users": [ ... ] }
                users = JsonSerializer.Deserialize<IEnumerable<UserDto>>(usersElement.GetRawText(), options)
                        ?? Array.Empty<UserDto>();
            }
            else
            {
                throw new JsonException("Unexpected JSON shape when parsing users.");
            }
        }
        catch (JsonException ex)
        {
            // Re-throw with content to help debugging in development. Remove detailed content in production.
            throw new InvalidOperationException($"Failed to parse users JSON. Content: {content}", ex);
        }

        return new FetchingUsersResponse(users.ToList());
    }
}



