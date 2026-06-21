import type SuccessMessageDto from "@/types/SuccessMessageDto";

export default interface TokenDto extends SuccessMessageDto {
  token: string;
}
