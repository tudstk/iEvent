import axios from "axios";
import { API_URL } from "./config";
import { GetProfileDto } from "../types/dtos/profile";
import { UserEventDto } from "../types/dtos/home";


const axiosInstance = axios.create();

axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export const getProfile = async (): Promise<GetProfileDto> => {
  const response = await axiosInstance.get<GetProfileDto>(`${API_URL}/users/profile`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch profile");
};

export const getMyEvents = async (): Promise<UserEventDto[]> => {
  const response = await axiosInstance.get<UserEventDto[]>(`${API_URL}/all-user-events`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch my events");
};

export const getRecommendedEvents = async (): Promise<UserEventDto[]> => {
  const response = await axiosInstance.get<UserEventDto[]>(`${API_URL}/recommendations`);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch recommended events");
};

export const addEventToMyList = async (eventId: number): Promise<void> => {
  const response = await axiosInstance.post(`${API_URL}/event`, eventId, {
    headers: { "Content-Type": "application/json" },
  });
  if (response.status !== 200) throw new Error("Failed to add event to my list");
};

export const removeEventFromMyList = async (eventId: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL}/event`, {
    data: eventId,
    headers: { "Content-Type": "application/json" },
  });
  if (response.status !== 200) throw new Error("Failed to remove event from my list");
};
