export interface WorkCreateDto {
  id: string;
  projectId: string;
  developerId: string;
  startTime: string;
  endTime: string;
  comment: string;
  hours: number;
}
