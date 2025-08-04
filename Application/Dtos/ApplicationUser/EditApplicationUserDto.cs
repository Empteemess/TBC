namespace Application.Dtos.ApplicationUser;

public class EditApplicationUserDto
{
    public int UserId { get; set; }
    [DefaultValue("FirstName")]
    public string? FirstName { get; set; }

    [DefaultValue("LastName")]
    public string? LastName { get; set; }

    public GenderEnum? Gender { get; set; }

    [DefaultValue("PersonalId")]
    public string? PersonalId { get; set; }

    public DateTime? BirthDate { get; set; }

    public IEnumerable<PhoneInfoDto>? PhoneInfos { get; set; }
}