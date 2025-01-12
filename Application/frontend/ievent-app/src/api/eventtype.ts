import axios from "axios";
import { API_URL } from "./config";
import { AddEventTypeDto, GetAllEventTypesDto, GetByIdEventTypeDto, UpdateEventTypeDto } from "../types/dtos/eventtypeDto";


const API_URL_EventTypes = `${API_URL}/event-types`;

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
export const getAllEventTypes = async (): Promise<GetAllEventTypesDto[]> => {
  const response = await axiosInstance.get<GetAllEventTypesDto[]>(API_URL_EventTypes);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch event types");
};

export const getEventTypeById = async (id: number): Promise<GetByIdEventTypeDto> => {
  const response = await axiosInstance.get<GetByIdEventTypeDto>(
    `${API_URL_EventTypes}/${id}`
  );
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch event type");
};

export const addEventType = async (eventType: AddEventTypeDto): Promise<void> => {
  const response = await axiosInstance.post(API_URL_EventTypes, eventType);
  if (response.status === 200 || response.status === 201) return;
  throw new Error("Failed to add event type");
};

export const updateEventType = async (eventType: UpdateEventTypeDto): Promise<void> => {
  const response = await axiosInstance.put(
    `${API_URL_EventTypes}/${eventType.id}`,
    eventType
  );
  if (response.status === 200) return;
  throw new Error("Failed to update event type");
};

export const deleteEventType = async (id: number): Promise<void> => {
  const response = await axiosInstance.delete(`${API_URL_EventTypes}/${id}`);
  if (response.status === 200) return;
  throw new Error("Failed to delete event type");
};
