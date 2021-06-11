export interface IFilterRequest {
  page?: number;
  limit?: number;
  sort?: string;
  ascendingOrder?: boolean;
  search?: string;
}
