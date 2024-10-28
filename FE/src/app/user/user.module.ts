import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { LoginComponent } from './login/login.component';
import { LoginModule } from './login/login.module';

@NgModule({
  declarations: [UserComponent, LoginComponent],
  imports: [
    CommonModule, 
    UserRoutingModule, 
    // LoginModule, 
    FormsModule
  ],
  exports: [LoginComponent],
})
export class UserModule {}
