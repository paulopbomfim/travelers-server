namespace Travelers.Exception.ExceptionBase;

public abstract class TravelersException : SystemException
{
    protected TravelersException(string message) : base(message) {}
    
    public abstract int StatusCode { get; }
    public abstract IList<string> GetErrors();
}