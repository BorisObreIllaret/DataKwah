import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {ProductFilterRequest} from '../models/product-filter-request.model';
import {ProductFilterResponse} from '../models/product-filter-response.model';
import {IFilterRequest} from '../models/i-filter-request.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  basePath = `${environment.baseUrl}/product`;

  constructor(private http: HttpClient) {
  }

  filterProducts(request: ProductFilterRequest): Observable<ProductFilterResponse> {
    const params = this.buildQueryStringParams(request);
    console.log('filterProducts', params);
    return this.http.get<ProductFilterResponse>(`${this.basePath}/filter`, {params});
  }

  private buildQueryStringParams(request: IFilterRequest): HttpParams {
    if (!request) return new HttpParams();
    let params = new HttpParams();
    console.log('buildQueryStringParams', request);
    if (request.page !== null && request.page !== undefined) {
      params = params.set('page', request.page);
    }

    if (request.limit) {
      params = params.set('limit', request.limit);
    }

    if (request.sort) {
      params = params.set('sort', request.sort);
    }

    if (request.ascendingOrder) {
      params = params.set('ascendingOrder', request.ascendingOrder);
    }

    if (request.search) {
      params = params.set('search', request.search);
    }
    return params;
  }
}
