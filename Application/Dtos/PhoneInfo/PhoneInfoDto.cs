namespace Application.Dtos.PhoneInfo;

public class PhoneInfoDto
{
    public PhoneTypeEnum PhoneType { get; set; }
    public required string PhoneNumber { get; set; }
}