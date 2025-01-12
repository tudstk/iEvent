import axios from "axios";
import { TokenRequest } from "../types/requests/account/tokenRequest";
import { API_URL } from "./config";
import { LoginRequest } from "../types/requests/account/loginRequest";
import { AuthResponse } from "../types/responses/account/authResponse";
import { RegisterRequest } from "../types/requests/account/registerRequest";

export const verifyToken = async (tokenRequest: TokenRequest): Promise<boolean> => {
  try {
    const response = await axios.get(`${API_URL}/account/check-token`, {
      headers: { Authorization: `Bearer ${tokenRequest.token}` },
    });

    return response.status === 200;
  } catch (error) {
    console.error("Token verification failed:", error);
    return false;
  }
};

export const login = async (loginRequest: LoginRequest): Promise<boolean> => {
  try {
    const response = await axios.post<AuthResponse>(`${API_URL}/account/login`, loginRequest);

    if (response.status === 200) {
      localStorage.setItem("token", response.data.token);
      return true;
    }
    return false;
  } catch (error) {
    console.error("Login failed:", error);
    return false;
  }
};

export const register = async (registerRequest: RegisterRequest): Promise<boolean> => {
  try {
    const response = await axios.post<AuthResponse>(`${API_URL}/account/register`, registerRequest);

    if (response.status === 200) {
      localStorage.setItem("token", response.data.token);
      return true;
    }
    return false;
  } catch (error) {
    console.error("Registration failed:", error);
    return false;
  }
};
