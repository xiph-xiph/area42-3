import axios from "axios";

// import type SuccessMessageDto from "../types/SuccessMessageDto";
import type CartDto from "../types/CartDto";
import type SuccessMessageDto from "../types/SuccessMessageDto";

const API_URL = "/api/order";

export const getCart = async (): Promise<CartDto> => {
  const response = await axios.get<CartDto>(`${API_URL}/cart`, {
    headers: {
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });
  return response.data;
};

export const addToCart = async (
  menuItemId: number,
  quantity: number,
): Promise<SuccessMessageDto> => {
  const response = await axios.post<SuccessMessageDto>(
    `${API_URL}/cart/add`,
    { menuItemId, quantity },
    {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    },
  );
  return response.data;
};

export const removeFromCart = async (
  orderItemId: number,
  quantity: number,
): Promise<SuccessMessageDto> => {
  const response = await axios.post<SuccessMessageDto>(
    `${API_URL}/cart/remove`,
    { orderItemId, quantity },
    {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    },
  );
  return response.data;
};

export const checkoutCart = async (
  name: string,
  phone: string,
  pickupTime: Date,
  remarks: string,
): Promise<SuccessMessageDto> => {
  const response = await axios.post<SuccessMessageDto>(
    `${API_URL}/cart/checkout`,
    { name, phone, pickupTime, remarks },
    { headers: { Authorization: `Bearer ${localStorage.getItem("token")}` } },
  );
  return response.data;
};
