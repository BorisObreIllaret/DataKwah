export class IndexManyProductsRequest {
  asins: string[];

  constructor(init?: Partial<IndexManyProductsRequest>) {
    this.asins = init?.asins ?? [];
  }
}
