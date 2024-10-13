import { Component, Input } from '@angular/core';
import { Review } from '../../module/Review.module';
import { ReviewService } from './review.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.css',
})
export class ReviewComponent {
  @Input() idProduct = '';

  reviews: Review[] = [];

  constructor(private reviewService: ReviewService) {}

  ngOnInit(): void {
    this.getReviews(this.idProduct);
  }

  getReviews(id: string): void {
    this.reviewService.getAllReviewByProduct(this.idProduct).subscribe({
      next: (reponse: Review[]) => {
        this.reviews = reponse;
      },
      error: (error) => {
        console.error('Error fetching reviews:', error);
      },
      complete: () => {
        console.log('Review fetching complete');
      },
    });
  }
}
