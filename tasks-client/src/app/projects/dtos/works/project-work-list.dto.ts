import { DeveloperListDto } from "src/app/developers/dtos/developer-list.dto";

export interface ProjectWorkListDto {
  id: string;
  startTime: string;
  endTime: string;
  developer: DeveloperListDto;
  comment: string;
  hours: number;
}
