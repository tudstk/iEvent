import axios from "axios";
import { API_URL } from "./config";
import { GetProfileDto, ModifyProfileDto } from "../types/dtos/profile";
const API_URL_PROFILE = `${API_URL}/users/profile`;

const getAuthToken = (): string | null => localStorage.getItem("token");

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

export const getProfile = async (): Promise<GetProfileDto> => {
  const response = await axiosInstance.get<GetProfileDto>(API_URL_PROFILE);
  if (response.status === 200) return response.data;
  throw new Error("Failed to fetch profile");
};

export const updateProfile = async (profile: ModifyProfileDto): Promise<void> => {
  const response = await axiosInstance.put(API_URL_PROFILE, profile);
  if (response.status === 200) return;
  throw new Error("Failed to update profile");
};
