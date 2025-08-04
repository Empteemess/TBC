namespace Infrastructure.DbConfigurations;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(pk => pk.Id).ValueGeneratedOnAdd();
    }
}