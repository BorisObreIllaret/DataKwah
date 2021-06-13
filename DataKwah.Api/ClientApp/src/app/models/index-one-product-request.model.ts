export class IndexOneProductRequest {
  asin: string;

  constructor(init?: Partial<IndexOneProductRequest>) {
    this.asin = init?.asin ?? '';
  }
}
