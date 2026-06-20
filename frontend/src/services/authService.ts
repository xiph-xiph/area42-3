import axios from "axios";

import type SuccessMessageDto from "../types/SuccessMessageDto";

const API_URL = "/api/auth";

export const register = async (
  name: string,
  phone: string,
  email: string,
  password: string,
): Promise<SuccessMessageDto> => {
  const response = await axios.post<SuccessMessageDto>(`${API_URL}/register`, {
    name,
    phone,
    email,
    password,
  });
  return response.data;
};
