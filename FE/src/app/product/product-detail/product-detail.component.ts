import { Component, Input } from '@angular/core';
import { Product } from '../../../module/product.module';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent {
  product!: Product;

  ratingPercentage:number = 0;

  content: string = '';
  token = localStorage.getItem('token');

  apiUrl: string = 'https://localhost:7289/api/Reviews/Create Reviews';
  
  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient) {
    this.route.paramMap.subscribe(params => {
      const jsonData = params.get('data');
      if(jsonData) {
        this.product = JSON.parse(decodeURIComponent(jsonData));
      }
    })

    this.ratingPercentage = this.product.rating * 20;

    console.log(this.token);
    
  }
  
  back() {
    this.router.navigate(['/home']);
  }

  ngOnInit(): void {
    console.log(this.product);    
  }
  
  postReview() {
    const review = {
      product_id: this.product._id,
      rating: 5,
      content: this.content
    };

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}` // Make sure the token is correct
    });

    console.log('log Post Review:', review);    

    return this.http.post(this.apiUrl, review, {headers})
      .subscribe(response => {
        console.log('Review posted successfully', response);
      }, error => {
        console.error('Error posting review', error);
      });
  }
}
