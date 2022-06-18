import { IPagination } from './Models/pagination';
import { IProduct } from './Models/product';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
 
  title = 'Skinet Client';
  products?:IProduct[];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get("https://localhost:5001/api/Products").subscribe((response:IPagination)=>{
      console.log(response);
      this.products = response.data;
    }, error=>{
       console.log(error);
    }); 
    
  
  }
}
