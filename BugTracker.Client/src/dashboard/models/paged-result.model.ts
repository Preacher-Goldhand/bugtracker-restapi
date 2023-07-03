export interface PagedResult<T> {
  items: T[];
  totalItemsCount: number;
  pageSize: number;
  pageNumber: number;
  totalPages: number;
}
