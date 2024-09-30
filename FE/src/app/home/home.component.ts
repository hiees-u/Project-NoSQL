import { Component } from '@angular/core';
import { Product } from '../../module/product.module';
import { ProductService } from '../product/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  userId = sessionStorage.getItem('userId');

  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (date) => {
        this.products = date;
      }, (error) => {
        console.log('Lỗi khi lấy dữ liệu sản phẩm', error);
      }
    )
  }
}
