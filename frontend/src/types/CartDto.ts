import type { MenuItem } from "./MenuDto";
import type SuccessMessageDto from "./SuccessMessageDto";

export interface OrderItem extends MenuItem {
  quantity: number;
}

export interface Order {
  id: number;
  name: string;
  phone: string;
  remarks: string;
  userId: number;
  status: number;
  totalPrice: number;
  pickupTime: Date;
  items: OrderItem[];
}

export default interface CartDto extends SuccessMessageDto {
  cart: Order;
}
