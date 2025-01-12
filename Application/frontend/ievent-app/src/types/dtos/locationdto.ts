export interface AddLocationDto {
  name: string;
  description?: string;
}

export interface GetAllLocationsDto {
  id: number;
  name: string;
  description?: string;
}

export interface GetByIdLocationDto {
  id: number;
  name: string;
  description?: string;
}

export interface UpdateLocationDto {
  id: number;
  name: string;
  description?: string;
}
