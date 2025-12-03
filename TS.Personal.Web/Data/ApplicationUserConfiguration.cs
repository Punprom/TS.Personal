using Microsoft.EntityFrameworkCore;

namespace TS.Personal.Web.Data;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(
        Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.MAXNAMELENGTH);
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(Constants.MAXNAMELENGTH);
        builder.Property(u => u.DateOfBirth)
            .IsRequired();
        builder.Property(u => u.Gender)
            .HasMaxLength(Constants.MAXGENDERLENGTH);

        
    }
}