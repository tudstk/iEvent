import axios from "axios";
import { API_URL } from "./config";
import { GetAllGenresDto, GetByIdGenreDto, AddGenreDto, UpdateGenreDto } from "../types/dtos/genreDto";


const API_URL_Genres = `${API_URL}/genres`;

const getAuthToken = (): string | null => {
  return localStorage.getItem("token");
};

const axiosInstance = axios.create();

axiosInstance.interceptors.request.use(
  (config) => {
    const token = getAuthToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// API Requests
export const getAllGenres = async (): Promise<GetAllGenresDto[]> => {
  const response = await axiosInstance.get<GetAllGenresDto[]>(API_URL_Genres);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch genres");
};

export const getGenreById = async (id: number): Promise<GetByIdGenreDto> => {
  const response = await axiosInstance.get<GetByIdGenreDto>(
    `${API_URL_Genres}/${id}`
  );
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch genre");
};

export const addGenre = async (genre: AddGenreDto): Promise<void> => {
  const response = await axiosInstance.post(API_URL_Genres, genre);
  if (response.status === 200) return;
  throw new Error("Failed to add genre");
};

export const updateGenre = async (genre: UpdateGenreDto): Promise<void> => {
  const response = await axiosInstance.put(
    `${API_URL_Genres}/${genre.id}`,
    genre
  );
  if (response.status === 200) return;
  throw new Error("Failed to update genre");
};

export const deleteGenre = async (id: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL_Genres}/${id}`);
  if (response.status === 200) return;
  throw new Error("Failed to delete genre");
};
