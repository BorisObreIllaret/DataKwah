import {IFilterResponse} from './i-filter-response.model';
import {ProductIndexationState} from './product-indexation-state.enum';

export class ProductFilterResponse implements IFilterResponse<ProductFilterResponseData> {
  count: number;
  items: ProductFilterResponseData[];

  constructor(init?: Partial<ProductFilterResponse>) {
    this.count = init?.count ?? 0;
    this.items = init?.items ?? [];
  }
}

export class ProductFilterResponseData {
  id: number;
  asin: string;
  label: string;
  state: ProductIndexationState;

  constructor(init?: Partial<ProductFilterResponseData>) {
    this.id = init?.id ?? 0;
    this.asin = init?.asin ?? '';
    this.label = init?.label ?? '';
    this.state = init?.state ?? ProductIndexationState.Requested;
  }
}
