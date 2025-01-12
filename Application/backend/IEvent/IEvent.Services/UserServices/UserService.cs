using IEvent.Data.Entities;
using IEvent.Data.Infrastructure;
using IEvent.Services.Shared;
using IEvent.Services.UserServices.Dto;
using IEvent.Shared.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IEvent.Services.UserServices
{
  public class UserService : IUserService
  {
    private readonly IRepository<ApplicationUser> userRepository;
    private readonly IRepository<UserEvent> userEventRepository;
    private readonly IRepository<UserFavoriteArtist> userArtistRepository;
    private readonly IRepository<UserFavoriteGenre> userGenreRepository;
    private readonly IRepository<UserFavoriteLocation> userLocationRepository;
    private readonly IRepository<Event> eventRepository;

    private readonly IUnitOfWork unitOfWork;
    private readonly EnvSettings envSettings;

    public UserService
      (IRepository<UserEvent> userEventRepository,
      IRepository<UserFavoriteArtist> userArtistRepository,
      IRepository<UserFavoriteGenre> userGenreRepository,
      IRepository<UserFavoriteLocation> userLocationRepository,
      IRepository<ApplicationUser> userRepository,
      IRepository<Event> eventRepository,
      IUnitOfWork unitOfWork,
      IOptions<EnvSettings> envSettings)
    {
      this.userEventRepository = userEventRepository;
      this.userArtistRepository = userArtistRepository;
      this.userGenreRepository = userGenreRepository;
      this.userLocationRepository = userLocationRepository;
      this.eventRepository = eventRepository;
      this.userRepository = userRepository;
      this.unitOfWork = unitOfWork;
      this.envSettings = envSettings.Value;
    }

    public async Task AddEventForUser(int personId, int eventId)
    {
      var foundEvent = await eventRepository.Query()
        .SingleOrDefaultAsync(x => x.Id == eventId && !x.IsDeleted);

      if (foundEvent != null)
      {
        await userEventRepository.AddAsync(new UserEvent
        {
          UserId = personId,
          EventId = eventId,
        });

        await unitOfWork.CommitAsync();
      }
    }

    public async Task<GetProfileDto> GetProfileAsync(int personId)
    {
      var user = await userRepository.Query().SingleOrDefaultAsync(x => x.Id == personId);

      if (user != null)
      {
        var myArtists = await userArtistRepository.Query().Include(x => x.Artist).Where(x => x.UserId == personId && !x.Artist.IsDeleted).ToListAsync();
        var myLocations = await userLocationRepository.Query().Include(x => x.Location).Where(x => x.UserId == personId && !x.Location.IsDeleted).ToListAsync();
        var myGenres = await userGenreRepository.Query().Include(x => x.Genre).Where(x => x.UserId == personId && !x.Genre.IsDeleted).ToListAsync();

        return new GetProfileDto
        {
          PersonId = personId,
          UserName = user.UserName,
          Email = user.Email,
          MyArtists = myArtists.Select(x => new DropdownOption
          {
            Id = x.ArtistId,
            Name = x.Artist?.Name,
          }).ToList(),
          MyLocations = myLocations.Select(x => new DropdownOption
          {
            Id = x.LocationId,
            Name = x.Location?.Name,
          }).ToList(),
          MyGenres = myGenres.Select(x => new DropdownOption
          {
            Id = x.GenreId,
            Name = x.Genre?.Name,
          }).ToList(),
        };
      }
      return null;
  }

    public async Task<List<UserEventDto>> GetRecommendedUserEvents(int personId)
    {
      var currentUserEventIds = await userEventRepository.Query()
          .Where(x => x.UserId == personId && !x.Event.IsDeleted)
          .Select(x => x.EventId)
          .ToListAsync();

      var userArtistIds = await userArtistRepository.Query()
          .Where(x => x.UserId == personId && !x.Artist.IsDeleted)
          .Select(x => x.ArtistId)
          .ToListAsync();

      var userLocationIds = await userLocationRepository.Query()
          .Where(x => x.UserId == personId && !x.Location.IsDeleted)
          .Select(x => x.LocationId)
          .ToListAsync();

      var userGenreIds = await userGenreRepository.Query()
          .Where(x => x.UserId == personId && !x.Genre.IsDeleted)
          .Select(x => x.GenreId)
          .ToListAsync();

      // Get recommended events
      var recommendedEvents = await eventRepository.Query()
          .Include(x => x.Location)
          .Include(x => x.EventType)
          .Include(x => x.Genre)
          .Include(x => x.MainArtist)
          .Where(x => !x.IsDeleted &&
                      !currentUserEventIds.Contains(x.Id) &&
                      (userArtistIds.Contains(x.MainArtist.Id) ||
                       userLocationIds.Contains(x.Location.Id) ||
                       userGenreIds.Contains(x.Genre.Id)))
          .ToListAsync();

      return recommendedEvents.Select(x => new UserEventDto
      {
        Id = x.Id,
        Name = x.Name,
        Description = x.Description,
        Date = x.Date,
        LocationName = x.Location?.Name,
        EventTypeName = x.EventType?.Name,
        GenreName = x.Genre?.Name,
        MainArtistName = x.MainArtist?.Name,
        Theme = x.Theme
      }).ToList();
    }

    public async Task<List<UserEventDto>> GetUserEvents(int personId)
    {
      var userEvents = await userEventRepository.Query()
        .Include(x => x.Event)
          .ThenInclude(y => y.Location)
        .Include(x => x.Event)
          .ThenInclude(y => y.EventType)
        .Include(x => x.Event)
          .ThenInclude(y => y.Genre)
        .Include(x => x.Event)
          .ThenInclude(y => y.MainArtist)
        .Where(x => x.UserId == personId && !x.Event.IsDeleted)
        .ToListAsync();

      return userEvents.Select(x => new UserEventDto
      {
        Id = x.Event.Id,
        Name = x.Event.Name,
        Description = x.Event?.Description,
        Date = x.Event?.Date,
        LocationName = x.Event?.Location?.Name,
        EventTypeName = x.Event?.EventType?.Name,
        GenreName = x.Event?.Genre?.Name,
        MainArtistName = x.Event?.MainArtist?.Name,
        Theme = x.Event?.Theme,
      }).ToList();
    }

    public async Task RemoveEventForUser(int personId, int eventId)
    {
      var itemToDelete = await userEventRepository.Query()
        .SingleOrDefaultAsync(x => x.UserId == personId && x.EventId == eventId && !x.Event.IsDeleted);

      if (itemToDelete != null)
      {
        userEventRepository.Delete(itemToDelete);
      }
    }

    public async Task UpdateProfileAsync(int personId, ModifyProfileDto modifyProfileDto)
    {
      var user = await userRepository.Query().SingleOrDefaultAsync(x => x.Id == personId);

      if (user != null)
      {
        user.UserName = modifyProfileDto.UserName;
        user.Email = modifyProfileDto.UserEmail;
      }

      // Synchronize artists
      var existingArtists = await userArtistRepository.Query().Where(x => x.UserId == personId && !x.Artist.IsDeleted).ToListAsync();
      var existingArtistIds = existingArtists.Select(x => x.ArtistId).ToList();

      var artistIdsToAdd = modifyProfileDto.ArtistsIds.Except(existingArtistIds).ToList();
      var artistIdsToRemove = existingArtistIds.Except(modifyProfileDto.ArtistsIds).ToList();

      foreach (var artistId in artistIdsToAdd)
      {
        await userArtistRepository.AddAsync(new UserFavoriteArtist
        {
          UserId = personId,
          ArtistId = artistId
        });
      }

      foreach (var artistId in artistIdsToRemove)
      {
        var artistToRemove = existingArtists.FirstOrDefault(x => x.ArtistId == artistId);
        if (artistToRemove != null)
        {
          userArtistRepository.Delete(artistToRemove);
        }
      }

      // Synchronize locations
      var existingLocations = await userLocationRepository.Query().Where(x => x.UserId == personId && !x.Location.IsDeleted).ToListAsync();
      var existingLocationIds = existingLocations.Select(x => x.LocationId).ToList();

      var locationIdsToAdd = modifyProfileDto.LocationsIds.Except(existingLocationIds).ToList();
      var locationIdsToRemove = existingLocationIds.Except(modifyProfileDto.LocationsIds).ToList();

      foreach (var locationId in locationIdsToAdd)
      {
        await userLocationRepository.AddAsync(new UserFavoriteLocation
        {
          UserId = personId,
          LocationId = locationId
        });
      }

      foreach (var locationId in locationIdsToRemove)
      {
        var locationToRemove = existingLocations.FirstOrDefault(x => x.LocationId == locationId);
        if (locationToRemove != null)
        {
          userLocationRepository.Delete(locationToRemove);
        }
      }

      // Synchronize genres
      var existingGenres = await userGenreRepository.Query().Where(x => x.UserId == personId && !x.Genre.IsDeleted).ToListAsync();
      var existingGenreIds = existingGenres.Select(x => x.GenreId).ToList();

      var genreIdsToAdd = modifyProfileDto.GenresIds.Except(existingGenreIds).ToList();
      var genreIdsToRemove = existingGenreIds.Except(modifyProfileDto.GenresIds).ToList();

      foreach (var genreId in genreIdsToAdd)
      {
        await userGenreRepository.AddAsync(new UserFavoriteGenre
        {
          UserId = personId,
          GenreId = genreId
        });
      }

      foreach (var genreId in genreIdsToRemove)
      {
        var genreToRemove = existingGenres.FirstOrDefault(x => x.GenreId == genreId);
        if (genreToRemove != null)
        {
          userGenreRepository.Delete(genreToRemove);
        }
      }

      await unitOfWork.CommitAsync();
    }
  }
}
