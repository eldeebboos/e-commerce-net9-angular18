import { Component, inject, Inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  url = environment.apiUrl + 'products';
  http = inject(HttpClient);
  products: Product[] = [];
  title = 'client';
  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.url).subscribe((response) => {
      this.products = response.data;
      console.log(response);
    });
  }
}
