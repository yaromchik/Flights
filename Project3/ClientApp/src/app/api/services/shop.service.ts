/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';

import { Product } from '../models/product';
import { searchShop } from '../fn/shop/search-shop';
import { SearchShop$Params } from '../fn/shop/search-shop';
import { searchShop$Plain } from '../fn/shop/search-shop-plain';
import { SearchShop$Plain$Params } from '../fn/shop/search-shop-plain';

@Injectable({ providedIn: 'root' })
export class ShopService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `searchShop()` */
  static readonly SearchShopPath = '/api/Shop';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchShop$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchShop$Plain$Response(params?: SearchShop$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Product>>> {
    return searchShop$Plain(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchShop$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchShop$Plain(params?: SearchShop$Plain$Params, context?: HttpContext): Observable<Array<Product>> {
    return this.searchShop$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Product>>): Array<Product> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `searchShop()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchShop$Response(params?: SearchShop$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<Product>>> {
    return searchShop(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `searchShop$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  searchShop(params?: SearchShop$Params, context?: HttpContext): Observable<Array<Product>> {
    return this.searchShop$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<Product>>): Array<Product> => r.body)
    );
  }

}
