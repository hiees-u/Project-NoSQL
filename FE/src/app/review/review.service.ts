import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../../module/Review.module';
@Injectable({
  providedIn: 'root',
})
export class ReviewService {
  private apiUrl =
    'https://localhost:7289/api/Reviews/Get Reviews By ProductId/';

  constructor(private http: HttpClient) {}

  getAllReviewByProduct(id: string): Observable<Review[]> {
    const url = `${this.apiUrl}${id}`;
    return this.http.get<Review[]>(url);
  }
}
