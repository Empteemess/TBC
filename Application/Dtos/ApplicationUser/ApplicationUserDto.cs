namespace Application.Dtos.ApplicationUser;

public class ApplicationUserDto
{
    [DefaultValue("FirstName")]
    public required string FirstName { get; set; }
    
    [DefaultValue("LastName")]
    public required string LastName { get; set; }
    
    public GenderEnum Gender { get; set; }
    
    [DefaultValue("PersonalId")]
    public required string PersonalId { get; set; }
    public required string Image { get; set; }
    public required DateTime BirthDate { get; set; }



    public IEnumerable<PhoneInfoDto>? PhoneInfos { get; set; }
}