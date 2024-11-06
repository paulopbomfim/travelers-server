namespace Travelers.Communication.Requests;

public record UserRequest
{
    public string TxName { get; set; } = string.Empty;
    public string TxEmail { get; set; } = string.Empty;
    public string TxPassword { get; set; } = string.Empty;
}