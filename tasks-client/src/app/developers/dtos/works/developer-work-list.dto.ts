import { ProjectListDto } from "src/app/projects/dtos/project-list.dto";

export interface DeveloperWorkListDto {
  id: string;
  startTime: string;
  endTime: string;
  project: ProjectListDto;
  comment: string;
  hours: number;
}
