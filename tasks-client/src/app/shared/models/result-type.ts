export interface ResultType<T> {
  totalRows: number | null;
  data: T;
  success: boolean;
  errorMessages: string[];
}
