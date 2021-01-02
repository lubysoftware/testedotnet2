import { Pagination } from "src/app/shared/models/pagination";

export interface DeveloperWorkSearchDto extends Pagination {
  projectId: string | null;
}
