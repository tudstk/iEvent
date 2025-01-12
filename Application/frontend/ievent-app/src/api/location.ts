import axios from "axios";
import { API_URL } from "./config";
import { GetAllLocationsDto, GetByIdLocationDto, AddLocationDto, UpdateLocationDto } from "../types/dtos/locationdto";

const API_URL_Locations = `${API_URL}/locations`;

// Function to get the token from local storage
const getAuthToken = (): string | null => {
  return localStorage.getItem("token");
};

// Axios instance with an interceptor to include Authorization headers
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

// API Methods
export const getAllLocations = async (): Promise<GetAllLocationsDto[]> => {
  const response = await axiosInstance.get<GetAllLocationsDto[]>(API_URL_Locations);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch locations");
};

export const getLocationById = async (id: number): Promise<GetByIdLocationDto> => {
  const response = await axiosInstance.get<GetByIdLocationDto>(`${API_URL_Locations}/${id}`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch location");
};

export const addLocation = async (location: AddLocationDto): Promise<void> => {
  const response = await axiosInstance.post(API_URL_Locations, location);
  if (response.status === 200 || response.status === 201) return;
  throw new Error("Failed to add location");
};

export const updateLocation = async (location: UpdateLocationDto): Promise<void> => {
  const response = await axiosInstance.put(`${API_URL_Locations}/${location.id}`, location);
  if (response.status === 200) return;
  throw new Error("Failed to update location");
};

export const deleteLocation = async (id: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL_Locations}/${id}`);
  if (response.status === 200) return;
  throw new Error("Failed to delete location");
};
