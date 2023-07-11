import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from '../app/interceptors/jwt.interceptor';
import { AppComponent } from './app.component';
import { LoginComponent } from '../account/login/login.component'
import { HomeComponent } from '../account/home/home.component'
import { RegisterComponent } from '../account/register/register.component';
import { ChangePasswordComponent } from '../account/change-password/change-password.component';
import { BoardServiceComponent } from '../dashboard/boards/boardservice/boardservice.component';
import { HomeDashboardComponent } from '../dashboard/home-dashboard/home-dashboard.component';
import { CreateBoardComponent } from '../dashboard/boards/boardservice/create-board/create-board.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent,
    HomeDashboardComponent,
    BoardServiceComponent,
    CreateBoardComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    // Rejestruj interceptror JwtInterceptor
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
