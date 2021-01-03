import { DeveloperListDto } from "src/app/developers/dtos/developer-list.dto";

export interface ProjectDetailDto {
  id: string;
  title: string;
  description: string;
  developers: DeveloperListDto[];
}
