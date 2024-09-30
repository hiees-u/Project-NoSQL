import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  message: string;
  _id: string;
}

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = 'https://localhost:7289/api/Users/Login';

  constructor(private http: HttpClient) {}

  login(credentials: LoginRequest): Observable<LoginResponse> {  
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http
    .post<LoginResponse>(this.apiUrl, credentials, { headers });
  }
}
