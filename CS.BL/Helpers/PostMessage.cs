using Microsoft.AspNetCore.Http;

namespace CS.BL.Helpers;

public class PostMessage
{
    public string Text { get; set; }
    public IFormCollection Files { get; set; }//better rename it. usage of postMessage.Files.Files is a bit confusing
}