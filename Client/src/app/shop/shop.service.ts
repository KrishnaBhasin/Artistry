import { IProduct } from 'src/app/shared/models/product';
import { shopParams } from './../shared/models/shopParams';
import { IProductTypes } from './../shared/models/productTypes';
import { IBrand } from './../shared/models/brands';
import { HttpClient, HttpClientModule, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import {map} from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseurl = 'https://localhost:44357/api/'
  constructor(private http:HttpClient) { }

  getProducts(shopParams: shopParams){
    let params = new HttpParams();
    if(shopParams.brandId!=0)
    {
      params=  params.append('brandId',shopParams.brandId.toString());
    }
    if(shopParams.typeId!=0)
    {
      params=  params.append('typeId',shopParams.typeId.toString());
    }
    if(shopParams.search)
    {
      params = params.append('search',shopParams.search);
    }


    params=  params.append('sort',shopParams.sort.toString());
    params = params.append('pageIndex',shopParams.pageNumber.toString())
    params = params.append('pageIndex',shopParams.pageSize.toString())
    return this.http.get<IPagination>(this.baseurl+'products', {observe:'response',params})
      .pipe(
        map(response=>{
          return response.body;
        })
      );
  }
  getProduct(id:number){
    return this.http.get<IProduct>(this.baseurl+'products/'+id);
  }
  getBrands(){
    return this.http.get<IBrand[]>(this.baseurl+'products/brands');
  }
  getProductTypess(){
    return this.http.get<IProductTypes[]>(this.baseurl+'products/types');
  }

}
