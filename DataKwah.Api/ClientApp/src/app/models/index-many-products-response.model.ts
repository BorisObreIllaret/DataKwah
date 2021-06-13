export class IndexManyProductsResponse {
  indexingCount: number;

  constructor(init?: Partial<IndexManyProductsResponse>) {
    this.indexingCount = init?.indexingCount ?? 0;
  }
}
