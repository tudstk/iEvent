import axios from "axios";
import { API_URL } from "./config";
import {
  AddArtistDto,
  GetAllArtistDto,
  GetByIdArtistDto,
  UpdateArtistDto,
} from "../types/dtos/artistsDto";

const API_URL_Artist = `${API_URL}/artists`;

// Function to get the token from local storage
const getAuthToken = (): string | null => {
  return localStorage.getItem("token");
};

// Axios instance with interceptor for attaching the token
const axiosInstance = axios.create();

// Add a request interceptor
axiosInstance.interceptors.request.use(
  (config) => {
    const token = getAuthToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// API calls
export const getAllArtists = async (): Promise<GetAllArtistDto[]> => {
  const response = await axiosInstance.get<GetAllArtistDto[]>(API_URL_Artist);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch artists");
};

export const getArtistById = async (id: number): Promise<GetByIdArtistDto> => {
  const response = await axiosInstance.get<GetByIdArtistDto>(
    `${API_URL_Artist}/${id}`
  );
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch artist");
};

export const addArtist = async (artist: AddArtistDto): Promise<void> => {
  const response = await axiosInstance.post(API_URL_Artist, artist);
  if (response.status === 200) return;
  throw new Error("Failed to add artist");
};

export const updateArtist = async (artist: UpdateArtistDto): Promise<void> => {
  const response = await axiosInstance.put(
    `${API_URL_Artist}/${artist.id}`,
    artist
  );
  if (response.status === 200) return;
  throw new Error("Failed to update artist");
};

export const deleteArtist = async (id: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL_Artist}/${id}`);
  if (response.status === 200) return;
  throw new Error("Failed to delete artist");
};
