import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {ProductFilterRequest} from '../models/product-filter-request.model';
import {ProductFilterResponse} from '../models/product-filter-response.model';
import {IFilterRequest} from '../models/i-filter-request.model';
import {IndexOneProductRequest} from '../models/index-one-product-request.model';
import {IndexOneProductResponse} from '../models/index-one-product-response.model';
import {IndexManyProductsRequest} from '../models/index-many-products-request.model';
import {IndexManyProductsResponse} from '../models/index-many-products-response.model';
import {ProductDetailsRequest} from '../models/product-details-request.model';
import {ProductDetailsResponse} from '../models/product-details-response.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  basePath = `${environment.baseUrl}/product`;

  constructor(private http: HttpClient) {
  }

  filterProducts(request: ProductFilterRequest): Observable<ProductFilterResponse> {
    const params = this.buildQueryStringParams(request);
    return this.http.get<ProductFilterResponse>(`${this.basePath}/filter`, {params});
  }

  productDetails(request: ProductDetailsRequest): Observable<ProductDetailsResponse> {
    let params = this.buildQueryStringParams(request);
    params = params.set('productId', request.productId);
    return this.http.get<ProductDetailsResponse>(`${this.basePath}/details`, {params});
  }

  addProduct(request: IndexOneProductRequest): Observable<IndexOneProductResponse>  {
    return this.http.post<IndexOneProductResponse>(`${this.basePath}/index-one`, request);
  }

  addProducts(request: IndexManyProductsRequest): Observable<IndexManyProductsResponse> {
    return this.http.post<IndexManyProductsResponse>(`${this.basePath}/index-many`, request);
  }

  private buildQueryStringParams(request: IFilterRequest): HttpParams {
    if (!request) return new HttpParams();
    let params = new HttpParams();
    if (request.page !== null && request.page !== undefined) {
      params = params.set('page', request.page);
    }

    if (request.limit) {
      params = params.set('limit', request.limit);
    }

    if (request.sort) {
      params = params.set('sort', request.sort);
    }

    if (request.ascendingOrder !== null && request.ascendingOrder !== undefined) {
      params = params.set('ascendingOrder', request.ascendingOrder);
    }

    if (request.search) {
      params = params.set('search', request.search);
    }
    return params;
  }
}
