namespace Application.Dtos.ApplicationUser;

public class GetApplicationUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public GenderEnum Gender { get; set; }
    public required string PersonalId { get; set; }
    public required DateTime BirthDate { get; set; }
    public IEnumerable<PhoneInfoDto>? PhoneInfos { get; set; }
}
