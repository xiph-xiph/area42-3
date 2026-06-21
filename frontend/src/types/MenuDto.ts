import type SuccessMessageDto from "./SuccessMessageDto";

export interface MenuItem {
  id: number;
  price: number;
  title: string;
  imageUrl: string;
  category: string;
  isPopular: boolean;
}

export default interface MenuDto extends SuccessMessageDto {
  menu: MenuItem[];
}
