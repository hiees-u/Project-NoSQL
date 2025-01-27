import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';

import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { Router } from '@angular/router';
import { ProductComponent } from './product/product.component';
import { ProductDetailComponent } from './product/product-detail/product-detail.component';
import { ReviewComponent } from './review/review.component';
import { FormsModule } from '@angular/forms'; 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductComponent,
    ProductDetailComponent,
    ReviewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    UserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    Router
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
