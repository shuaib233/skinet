import { IType } from './../shared/Models/productType';
import { ShopService } from './shop.service';
import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/Models/product';
import { IBrand } from '../shared/Models/brand';
import { shopParams } from '../shared/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products:IProduct[];
  types:IType[];
  brands:IBrand[];
  shopParams = new shopParams()

  sortOptions = [
    {name:'Alphabetical',value:'name'},
    {name:'Price High to Low',value:'priceDesc'},
    {name:'Price Low to High',value:'priceAsc'}

  ];
  RecordsCount: number;

  constructor(private shopService: ShopService) { }

  ngOnInit() {
   this.getTypes();
   this.getBrands();
   this.getProducts();
  }

getProducts(){
  this.shopService.getProducts(this.shopParams).subscribe(response=>{
    this.products = response!.data;
    this.shopParams.pageNumber = response.pageIndex;
    this.shopParams.pageSize = response.pageSize;
    this.RecordsCount = response.count;

    console.log(response?.data);
  },error=>{
    console.log(error);
  });
}

getBrands()
{
  this.shopService.getBrands().subscribe(response=>{
    this.brands =  [{id:0,name:"All"}, ...response];
   },error=>{
    console.log(error);
  });
}

getTypes()
{
  this.shopService.getTypes().subscribe(response=>{
    this.types =  [{id:0,name:"All"}, ...response];
  },error=>{
    console.log(error);
  });
}

onBrandSelection(brandId:number)
{
  this.shopParams.brandIdSelected = brandId;
  this.shopParams.pageNumber =1;
  this.getProducts();
}

onTypeSelection(typeId:number)
{
  this.shopParams.typeIdSelected = typeId;
  this.shopParams.pageNumber =1;
  this.getProducts();
}

onSortSelection(sortOption:string)
{
  this.shopParams.sortSelected = sortOption;
  this.shopParams.pageNumber =1;
  this.getProducts();
}
onSortChange(value:any) {
  this.shopParams.sortSelected = value;
  this.shopParams.pageNumber =1;
  this.getProducts();
}

onPageChanged(event:any) {
  if(this.shopParams.pageNumber!==event)
  {
  this.shopParams.pageNumber = event;
  this.getProducts();
  }
}
//  sort = document.getElementById('sort');
//  sort?.addEventListener('',event=>{
//    const target = event.target as HTMLInputElement;
//    console.log(target.value);
   
//  })



}
