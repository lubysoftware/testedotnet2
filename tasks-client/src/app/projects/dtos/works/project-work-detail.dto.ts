import { DeveloperListDto } from "src/app/developers/dtos/developer-list.dto";
import { ProjectListDto } from "../project-list.dto";

export interface ProjectWorkDetailDto {
  id: string;
  startTime: string;
  endTime: string;
  developer: DeveloperListDto;
  project: ProjectListDto;
  comment: string;
  hours: number;
}
