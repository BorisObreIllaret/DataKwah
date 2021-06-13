import {IFilterRequest} from './i-filter-request.model';

export class ProductDetailsRequest implements IFilterRequest  {
  productId: number;
  page?: number;
  limit?: number;
  sort?: string;
  ascendingOrder?: boolean;
  search?: string;

  constructor(init?: Partial<ProductDetailsRequest>) {
    this.productId = init?.productId ?? 0;
    this.page = init?.page ?? undefined;
    this.limit = init?.limit ?? undefined;
    this.sort = init?.sort ?? undefined;
    this.ascendingOrder = init?.ascendingOrder ?? undefined;
    this.search = init?.search ?? undefined;
  }
}
