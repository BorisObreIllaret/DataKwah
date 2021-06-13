import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {ProductService} from '../services/product.service';
import {ProductDetailsResponseData} from '../models/product-details-response.model';
import {ProductDetailsRequest} from '../models/product-details-request.model';

@Component({
  selector: 'kwah-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  productId = 0;
  productLabel = '';
  reviews: ProductDetailsResponseData[] = [];
  count: number = 0;
  first = 0;
  rows = 10;
  page = 0;
  search = '';
  sort = '';
  isAscendingOrder = true;
  loading = true;
  selectedReview?: ProductDetailsResponseData;

  constructor(private route: ActivatedRoute,
              private productService: ProductService) { }

  ngOnInit(): void {
    this.productId = +(this.route.snapshot.paramMap.get('id') ?? 0);
    if (!this.productId) return;
    this.productDetails(0, this.rows, '', true, '');
  }

  loadReviews(event: any) {
    this.first = event.first;
    this.rows = event.rows;
    this.page = event.first / event.rows;
    this.sort = event.sortField;
    this.isAscendingOrder = event.sortOrder > 0;
    this.productDetails(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
  }

  private productDetails(page: number, limit: number, sort: string, ascendingOrder: boolean, search: string) {
    this.loading = true;
    this.productService.productDetails(new ProductDetailsRequest({
      page,
      limit,
      sort,
      ascendingOrder,
      search,
      productId: this.productId,
    })).subscribe(response => {
      this.productLabel = response.productLabel;
      this.reviews = response.items;
      this.count = response.count;
      this.loading = false;
    });
  }

  onSearch() {
    this.loading = true;
    this.productDetails(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
  }
}
