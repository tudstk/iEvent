export interface DropdownOption {
  id: number;
  name: string;
}

export interface GetProfileDto {
  personId: number;
  userName: string;
  email: string;
  myArtists: DropdownOption[];
  myLocations: DropdownOption[];
  myGenres: DropdownOption[];
}

export interface ModifyProfileDto {
  userName: string;
  userEmail: string;
  artistsIds: number[];
  locationsIds: number[];
  genresIds: number[];
  eventTypesIds: number[];
}
