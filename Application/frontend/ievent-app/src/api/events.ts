import axios from "axios";
import { API_URL } from "./config";
import { GetAllEventsDto, GetByIdEventDto, AddEventDto, UpdateEventDto } from "../types/dtos/eventss";


const API_URL_Events = `${API_URL}/events`;

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
export const getAllEvents = async (): Promise<GetAllEventsDto[]> => {
  const response = await axiosInstance.get<GetAllEventsDto[]>(API_URL_Events);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch events");
};

export const getEventById = async (id: number): Promise<GetByIdEventDto> => {
  const response = await axiosInstance.get<GetByIdEventDto>(`${API_URL_Events}/${id}`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch event");
};

export const addEvent = async (event: AddEventDto): Promise<void> => {
  const response = await axiosInstance.post(API_URL_Events, event);
  if (response.status === 200 || response.status === 201) return;
  throw new Error("Failed to add event");
};

export const updateEvent = async (event: UpdateEventDto): Promise<void> => {
  const response = await axiosInstance.put(`${API_URL_Events}/${event.id}`, event);
  if (response.status === 200) return;
  throw new Error("Failed to update event");
};

export const deleteEvent = async (id: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL_Events}/${id}`);
  if (response.status === 200) return;
  throw new Error("Failed to delete event");
};

// Additional APIs to fetch dropdown data
export const getAllArtists = async (): Promise<{ id: number; name: string }[]> => {
  const response = await axiosInstance.get(`${API_URL}/artists`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch artists");
};

export const getAllLocations = async (): Promise<{ id: number; name: string }[]> => {
  const response = await axiosInstance.get(`${API_URL}/locations`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch locations");
};

export const getAllGenres = async (): Promise<{ id: number; name: string }[]> => {
  const response = await axiosInstance.get(`${API_URL}/genres`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch genres");
};

export const getAllEventTypes = async (): Promise<{ id: number; name: string }[]> => {
  const response = await axiosInstance.get(`${API_URL}/event-types`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch event types");
};
