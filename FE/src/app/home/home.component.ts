import { Component } from '@angular/core';
import { Product } from '../../module/product.module';
import { ProductService } from '../product/product.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  userId = sessionStorage.getItem('userId');

  products: Product[] = [];

  showProduct_detail: boolean = false; 

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      (data) => {
        this.products = data;
      }, (error) => {
        console.log('Lỗi khi lấy dữ liệu sản phẩm', error);
      }
    )
  }

  isShowProductDetail(product: Product) {
    this.showProduct_detail = !this.showProduct_detail;
    const jsonData = JSON.stringify(product);

    this.router.navigate(['/product-detail', encodeURIComponent(jsonData)]);
  }
}
