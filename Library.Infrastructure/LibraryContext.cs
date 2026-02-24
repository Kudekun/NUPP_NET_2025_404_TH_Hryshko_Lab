using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class LibraryContext : DbContext
{
    public DbSet<LibraryItemModel> LibraryItems { get; set; }
    public DbSet<BookModel> Books { get; set; }
    public DbSet<MagazineModel> Magazines { get; set; }
    public DbSet<MemberModel> Members { get; set; }
    public DbSet<LibraryCardModel> LibraryCards { get; set; }
    public DbSet<LoanModel> Loans { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPT — Table-per-Type для LibraryItemModel
        modelBuilder.Entity<LibraryItemModel>().ToTable("LibraryItems");
        modelBuilder.Entity<BookModel>().ToTable("Books");
        modelBuilder.Entity<MagazineModel>().ToTable("Magazines");

        // LibraryItemModel
        modelBuilder.Entity<LibraryItemModel>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Guid).IsRequired();
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.Year).IsRequired();
        });

        // BookModel
        modelBuilder.Entity<BookModel>(e =>
        {
            e.Property(x => x.Author).IsRequired().HasMaxLength(100);
            e.Property(x => x.ISBN).HasMaxLength(20);
            e.Property(x => x.Pages).IsRequired();
        });

        // MagazineModel
        modelBuilder.Entity<MagazineModel>(e =>
        {
            e.Property(x => x.Publisher).HasMaxLength(100);
            e.Property(x => x.Topic).HasMaxLength(100);
        });

        // MemberModel
        modelBuilder.Entity<MemberModel>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Guid).IsRequired();
            e.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            e.Property(x => x.Email).HasMaxLength(100);

            // 1-до-1: Member → LibraryCard
            e.HasOne(x => x.LibraryCard)
             .WithOne(x => x.Member)
             .HasForeignKey<LibraryCardModel>(x => x.MemberId);

            // 1-до-багатьох: Member → Loans
            e.HasMany(x => x.Loans)
             .WithOne(x => x.Member)
             .HasForeignKey(x => x.MemberId);
        });

        // LibraryCardModel
        modelBuilder.Entity<LibraryCardModel>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.CardNumber).IsRequired().HasMaxLength(20);
        });

        // LoanModel
        modelBuilder.Entity<LoanModel>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Guid).IsRequired();

            // 1-до-багатьох: LibraryItem → Loans
            e.HasOne(x => x.LibraryItem)
             .WithMany(x => x.Loans)
             .HasForeignKey(x => x.LibraryItemId);
        });
    }
}
