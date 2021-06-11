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
    return this.http.get<ProductFilterResponse>(`${this.basePath}/filter`, {params});
  }

  private buildQueryStringParams(request: IFilterRequest): HttpParams {
    const params = new HttpParams();
    if (!request) return params;
    request.page && params.append('page', request.page);
    request.limit && params.append('limit', request.limit);
    request.sort && params.append('sort', request.sort);
    request.ascendingOrder && params.append('ascendingOrder', request.ascendingOrder);
    request.search && params.append('search', request.search);
    return params;
  }
}
