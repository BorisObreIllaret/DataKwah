import {IFilterResponse} from './i-filter-response.model';

export class ProductDetailsResponse implements IFilterResponse<ProductDetailsResponseData> {
  count: number;
  items: ProductDetailsResponseData[];
  productLabel: string;

  constructor(init?: Partial<ProductDetailsResponse>) {
    this.count = init?.count ?? 0;
    this.items = init?.items ?? [];
    this.productLabel = init?.productLabel ?? '';
  }
}

export class ProductDetailsResponseData {
  id: number;
  asin: string;
  body: string;
  title: string;
  rating?: number;
  date?: Date;

  constructor(init?: Partial<ProductDetailsResponseData>) {
    this.id = init?.id ?? 0;
    this.asin = init?.asin ?? '';
    this.body = init?.body ?? '';
    this.title = init?.title ?? '';
    this.rating = init?.rating ?? undefined;
    this.date = init?.date ?? undefined;
  }
}
