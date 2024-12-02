namespace IEvent.Data
{
  using IEvent.Data.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;

  public class IEventContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
  {
    public IEventContext(DbContextOptions<IEventContext> options) : base(options)
    {

    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<TestEntity> TestEntities { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<UserFavoriteArtist> UserFavoriteArtists { get; set; }

    public DbSet<UserEvent> UserEvents { get; set; }

    public DbSet<UserFavoriteGenre> UserFavoriteGenres { get; set; }

    public DbSet<UserFavoriteLocation> UserFavoriteLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // ApplicationUser Relationships
      modelBuilder.Entity<ApplicationUser>()
          .HasMany(u => u.UserEvents)
          .WithOne(ue => ue.User)
          .HasForeignKey(ue => ue.UserId);

      modelBuilder.Entity<ApplicationUser>()
          .HasMany(u => u.UserFavoriteLocations)
          .WithOne(u => u.User)
          .HasForeignKey(u => u.UserId);

      modelBuilder.Entity<ApplicationUser>()
          .HasMany(u => u.UserFavoriteGenres)
          .WithOne(u => u.User)
          .HasForeignKey(u => u.UserId);

      modelBuilder.Entity<ApplicationUser>()
          .HasMany(u => u.UserFavoriteArtists)
          .WithOne(u => u.User)
          .HasForeignKey(u => u.UserId);

      // Artist Relationships
      modelBuilder.Entity<Artist>()
          .HasMany(a => a.UserFavoriteArtists)
          .WithOne(ufa => ufa.Artist)
          .HasForeignKey(ufa => ufa.ArtistId);

      modelBuilder.Entity<Artist>()
          .HasMany(a => a.ArtistEvents)
          .WithOne(e => e.MainArtist)
          .HasForeignKey(e => e.MainArtistId);

      // Event Relationships
      modelBuilder.Entity<Event>()
          .HasMany(e => e.UserEvents)
          .WithOne(ue => ue.Event)
          .HasForeignKey(ue => ue.EventId);

      modelBuilder.Entity<Event>()
          .HasOne(e => e.Location)
          .WithMany(l => l.LocationEvents)
          .HasForeignKey(e => e.LocationId);

      modelBuilder.Entity<Event>()
          .HasOne(e => e.EventType)
          .WithMany()
          .HasForeignKey(e => e.EventTypeId);

      modelBuilder.Entity<Event>()
          .HasOne(e => e.Genre)
          .WithMany(g => g.GenreEvents)
          .HasForeignKey(e => e.GenreId);

      modelBuilder.Entity<Event>()
          .HasOne(e => e.MainArtist)
          .WithMany(a => a.ArtistEvents)
          .HasForeignKey(e => e.MainArtistId);

      // Genre Relationships
      modelBuilder.Entity<Genre>()
          .HasMany(g => g.UserFavoriteGenres)
          .WithOne(ufg => ufg.Genre)
          .HasForeignKey(ufg => ufg.GenreId);

      modelBuilder.Entity<Genre>()
          .HasMany(g => g.GenreEvents)
          .WithOne(e => e.Genre)
          .HasForeignKey(e => e.GenreId);

      // Location Relationships
      modelBuilder.Entity<Location>()
          .HasMany(l => l.UserFavoriteLocations)
          .WithOne(u => u.Location)
          .HasForeignKey(u => u.LocationId);

      modelBuilder.Entity<Location>()
          .HasMany(l => l.LocationEvents)
          .WithOne(e => e.Location)
          .HasForeignKey(e => e.LocationId);

      // UserFavoriteArtist Relationships
      modelBuilder.Entity<UserFavoriteArtist>()
          .HasOne(ufa => ufa.User)
          .WithMany(u => u.UserFavoriteArtists)
          .HasForeignKey(ufa => ufa.UserId);

      modelBuilder.Entity<UserFavoriteArtist>()
          .HasOne(ufa => ufa.Artist)
          .WithMany(a => a.UserFavoriteArtists)
          .HasForeignKey(ufa => ufa.ArtistId);

      // UserEvent Relationships
      modelBuilder.Entity<UserEvent>()
          .HasOne(ue => ue.User)
          .WithMany(u => u.UserEvents)
          .HasForeignKey(ue => ue.UserId);

      modelBuilder.Entity<UserEvent>()
          .HasOne(ue => ue.Event)
          .WithMany(e => e.UserEvents)
          .HasForeignKey(ue => ue.EventId);

      // UserFavoriteGenre Relationships
      modelBuilder.Entity<UserFavoriteGenre>()
          .HasOne(ufg => ufg.User)
          .WithMany(u => u.UserFavoriteGenres)
          .HasForeignKey(ufg => ufg.UserId);

      modelBuilder.Entity<UserFavoriteGenre>()
          .HasOne(ufg => ufg.Genre)
          .WithMany(g => g.UserFavoriteGenres)
          .HasForeignKey(ufg => ufg.GenreId);

      // UserFavoriteLocation Relationships
      modelBuilder.Entity<UserFavoriteLocation>()
          .HasOne(u => u.User)
          .WithMany(u => u.UserFavoriteLocations)
          .HasForeignKey(u => u.UserId);

      modelBuilder.Entity<UserFavoriteLocation>()
          .HasOne(u => u.Location)
          .WithMany(l => l.UserFavoriteLocations)
          .HasForeignKey(u => u.LocationId);
    }
  }
}
