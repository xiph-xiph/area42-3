import axios from "axios";

import type MenuDto from "../types/MenuDto";

const API_URL = "/api/menu";

export const getMenu = async (): Promise<MenuDto> => {
  const response = await axios.get<MenuDto>(`${API_URL}`, {
    headers: {
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });
  return response.data;
};
