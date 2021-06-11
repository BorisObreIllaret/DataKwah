import {Component, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {Observable} from 'rxjs';
import {ProductFilterResponseData} from '../models/product-filter-response.model';
import {ProductFilterRequest} from '../models/product-filter-request.model';
import {map} from 'rxjs/operators';

@Component({
  selector: 'kwah-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products$ = new Observable<ProductFilterResponseData[]>();

  constructor(private productService: ProductService) {
  }

  ngOnInit(): void {
    this.products$ = this.productService.filterProducts(new ProductFilterRequest()).pipe(
      map(response => response.items)
    );
  }

}
