namespace CS.DOM.Helpers;

public class ApiException : Exception
{
    public int Status { get; set; }
    public string Detail { get; set; }
}