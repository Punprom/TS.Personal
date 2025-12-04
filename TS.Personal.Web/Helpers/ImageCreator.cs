using Microsoft.AspNetCore.Components.Forms;

namespace TS.Personal.Web.Helpers;

public static class ImageCreator
{
    public static async Task<MemoryStream[]> CreateImageFromStreamAsync(IBrowserFile file)
    {
        if (file == null)
        {
            return Array.Empty<MemoryStream>();
        }

        try
        {
            using var stream = file.OpenReadStream();
            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            
            return new[] { ms };
        }
        catch (Exception)
        {
            return Array.Empty<MemoryStream>();
        }
       
    }
}
