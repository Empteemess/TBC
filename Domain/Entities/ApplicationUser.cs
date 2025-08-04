namespace Domain.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public GenderEnum Gender { get; set; }
    public required string PersonalId { get; set; }
    public required DateTime BirthDate { get; set; }

    public string? Image { get; set; }

    public ICollection<PhoneInfo>? PhoneInfos { get; set; }
    public ICollection<UserRelationship>? Connections { get; set; } = [];
    public ICollection<UserRelationship>? ConnectedBy { get; set; } = [];
}