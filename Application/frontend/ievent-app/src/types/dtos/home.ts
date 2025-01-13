export interface UserEventDto {
  id: number;
  name: string;
  description?: string; // Optional field
  date?: Date; // Optional field
  locationName?: string; // Optional field
  eventTypeName?: string; // Optional field
  genreName?: string; // Optional field
  mainArtistName?: string; // Optional field
  theme?: string; // Optional field
}
