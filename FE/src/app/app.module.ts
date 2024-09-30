import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';

import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { Router } from '@angular/router';
import { ProductComponent } from './product/product.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    UserModule,
    HttpClientModule,
  ],
  providers: [
    Router
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
