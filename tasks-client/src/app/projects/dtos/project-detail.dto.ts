import { DeveloperListDto } from "src/app/developer/dtos/developer-list.dto";

export interface ProjectDetailDto {
  id: string;
  title: string;
  description: string;
  developers: DeveloperListDto[];
}
