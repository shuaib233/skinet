import { IProduct } from '../shared/Models/product';
import { shopParams } from './../shared/Models/shopParams';
import { IPagination } from './../shared/Models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/Models/brand';
import { IType } from '../shared/Models/productType';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseURL ='https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  
  getProducts(shopParams:shopParams){
    let params = new HttpParams();
    if(shopParams.brandIdSelected!==0)
    params=params.append('brandId',shopParams.brandIdSelected.toString());

    if(shopParams.typeIdSelected!==0)
    params=params.append('typeId',shopParams.typeIdSelected.toString());

    params=params.append('sort',shopParams.sortSelected);
    params=params.append('pageIndex',shopParams.pageNumber.toString());
    params=params.append('pageSize',shopParams.pageSize.toString());
    if(shopParams.searchTerm)
    params=params.append('search',shopParams.searchTerm.toString());



   // return this.http.get<IPagination>(this.baseURL+"Products?"+"typeId="+typeId+"&brandId="+brandId+"&sort="+sort);//,{observe:'response',params})

    return this.http.get<IPagination>(this.baseURL+'products',{observe:'response',params})
    .pipe(
     map(response=>{return response.body;})
    );
  }
 
  getProduct(id:number)
  {
    return this.http.get<IProduct>(this.baseURL+'products/'+id);
  } 
  
  getTypes(){
    return this.http.get<IType[]>(this.baseURL+"products/types");
  }

  
  getBrands(){
    return this.http.get<IBrand[]>(this.baseURL+"products/brands");
  }


}
