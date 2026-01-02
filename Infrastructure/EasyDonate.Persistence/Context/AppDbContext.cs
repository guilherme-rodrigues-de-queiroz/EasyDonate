using EasyDonate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyDonate.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<Donor> Donors => Set<Donor>();
    public DbSet<Ong> Ongs => Set<Ong>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        /* [User] */
        mb.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        mb.Entity<User>()
            .Property(u => u.Password)
            .HasMaxLength(60);

        /* [Ong] */
        mb.Entity<Ong>()
            .Property(o => o.Name)
            .HasMaxLength(255);

        mb.Entity<Ong>()
            .Property(o => o.Cnpj)
            .HasMaxLength(14);

        mb.Entity<Ong>()
            .HasIndex(o => o.Cnpj)
            .IsUnique();

        mb.Entity<Ong>()
            .Property(o => o.ActivityType)
            .HasMaxLength(255);

        mb.Entity<Ong>()
            .Property(o => o.Description)
            .HasMaxLength(1000);

        mb.Entity<Ong>()
            .Property(o => o.Ddd)
            .HasMaxLength(2);

        mb.Entity<Ong>()
            .Property(o => o.Phone)
            .HasMaxLength(9);

        mb.Entity<Ong>()
            .Property(o => o.RegistrationManager)
            .HasMaxLength(255);

        /* [Donor] */
        mb.Entity<Donor>()
            .Property(d => d.Name)
            .HasMaxLength(255);

        mb.Entity<Donor>()
            .Property(d => d.SocialName)
            .HasMaxLength(150);

        mb.Entity<Donor>()
            .Property(d => d.CpfCnpj)
            .HasMaxLength(14);

        mb.Entity<Donor>()
            .HasIndex(d => d.CpfCnpj)
            .IsUnique();

        mb.Entity<Donor>()
            .Property(d => d.BirthDate)
            .HasColumnType("DATE");

        mb.Entity<Donor>()
            .Property(d => d.Ddd)
            .HasMaxLength(2);

        mb.Entity<Donor>()
            .Property(d => d.Phone)
            .HasMaxLength(9);

        /* [Address] */
        mb.Entity<Address>()
            .Property(a => a.Cep)
            .HasMaxLength(8);

        mb.Entity<Address>()
            .Property(a => a.StreetAddress)
            .HasMaxLength(255);

        mb.Entity<Address>()
            .Property(a => a.Number)
            .HasMaxLength(20);

        mb.Entity<Address>()
            .Property(a => a.Complement)
            .HasMaxLength(150);

        mb.Entity<Address>()
            .Property(a => a.Neighborhood)
            .HasMaxLength(150);

        mb.Entity<Address>()
            .Property(a => a.City)
            .HasMaxLength(100);

        mb.Entity<Address>()
            .Property(a => a.State)
            .HasMaxLength(2);

        mb.Entity<Address>()
            .Property(a => a.GoogleLocation)
            .HasMaxLength(255);

        /* [Donation] */
        mb.Entity<Donation>()
            .Property(d => d.Description)
            .HasMaxLength(255);

        mb.Entity<Donation>()
            .Property(d => d.ItemCondition)
            .HasMaxLength(100);

        mb.Entity<Donation>()
            .Property(d => d.Amount)
            .HasColumnType("DECIMAL(18,2)");

        /* [Relacionamentos] */
        /* [1 Ong -> 1 User] */
        mb.Entity<Ong>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        /* [1 User -> 1 Unique Ong] */
        mb.Entity<Ong>()
            .HasIndex(o => o.UserId)
            .IsUnique();

        /* [1 Ong -> 1 Address] */
        mb.Entity<Ong>()
            .HasOne(o => o.Address)
            .WithOne(a => a.Ong)
            .HasForeignKey<Address>(a => a.OngId);

        /* [1 Donor -> 1 User] */
        mb.Entity<Donor>()
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        /* [1 User -> 1 Unique Donor] */
        mb.Entity<Donor>()
            .HasIndex(d => d.UserId)
            .IsUnique();

        /* [1 Donor -> 1 Address] */
        mb.Entity<Donor>()
            .HasOne(d => d.Address)
            .WithOne(a => a.Donor)
            .HasForeignKey<Address>(a => a.DonorId);

        /* [N Donations -> 1 Donor] */
        mb.Entity<Donation>()
            .HasOne(d => d.Donor)
            .WithMany(dn => dn.Donations)
            .HasForeignKey(d => d.DonorId)
            .OnDelete(DeleteBehavior.Cascade);

        /* [N Donations -> 1 Ong] */
        mb.Entity<Donation>()
            .HasOne(d => d.Ong)
            .WithMany(o => o.Donations)
            .HasForeignKey(d => d.OngId)
            .OnDelete(DeleteBehavior.Cascade);

        /* [1 Donation -> 1 Appointment] */
        mb.Entity<Donation>()
            .HasOne(d => d.Appointment)
            .WithOne(a => a.Donation)
            .HasForeignKey<Appointment>(a => a.DonationId)
            .OnDelete(DeleteBehavior.Cascade);

        /* [Address ? Donor : Ong] */
        mb.Entity<Address>(b =>
        {
            b.ToTable(t => t.HasCheckConstraint(
                "CK_Address_OnlyOneOwner",
                "(DonorId IS NOT NULL AND OngId IS NULL) OR (DonorId IS NULL AND OngId IS NOT NULL)"
            ));

            b.HasKey(a => a.Id);

            b.Property(a => a.DonorId);
            b.Property(a => a.OngId);
        });
    }
}