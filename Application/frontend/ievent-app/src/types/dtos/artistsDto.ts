export interface AddArtistDto {
  name: string;
  description: string;
}

export interface UpdateArtistDto {
  id: number;
  name: string;
  description: string;
}

export interface GetAllArtistDto {
  id: number;
  name: string;
  description: string;
}

export interface GetByIdArtistDto {
  id: number;
  name: string;
  description: string;
}
