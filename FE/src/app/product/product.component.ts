import { Component, Input } from '@angular/core';
import { Product } from '../../module/product.module';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  @Input() product!: Product;

  truncateName(name: string):string {
    if(name.length > 22)
    {
      return name.substring(0, 20) + '...';
    }
    return name;
  }
}
