export class IndexOneProductResponse {
  asin: string;
  label: string;
  state: number;

  constructor(init?: Partial<IndexOneProductResponse>) {
    this.asin = init?.asin ?? '';
    this.label = init?.label ?? '';
    this.state = init?.state ?? 0;
  }
}
