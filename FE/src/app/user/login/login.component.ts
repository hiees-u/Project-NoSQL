import { Component } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { LoginRequest } from '../../../module/LoginRequest.module';
import { LoginResponse } from '../../../module/LoginResponse.module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private loginService: LoginService, private router: Router) {}

  onLogin() {
    const credentials: LoginRequest = {
      email: this.email,
      password: this.password
    }

    const observer = {
      next: (response: LoginResponse) => {
        console.log('Đăng nhập thành công:', response.message);
        // Lưu token vào localStorage
        localStorage.setItem('token', response.token);
        this.router.navigate(['/home']);
      },
      error: (error: any) => {
        console.error('Đăng nhập thất bại:', error);
      },
      complete: () => {
        console.log('Observable completed'); // Optional logging
      }
    };

    this.loginService.login(credentials).subscribe(observer);
  };
}
