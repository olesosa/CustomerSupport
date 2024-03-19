namespace CS.DOM.Helpers;

public class ApiException : Exception
{
    public int Status { get; set; }
    public string Detail { get; set; }

    
    public ApiException(int status, string msg) : base(msg)
    {
        Status = status;
    }
    public ApiException(int status, string msg, string detail) : base(msg)
    {
        Status = status;
        Detail = detail;
    }
}