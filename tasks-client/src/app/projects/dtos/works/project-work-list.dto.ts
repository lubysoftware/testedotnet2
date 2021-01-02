import { DeveloperListDto } from "src/app/developer/dtos/developer-list.dto";

export interface ProjectWorkListDto {
  id: string;
  startTime: string;
  endTime: string;
  developer: DeveloperListDto;
  comment: string;
  hours: number;
}
