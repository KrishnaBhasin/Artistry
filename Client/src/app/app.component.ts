import { IPagination } from './models/pagination';
import { IProduct } from './models/product';
import { HttpClient } from '@angular/common/http';
import { Component ,OnInit} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Artistry';
  products: IProduct[];

  constructor(private http:HttpClient){}
  ngOnInit(): void {

    this.http.get('https://localhost:44357/api/Products?pageSize=50').subscribe((response:IPagination)=>{
      this.products=response.data;
    },(error:IPagination)=>{
      console.log(error);
    })

  }

}
