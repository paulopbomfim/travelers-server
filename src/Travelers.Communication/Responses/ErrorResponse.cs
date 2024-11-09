namespace Travelers.Communication.Responses;

public record ErrorResponse
{
    public IList<string> ErrorMessages { get; }

    public ErrorResponse(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }
    
    public ErrorResponse(IList<string> errorMessage)
    {
        ErrorMessages = errorMessage;
    }
}