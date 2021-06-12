import {Component, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {ProductFilterResponseData} from '../models/product-filter-response.model';
import {ProductFilterRequest} from '../models/product-filter-request.model';

@Component({
  selector: 'kwah-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products: ProductFilterResponseData[] = [];
  count: number = 0;
  first = 0;
  rows = 10;
  loading = true;

  constructor(private productService: ProductService) {
  }

  ngOnInit(): void {
    this.filterProducts(0, this.rows, '', true, '');
  }

  loadProducts(event: any) {
    this.first = event.first;
    this.rows = event.rows;
    this.filterProducts(event.first / event.rows, event.rows, event.sortField, !!event.sortOrder, '');
  }

  private filterProducts(page: number, limit: number, sort: string, ascendingOrder: boolean, search: string): void {
    this.loading = true;
    this.productService.filterProducts(new ProductFilterRequest({
      page,
      limit,
      sort,
      ascendingOrder,
      search,
    })).subscribe(response => {
      this.products = response.items;
      this.count = response.count;
      this.loading = false;
    })
  }
}
