export interface AddEventTypeDto {
  name: string;
  description: string;
}

export interface GetAllEventTypesDto {
  id: number;
  name: string;
  description: string;
}

export interface GetByIdEventTypeDto {
  id: number;
  name: string;
  description: string;
}

export interface UpdateEventTypeDto {
  id: number;
  name: string;
  description: string;
}
