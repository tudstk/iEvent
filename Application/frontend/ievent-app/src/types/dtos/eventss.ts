export interface AddEventDto {
  name: string;
  createdByPersonId: number;
  description?: string;
  date?: Date;
  locationId?: number;
  eventTypeId?: number;
  genreId?: number;
  mainArtistId?: number;
  theme?: string;
}

export interface GetAllEventsDto {
  id: number;
  createdByPersonId: number;
  name: string;
  description?: string;
  date?: Date;
  locationName?: string;
  eventTypeName?: string;
  genreName?: string;
  mainArtistName?: string;
  theme?: string;
}

export interface GetByIdEventDto {
  id: number;
  createdByPersonId: number;
  name: string;
  description?: string;
  date?: Date;
  locationName?: string;
  eventTypeName?: string;
  genreName?: string;
  mainArtistName?: string;
  theme?: string;
}

export interface UpdateEventDto {
  id: number;
  name: string;
  description?: string;
  date?: Date;
  locationId?: number;
  eventTypeId?: number;
  genreId?: number;
  mainArtistId?: number;
  theme?: string;
}
