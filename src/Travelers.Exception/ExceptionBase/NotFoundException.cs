using System.Net;

namespace Travelers.Exception.ExceptionBase;

public class NotFoundException : TravelersException
{
    public NotFoundException(string message) : base(message) { }
    
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override IList<string> GetErrors()
    {
        return [Message];
    }
}