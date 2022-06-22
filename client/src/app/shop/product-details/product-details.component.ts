import { ShopService } from './../shop.service';
import { IProduct } from '../../shared/Models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  constructor(private shopService:ShopService,private activateRoute:ActivatedRoute) { }
  product:IProduct
  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct()
  {
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(prod=>{
      this.product = prod
    },error=>{
      console.log(error);
    });
    
  }

}
