import { Pagination } from "src/app/shared/models/pagination";

export interface ProjectWorkSearchDto extends Pagination {
  developerId: string | null;
}
