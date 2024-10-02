import { Component, Input } from '@angular/core';
import { Product } from '../../../module/product.module';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent {
  product!: Product;

  constructor(private route: ActivatedRoute, private router: Router) {
    this.route.paramMap.subscribe(params => {
      const jsonData = params.get('data');
      if(jsonData) {
        this.product = JSON.parse(decodeURIComponent(jsonData));
      }
    })
  }

  back() {
    this.router.navigate(['/home']);
  }

  ngOnInit(): void {
    console.log(this.product);    
  }
}