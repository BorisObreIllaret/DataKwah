import {IFilterRequest} from './i-filter-request.model';

export class ProductFilterRequest implements IFilterRequest {
  page?: number;
  limit?: number;
  sort?: string;
  ascendingOrder?: boolean;
  search?: string;

  constructor(init?: Partial<ProductFilterRequest>) {
    this.page = init?.page ?? undefined;
    this.limit = init?.limit ?? undefined;
    this.sort = init?.sort ?? undefined;
    this.ascendingOrder = init?.ascendingOrder ?? undefined;
    this.search = init?.search ?? undefined;
  }
}
