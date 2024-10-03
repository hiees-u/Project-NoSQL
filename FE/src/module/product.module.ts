import { Review } from "./Review.module";

export interface Product {
  _id: string;
  name: string;
  price: Int32Array;
  description: string;
  category: string;
  images: string[];
  rating: number;
  total_ratings: number;
  reviews: Review[];
}
