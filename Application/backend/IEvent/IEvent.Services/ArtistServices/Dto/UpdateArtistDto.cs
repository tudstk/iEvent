﻿namespace IEvent.Services.ArtistServices.Dto
{
  public class UpdateArtistDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; } = string.Empty;
  }
}
