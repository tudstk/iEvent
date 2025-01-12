export interface AddGenreDto {
  name: string;
  description: string;
}

export interface GetAllGenresDto {
  id: number;
  name: string;
  description: string;
}

export interface GetByIdGenreDto {
  id: number;
  name: string;
  description: string;
}

export interface UpdateGenreDto {
  id: number;
  name: string;
  description: string;
}
