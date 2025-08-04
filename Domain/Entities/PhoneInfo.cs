namespace Domain.Entities;

public class PhoneInfo
{
    public int Id { get; set; }

    public PhoneTypeEnum PhoneType { get; set; }
    public required string PhoneNumber { get; set; }

    public int ApplicationUserid { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}