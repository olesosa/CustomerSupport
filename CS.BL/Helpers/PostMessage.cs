using Microsoft.AspNetCore.Http;

namespace CS.BL.Helpers;

public class PostMessage
{
    public string Text { get; set; }
    public IFormCollection Files { get; set; }
}