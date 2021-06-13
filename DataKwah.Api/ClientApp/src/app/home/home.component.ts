import {Component, OnInit} from '@angular/core';
import {ProductService} from '../services/product.service';
import {MessageService} from 'primeng/api';
import {ProductFilterResponseData} from '../models/product-filter-response.model';
import {ProductFilterRequest} from '../models/product-filter-request.model';
import {IndexOneProductRequest} from '../models/index-one-product-request.model';
import {IndexManyProductsRequest} from '../models/index-many-products-request.model';

@Component({
  selector: 'kwah-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  products: ProductFilterResponseData[] = [];
  count: number = 0;
  dialogVisible = false;
  first = 0;
  rows = 10;
  page = 0;
  search = '';
  sort = '';
  isAscendingOrder = true;
  loading = true;
  newASIN = '';
  newASINs: {asin: string}[] = [{asin: ''}];

  constructor(private productService: ProductService,
              private messageService: MessageService) {
  }

  ngOnInit(): void {
    this.filterProducts(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
  }

  loadProducts(event: any) {
    this.first = event.first;
    this.rows = event.rows;
    this.page = event.first / event.rows;
    this.sort = event.sortField;
    this.isAscendingOrder = event.sortOrder > 0;
    this.filterProducts(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
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
    });
  }

  addNew() {
    this.loading = true;
    this.productService.addProduct(new IndexOneProductRequest({asin: this.newASIN})).subscribe(response => {
      let life = 5000;
      let severity = 'success';
      let stateLabel = '';
      let summary = 'Success';
      switch (response.state) {
        case 0:
          stateLabel = 'requested';
          break;
        case 1:
          stateLabel = 'pending';
          break;
        case 2:
          stateLabel = 'complete';
          break;
        case 3:
          life = 10000;
          stateLabel = 'failed';
          severity = 'warn';
          summary = 'Warning';
          break;
      }

      this.messageService.add({
        detail: `Indexation of ASIN '${response.asin}' ${stateLabel}`,
        closable: true,
        life,
        severity,
        summary,
      });

      this.filterProducts(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
      this.newASIN = '';
    });
  }

  openDialog() {
    this.dialogVisible = true;
  }

  addNewAsinInList() {
    this.newASINs.push({asin: ''});
  }

  closeDialog() {
    this.loading = true;
    const asins = this.newASINs.filter(asin => asin.asin).map(asin => asin.asin);
    console.log('closeDialog', asins);
    this.productService.addProducts(new IndexManyProductsRequest({asins})).subscribe(response => {
        this.messageService.add({
          detail: `Indexation of ${response.indexingCount} ASINs done`,
          closable: true,
          life: 5000,
          severity: 'success',
          summary: 'Success',
        });
        this.filterProducts(0, this.rows, '', true, '');
        this.dialogVisible = false;
      }
    )
  }

  onSearch() {
    this.loading = true;
    this.filterProducts(this.page, this.rows, this.sort , this.isAscendingOrder, this.search);
  }
}
