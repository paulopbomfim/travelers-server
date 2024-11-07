using System.ComponentModel.DataAnnotations;
using Travelers.Domain.Enums;

namespace Travelers.Domain.Entities;

public record User
{
    [Key]
    public long IdUser { get; set; }
    public string TxName { get; set; } = string.Empty;
    public string TxEmail { get; set; } = string.Empty;
    public string TxPassword { get; set; } = string.Empty;
    public string TxAvatarUrl { get; set; } = string.Empty;
    public string TxRole { get; set; } = Roles.Traveler;
    public Guid CoUserIdentifier { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}